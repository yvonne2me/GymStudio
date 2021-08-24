using Xunit;
using Moq;
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
    public class BookingRepositoryTests
    {
        GymStudioContext _context;
        Mock<IFileLogger> mockLogger;

        [Fact]
        public async void BookingRepository_SaveBooking_Success()
        {
            //Assign
            SetupTestInfo();
            var sut = new BookingRepository(mockLogger.Object, _context);

            Booking saveNewBooking = new Booking()
            {
                Id = Guid.NewGuid(),
                ClassId = Guid.NewGuid(),
                Name = "SaveNewBooking",
                Date = DateTime.UtcNow.Date
            };

            //Act
            var response = await sut.SaveBooking(saveNewBooking);

            //Assert
            Assert.Equal(saveNewBooking.Id, response.Id);
        }

        [Fact]
        public async void BookingRepository_GetBookings_ReturnsBookings()
        {
            //Assign
            SetupTestInfo();
            var sut = new BookingRepository(mockLogger.Object, _context);

            var classId = Guid.NewGuid();

            for(var i = 0; i < 2; i++)
            {
                Booking saveNewBooking = new Booking()
                {
                    Id = Guid.NewGuid(),
                    ClassId = classId,
                    Name = "SaveNewBooking" + i,
                    Date = DateTime.UtcNow.Date
                };

                await sut.SaveBooking(saveNewBooking);
            }

            //Act
            var response = await sut.GetBookings(classId);

            //Assert
            Assert.NotEmpty(response);
            Assert.Equal(2, response.Count);
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