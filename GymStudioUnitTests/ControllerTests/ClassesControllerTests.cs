using Xunit;
using Moq;
using GymStudioApi.Logging;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;
using GymStudioApi.Services;
using GymStudioApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;

namespace GymStudioUnitTests.ControllerTests
{
    public class ClassesControllerTests
    {

        Mock<IFileLogger> mockLogger;
        Mock<IClassService> mockClassService;
        Mock<IMapper> mockMapper;
        ClassRequest classRequest;

        [Fact]
        public async void ClassesController_Post_NullRequest_Returns_BadRequest()
        {
            //Assign
            SetupTestInfo();
            var sut = new ClassesController(mockLogger.Object, mockClassService.Object, mockMapper.Object);
            ClassRequest classRequest = null;

            //Act
            var response = await sut.Post(classRequest);
            var badResponse = response as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("No Class information provided", badResponse.Value);
        }

        [Fact]
        public async void ClassesController_Post_ValidRequest_Returns_OK()
        {
            //Assign
            SetupTestInfo();
            var sut = new ClassesController(mockLogger.Object, mockClassService.Object, mockMapper.Object);

            //Act
            var response = await sut.Post(classRequest);
            var okResponse = response as OkObjectResult;

            //Assert
            Assert.Equal(200, okResponse.StatusCode);
        }

        [Fact]
        public async void ClassesController_Post_ExceptionThrown_Returns_Error()
        {
            //Assign
            SetupTestInfo();
            this.mockClassService.Setup(s => s.CreateClass(It.IsAny<Class>())).Throws(new Exception());
            var sut = new ClassesController(mockLogger.Object, mockClassService.Object, mockMapper.Object);

            //Act
            Func<Task> act = () => sut.Post(classRequest);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Error occurred while saving Class", exception.Message);
        }

        [Fact]
        public async void ClassesController_Get_ClassNotFound_Returns_NotFound()
        {
            //Assign
            SetupTestInfo();
            Class newClass = null;
            this.mockClassService.Setup(s => s.GetClass(It.IsAny<Guid>())).ReturnsAsync(newClass);
            var sut = new ClassesController(mockLogger.Object, mockClassService.Object, mockMapper.Object);

            //Act
            var response = await sut.Get(Guid.NewGuid());
            
            //Assert
            var notFoundResponse = Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, notFoundResponse.StatusCode);
        }

        [Fact]
        public async void ClassesController_Get_ClassFound_Returns_Class()
        {
            //Assign
            SetupTestInfo();
            this.mockClassService.Setup(s => s.GetClass(It.IsAny<Guid>())).ReturnsAsync(new Class());
            var sut = new ClassesController(mockLogger.Object, mockClassService.Object, mockMapper.Object);

            //Act
            var response = await sut.Get(Guid.NewGuid());
            var okResponse = response as OkObjectResult;

            //Assert
            Assert.Equal(200, okResponse.StatusCode);
        }

        private void SetupTestInfo()
        {
            this.mockLogger = new Mock<IFileLogger>();
            this.mockClassService = new Mock<IClassService>();
            this.mockMapper = new Mock<IMapper>();
            this.classRequest = new ClassRequest()
            {
                ClassName = "ClassName",
                Start_Date = System.DateTime.UtcNow.Date,
                End_Date = System.DateTime.UtcNow.Date.AddDays(1),
                Capacity = 10
            };
        }
    }
}
