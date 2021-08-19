using Xunit;
using Moq;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;
using GymStudioApi.Services;
using System.Threading.Tasks;
using System;

namespace GymStudioUnitTests.ServiceTests
{
    public class ClassServiceTests
    {
        [Fact]
        public async void ClassService_CreateClass_StartDate_Before_EndDate_ThrowsException()
        {
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                Id = Guid.NewGuid(),
                ClassName = "TestClassName",
                Start_Date = DateTime.Now.AddDays(1),
                End_Date = DateTime.Now,
                Capacity = 10
            };

            Func<Task> act = () => sut.CreateClass(newClass);

            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Start_Date cannot occur before End_Date", exception.Message);
        }

                [Fact]
        public async void ClassService_CreateClass_AssignsClassId()
        {
            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.Now,
                End_Date = DateTime.Now.AddDays(1),
                Capacity = 10
            };

            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(r => r.SaveClass(It.IsAny<Class>())).Returns(newClass);
            var sut = new ClassService(mockClassRepository.Object);

            var classResponse = await sut.CreateClass(newClass);

            Assert.NotNull(classResponse.Id);
        }
    }
}