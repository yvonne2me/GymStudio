using Xunit;
using Moq;
using GymStudioApi.Logging;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;
using GymStudioApi.Services;
using GymStudioApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace GymStudioUnitTests.ControllerTests
{
    public class BookingsControllerTests
    {
        Mock<IFileLogger> mockLogger;
        Mock<IBookingService> mockBookingService;
        Mock<IMapper> mockMapper;
        BookingRequest bookingRequest;

        [Fact]
        public async void BookingsController_Post_NullRequest_Returns_BadRequest()
        {
            //Assign
            SetupTestInfo();
            var sut = new BookingsController(mockLogger.Object, mockBookingService.Object, mockMapper.Object);
            BookingRequest bookingRequest = null;

            //Act
            var response = await sut.Post(bookingRequest);
            var badResponse = response as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, badResponse.StatusCode);
            Assert.Equal("No Booking information provided", badResponse.Value);
        }

        [Fact]
        public async void BookingsController_Post_ValidRequest_Returns_OK()
        {
            //Assign
            SetupTestInfo();
            var sut = new BookingsController(mockLogger.Object, mockBookingService.Object, mockMapper.Object);

            //Act
            var response = await sut.Post(bookingRequest);
            var okResponse = response as OkObjectResult;

            //Assert
            Assert.Equal(200, okResponse.StatusCode);
        }

        [Fact]
        public async void BookingsController_Post_ExceptionThrown_Returns_Error()
        {
            //Assign
            SetupTestInfo();
            this.mockBookingService.Setup(s => s.CreateBooking(It.IsAny<Booking>())).Throws(new Exception());
            var sut= new BookingsController(mockLogger.Object, mockBookingService.Object, mockMapper.Object);

            //Act
            Func<Task> act = () => sut.Post(bookingRequest);

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal("Error occurred while saving Booking", exception.Message);
        }

        [Fact]
        public async void BookingsController_Get_BookingNotFound_Returns_NotFound()
        {
            //Assign
            SetupTestInfo();
            List<Booking> newBooking = null;
            this.mockBookingService.Setup(s => s.GetBookings(It.IsAny<Guid>())).ReturnsAsync(newBooking);
            var sut = new BookingsController(mockLogger.Object, mockBookingService.Object, mockMapper.Object);

            //Act
            var response = await sut.Get(Guid.NewGuid());
            
            //Assert
            var notFoundResponse = Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, notFoundResponse.StatusCode);
        }

        [Fact]
        public async void BookingsController_Get_BookingFound_Returns_Booking()
        {
            //Assign
            SetupTestInfo();

            List<Booking> listOfBookings = new List<Booking>();
            listOfBookings.Add(new Booking());

            this.mockBookingService.Setup(s => s.GetBookings(It.IsAny<Guid>())).ReturnsAsync(listOfBookings);
            var sut = new BookingsController(mockLogger.Object, mockBookingService.Object, mockMapper.Object);

            //Act
            var response = await sut.Get(Guid.NewGuid());
            var okResponse = response as OkObjectResult;

            //Assert
            Assert.Equal(200, okResponse.StatusCode);
        }

        private void SetupTestInfo()
        {
            this.mockLogger = new Mock<IFileLogger>();
            this.mockBookingService = new Mock<IBookingService>();
            this.mockMapper = new Mock<IMapper>();
            this.bookingRequest = new BookingRequest()
            {
                Name = "MemberName",
                Date = DateTime.Now,
                ClassId = Guid.NewGuid()
            };
        }
    }
}
