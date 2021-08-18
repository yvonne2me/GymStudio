using Xunit;
using Moq;
using GymStudioApi.Logging;
using GymStudioApi.Models.API;
using GymStudioApi.Services;
using GymStudioApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GymStudioUnitTests.ControllerTests
{
    public class ClassControllerTests
    {
        [Fact]
        public void post_returns_bad_request_when_request_is_null()
        {
            //Assign
            var mockLogger = new Mock<IFileLogger>();
            var mockClassService = new Mock<IClassService>();

            var classController = new ClassController(mockLogger.Object, mockClassService.Object);
            ClassRequest classRequest = null;

            //Act
            var response = classController.Post(classRequest);
            var badResponse = response as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("No Class information provided", badResponse.Value);
        }

        [Fact]
        public void post_returns_ok_when_request_is_valid()
        {
            //Assign
            var mockLogger = new Mock<IFileLogger>();
            var mockClassService = new Mock<IClassService>();

            var classController = new ClassController(mockLogger.Object, mockClassService.Object);
            ClassRequest classRequest = new ClassRequest()
            {
                ClassName = "ClassName",
                Start_Date = System.DateTime.Now,
                End_Date = System.DateTime.Now.AddDays(1),
                Capacity = 10
            };

            //Act
            var response = classController.Post(classRequest);
            var okResponse = response as OkObjectResult;

            //Assert
            Assert.Equal(200, okResponse.StatusCode);
        }

        [Fact]
        public void post_catches_exception_and_returns_bad_request()
        {
            //Assign
            var mockLogger = new Mock<IFileLogger>();
            var mockClassService = new Mock<IClassService>();
            mockClassService.Setup(s => s.CreateClass(It.IsAny<ClassRequest>())).Throws(new Exception());

            var classController = new ClassController(mockLogger.Object, mockClassService.Object);
            ClassRequest classRequest = new ClassRequest()
            {
                ClassName = "ClassName",
                Start_Date = System.DateTime.Now,
                End_Date = System.DateTime.Now.AddDays(1),
                Capacity = 10
            };

            //Act
            var response = classController.Post(classRequest);
            var badResponse = response as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, badResponse.StatusCode);
        }
    }
}
