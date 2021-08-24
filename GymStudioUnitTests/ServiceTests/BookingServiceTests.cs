using Xunit;
using Moq;
using GymStudioApi.Exceptions;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;
using GymStudioApi.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GymStudioUnitTests.ServiceTests
{
    public class BookingServiceTests
    {
        Mock<IBookingRepository> mockBookingRepository;
        Mock<IClassService> mockClassService;
        Booking newBooking;

        [Fact]
        public async void BookingService_ClassDoesNotExist_ThrowsException()
        {
            //Assign
            SetupTestInfo();
            Class existingClass = null;
            this.mockClassService.Setup(c => c.GetClass(It.IsAny<Guid>())).ReturnsAsync(existingClass);            

            var sut = new BookingService(this.mockClassService.Object, this.mockBookingRepository.Object);

            //Act
            Func<Task> act = () => sut.CreateBooking(this.newBooking);

            //Assert
            var exception = await Assert.ThrowsAsync<ClassNotFoundException>(act);
            Assert.Equal("Class does not exist - Please review the information provided", exception.Message);
        }

        [Fact]
        public async void BookingService_ClassSessionNotAvailableOnDate_ThrowsException()
        {
            //Assign
            SetupTestInfo();
            ClassSession existingClassSession = null;
            this.mockClassService.Setup(c => c.GetClass(It.IsAny<Guid>())).ReturnsAsync(new Class());
            this.mockClassService.Setup(c => c.GetClassSessionsByDate(It.IsAny<Guid>(), It.IsAny<DateTime>())).ReturnsAsync(existingClassSession);

            var sut = new BookingService(this.mockClassService.Object, this.mockBookingRepository.Object);

            //Act
            Func<Task> act = () => sut.CreateBooking(this.newBooking);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("No Class Session available for this Date.", exception.Message);
        }

        [Fact]
        public async void BookingService_CreateBooking_ReturnsBooking()
        {
            //Assign
            SetupTestInfo();
            this.mockClassService.Setup(c => c.GetClass(It.IsAny<Guid>())).ReturnsAsync(new Class());
            this.mockClassService.Setup(c => c.GetClassSessionsByDate(It.IsAny<Guid>(), It.IsAny<DateTime>())).ReturnsAsync(new ClassSession());            
            this.mockBookingRepository.Setup(r => r.SaveBooking(It.IsAny<Booking>())).ReturnsAsync(this.newBooking);

            var sut = new BookingService(this.mockClassService.Object, this.mockBookingRepository.Object);

            //Act
            var response = await sut.CreateBooking(this.newBooking);

            //Assert
            Assert.NotNull(response);
            Assert.NotEqual(Guid.Empty, response.Id);
        }

        [Fact]
        public async void BookingService_GetBookings_ReturnsBookings()
        {
            //Assign
            SetupTestInfo();
            List<Booking> listOfBookings = new List<Booking>();
            var anotherBooking = new Booking()
            {
                ClassId = this.newBooking.ClassId,
                Name = "NameOfAnotherBookingMember",
                Date = DateTime.UtcNow.Date
            };
            listOfBookings.Add(this.newBooking);
            listOfBookings.Add(anotherBooking);

            this.mockBookingRepository.Setup(r => r.GetBookings(It.IsAny<Guid>())).ReturnsAsync(listOfBookings);
            var sut = new BookingService(this.mockClassService.Object, this.mockBookingRepository.Object);

            //Act
            var response = await sut.GetBookings(this.newBooking.ClassId);

            //Assert
            Assert.Equal(2, response.Count);
            Assert.Equal(this.newBooking.ClassId, response[0].ClassId);
            Assert.Equal(this.newBooking.ClassId, response[1].ClassId);
        }

        private void SetupTestInfo()
        {
            this.mockBookingRepository = new Mock<IBookingRepository>();
            this.mockClassService = new Mock<IClassService>();
            this.newBooking = new Booking()
            {
                ClassId = Guid.NewGuid(),
                Name = "NameOfBookingMember",
                Date = DateTime.UtcNow.Date
            };
        }
    }
}