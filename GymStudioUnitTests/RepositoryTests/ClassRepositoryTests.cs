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
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(1),
                Capacity = 2
            };

            //Act
            var response = await sut.SaveClass(saveNewClass);

            //Assert
            Assert.Equal(saveNewClass.Id, response.Id);
        }

        // [Fact]
        // public async void ClassRepository_ClassNotSaved_ThrowsException()
        // {
        //     //Assign
        //     SetupTestInfo();

        //     DbContextOptions<GymStudioContext> options = new DbContextOptions<GymStudioContext>();
        //     Mock<GymStudioContext> mockGymStudioContext = new Mock<GymStudioContext>();
        //     Mock<DbSet<Class>> mockDbSet = new Mock<DbSet<Class>>();

        //     mockGymStudioContext.Setup(x => x.Classes.Add(It.IsAny<Class>())).Returns(It.IsAny<EntityEntry<Class>>());
        //     mockGymStudioContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            
        //     var sut = new ClassRepository(mockLogger.Object, mockGymStudioContext.Object);

        //     Class saveNewClass = new Class()
        //     {
        //         Id = Guid.NewGuid(),
        //         ClassName = "SaveNewClass",
        //         Start_Date = DateTime.Now.AddDays(1),
        //         End_Date = DateTime.Now,
        //         Capacity = 2
        //     };

        //     //Act           
        //     Func<Task> response = () => sut.SaveClass(saveNewClass);

        //     //Assert
        //     var exception = await Assert.ThrowsAsync<Exception>(response);
        //     Assert.Equal("Error saving new Class", exception.Message);            
        // }

        [Fact]
        public async void ClassRepository_SaveClass_CreatesClassSessions()
        {
            //Assign
            SetupTestInfo();
            var sut = new ClassRepository(mockLogger.Object, _context);

            Class saveNewClass = new Class()
            {
                Id = Guid.NewGuid(),
                ClassName = "SaveNewClass",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(4),
                Capacity = 2
            };

            //Act
            await sut.SaveClass(saveNewClass);
            var classSessionResponse = await sut.GetClassSessions(saveNewClass.Id);

            //Assert
            Assert.Equal(4, classSessionResponse.Count);
        }

        private void SetupTestInfo()
        {
            mockLogger = new Mock<IFileLogger>();
            var builder = new DbContextOptionsBuilder<GymStudioContext>()
                    .UseInMemoryDatabase("GymStudioContext");

            var context = new GymStudioContext(builder.Options);

            for (var i = 0; i < 5; i++)
            {
                Class newClass = new Class()
                {
                    Id = Guid.NewGuid(),
                    ClassName = "TestClassName" + i,
                    Start_Date = DateTime.Now.AddDays(1),
                    End_Date = DateTime.Now,
                    Capacity = 10
                };

                context.Classes.Add(newClass);
            }

            context.SaveChanges();
            _context = context;
        }

    }
}