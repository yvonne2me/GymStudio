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
    public class ClassControllerTests
    {

        Mock<IFileLogger> mockLogger;
        Mock<IClassService> mockClassService;
        Mock<IMapper> mockMapper;
        ClassController classController;
        ClassRequest classRequest;

        [Fact]
        public async void ClassController_Post_NullRequest_Returns_BadRequest()
        {
            //Assign
            SetupTestInfo();
            this.classController = new ClassController(mockLogger.Object, mockClassService.Object, mockMapper.Object);
            ClassRequest classRequest = null;

            //Act
            var response = await classController.Post(classRequest);
            var badResponse = response as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("No Class information provided", badResponse.Value);
        }

        [Fact]
        public async void ClassController_Post_ValidRequest_Returns_OK()
        {
            //Assign
            SetupTestInfo();
            this.classController = new ClassController(mockLogger.Object, mockClassService.Object, mockMapper.Object);

            //Act
            var response = await classController.Post(classRequest);
            var okResponse = response as OkObjectResult;

            //Assert
            Assert.Equal(200, okResponse.StatusCode);
        }

        [Fact]
        public async void ClassController_Post_ExceptionThrown_Returns_BadRequest()
        {
            //Assign
            SetupTestInfo();
            this.mockClassService.Setup(s => s.CreateClass(It.IsAny<Class>())).Throws(new Exception());
            this.classController = new ClassController(mockLogger.Object, mockClassService.Object, mockMapper.Object);

            //Act
            var response = await classController.Post(classRequest);
            var badResponse = response as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, badResponse.StatusCode);
        }

        private void SetupTestInfo()
        {
            this.mockLogger = new Mock<IFileLogger>();
            this.mockClassService = new Mock<IClassService>();
            this.mockMapper = new Mock<IMapper>();
            this.classRequest = new ClassRequest()
            {
                ClassName = "ClassName",
                Start_Date = System.DateTime.Now,
                End_Date = System.DateTime.Now.AddDays(1),
                Capacity = 10
            };
        }
    }
}
