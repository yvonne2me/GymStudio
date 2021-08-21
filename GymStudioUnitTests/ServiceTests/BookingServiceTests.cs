using Xunit;
using Moq;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;
using GymStudioApi.Services;
using System.Threading.Tasks;
using System;

namespace GymStudioUnitTests.ServiceTests
{
    public class BookingServiceTests
    {
        [Fact]
        public async void BookingService_ClassDoesNotExist_ThrowsException()
        {
            //Assign
            Mock<IBookingRepository> mockBookingRepository = new Mock<IBookingRepository>();
            Mock<IClassService> mockClassService = new Mock<IClassService>();
            Class existingClass = null;

            mockClassService.Setup(c => c.GetClass(It.IsAny<Guid>())).ReturnsAsync(existingClass);
            
            Booking newBooking = new Booking()
            {
                ClassId = Guid.NewGuid(),
                Name = "NameOfBookingMember",
                Date = DateTime.Now
            };

            var bookingService = new BookingService(mockClassService.Object, mockBookingRepository.Object);

            //Act
            Func<Task> act = () => bookingService.CreateBooking(newBooking);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("Class does not exist - Please review the information provided", exception.Message);
        }

        [Fact]
        public async void BookingService_ClassSessionNotAvailableOnDate_ThrowsException()
        {
            //Assign
            Mock<IBookingRepository> mockBookingRepository = new Mock<IBookingRepository>();
            Mock<IClassService> mockClassService = new Mock<IClassService>();
            ClassSession existingClassSession = null;

            mockClassService.Setup(c => c.GetClass(It.IsAny<Guid>())).ReturnsAsync(new Class());
            mockClassService.Setup(c => c.GetClassSessionsByDate(It.IsAny<Guid>(), It.IsAny<DateTime>())).ReturnsAsync(existingClassSession);

            Booking newBooking = new Booking()
            {
                ClassId = Guid.NewGuid(),
                Name = "NameOfBookingMember",
                Date = DateTime.Now
            };

            var bookingService = new BookingService(mockClassService.Object, mockBookingRepository.Object);

            //Act
            Func<Task> act = () => bookingService.CreateBooking(newBooking);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(act);
            Assert.Equal("No Class Session available for this Date.", exception.Message);
        }

        [Fact]
        public async void BookingService_CreateBooking_ReturnsBooking()
        {
            //Assign
            Mock<IBookingRepository> mockBookingRepository = new Mock<IBookingRepository>();
            Mock<IClassService> mockClassService = new Mock<IClassService>();

            mockClassService.Setup(c => c.GetClass(It.IsAny<Guid>())).ReturnsAsync(new Class());
            mockClassService.Setup(c => c.GetClassSessionsByDate(It.IsAny<Guid>(), It.IsAny<DateTime>())).ReturnsAsync(new ClassSession());
            
            Booking newBooking = new Booking()
            {
                ClassId = Guid.NewGuid(),
                Name = "NameOfBookingMember",
                Date = DateTime.Now
            };

            mockBookingRepository.Setup(r => r.SaveBooking(It.IsAny<Booking>())).ReturnsAsync(newBooking);

            var bookingService = new BookingService(mockClassService.Object, mockBookingRepository.Object);

            var bookingResponse = await bookingService.CreateBooking(newBooking);

            Assert.NotNull(bookingResponse);
            Assert.NotEqual(Guid.Empty, bookingResponse.Id);
        } 
    }
}