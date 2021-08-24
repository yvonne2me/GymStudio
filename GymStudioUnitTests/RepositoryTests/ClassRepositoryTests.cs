using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using GymStudioApi.Models;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;
using GymStudioApi.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace GymStudioUnitTests.RepositoryTests
{
    public class ClassRepositoryTests
    {
        GymStudioContext _context;
        Mock<IFileLogger> mockLogger;

        [Fact]
        public async void ClassRepository_SaveClass_Success()
        {
            //Assign
            SetupTestInfo();
            var sut = new ClassRepository(mockLogger.Object, _context);

            Class saveNewClass = new Class()
            {
                Id = Guid.NewGuid(),
                ClassName = "SaveNewClass",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 2
            };

            //Act
            var response = await sut.SaveClass(saveNewClass);

            //Assert
            Assert.Equal(saveNewClass.Id, response.Id);
        }

        [Fact]
        public async void ClassRepository_SaveClass_CreatesClassSessions()
        {
            //Assign
            SetupTestInfo();
            var sut = new ClassRepository(mockLogger.Object, _context);
            var numberOfClassSessions = 4;

            Class saveNewClass = new Class()
            {
                Id = Guid.NewGuid(),
                ClassName = "SaveNewClass",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(numberOfClassSessions),
                Capacity = 2
            };

            //Act
            await sut.SaveClass(saveNewClass);
            var classSessionResponse = await sut.GetAllClassSessions(saveNewClass.Id);

            //Assert
            Assert.Equal(numberOfClassSessions, classSessionResponse.Count);
        }

        [Fact]
        public async void ClassRepository_SaveClass_CreatesClassSessions_WithCorrectCapacity()
        {
            //Assign
            SetupTestInfo();
            var sut = new ClassRepository(mockLogger.Object, _context);
            var expectedCapacity = 3;

            Class saveNewClass = new Class()
            {
                Id = Guid.NewGuid(),
                ClassName = "SaveNewClass",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(4),
                Capacity = expectedCapacity
            };

            //Act
            await sut.SaveClass(saveNewClass);
            var classSessionResponse = await sut.GetAllClassSessions(saveNewClass.Id);

            //Assert
            foreach(var classSession in classSessionResponse)
            {
                Assert.Equal(expectedCapacity, classSession.Capacity);
            }
        }
        private void SetupTestInfo()
        {
            mockLogger = new Mock<IFileLogger>();
            var builder = new DbContextOptionsBuilder<GymStudioContext>()
                    .UseInMemoryDatabase("GymStudioContext");

            _context = new GymStudioContext(builder.Options);
        }
    }
}