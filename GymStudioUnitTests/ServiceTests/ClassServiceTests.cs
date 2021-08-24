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
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date.AddDays(1),
                End_Date = DateTime.UtcNow.Date,
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Start_Date occurs after End_Date", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_ClassNameNotProvided_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = null,
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("A Class Name is required to create classes", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_ClassLimitOf30Days_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(31),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Classes would be spanning more than 30 days - Limit Reached", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_HistoricalStartDateProvided_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date.AddDays(-2),
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Start_Date provided is historical or not current.", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_ClassIdProvided_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                Id = Guid.NewGuid(),
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Id field should not be provided.", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_AssignsClassId()
        {
            //Assign
            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(r => r.SaveClass(It.IsAny<Class>())).ReturnsAsync(newClass);
            var sut = new ClassService(mockClassRepository.Object);

            //Act
            var classResponse = await sut.CreateClass(newClass);

            //Assert
            Assert.NotEqual(Guid.Empty, classResponse.Id);
        }

        [Fact]
        public async void ClassService_GetClass_ReturnsClass()
        {
            //Assign
            Guid classId = Guid.NewGuid();

            Class newClass = new Class()
            {
                Id = classId,
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(r => r.GetClassById(It.IsAny<Guid>())).ReturnsAsync(newClass);
            var sut = new ClassService(mockClassRepository.Object);

            //Act
            var classResponse = await sut.GetClass(classId);

            //Assert
            Assert.Equal(newClass.Id, classResponse.Id);
        }

        [Fact]
        public async void ClassService_CreateClass_ClassNameAlreadyExists_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(r => r.GetClassByName(It.IsAny<string>())).ReturnsAsync(new Class());
            mockClassRepository.Setup(r => r.GetClassById(It.IsAny<Guid>())).ReturnsAsync((Class)null);
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Class already exists - Please review the details that you provided.", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_ClassIdAlreadyExists_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(r => r.GetClassByName(It.IsAny<string>())).ReturnsAsync((Class)null);
            mockClassRepository.Setup(r => r.GetClassById(It.IsAny<Guid>())).ReturnsAsync(new Class());
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Class already exists - Please review the details that you provided.", exception.Message);
        }

        [Fact]
        public async void ClassService_CreateClass_BothClassIdAndNameAlreadyExist_ThrowsException()
        {
            //Assign
            Mock<IClassRepository> mockClassRepository = new Mock<IClassRepository>();
            mockClassRepository.Setup(r => r.GetClassByName(It.IsAny<string>())).ReturnsAsync(new Class());
            mockClassRepository.Setup(r => r.GetClassById(It.IsAny<Guid>())).ReturnsAsync(new Class());
            var sut = new ClassService(mockClassRepository.Object);

            Class newClass = new Class()
            {
                ClassName = "TestClassName",
                Start_Date = DateTime.UtcNow.Date,
                End_Date = DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };

            //Act
            Func<Task> act = () => sut.CreateClass(newClass);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Class already exists - Please review the details that you provided.", exception.Message);
        }        
    }
}