using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymStudioApi.Exceptions;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;

namespace GymStudioApi.Services
{
    public class BookingService : IBookingService
    {
        IClassService classService;
        IBookingRepository bookingRepository;

        public BookingService(IClassService classService, IBookingRepository bookingRepository)
        {
            this.classService = classService;
            this.bookingRepository = bookingRepository;
        }

        public async Task<Booking> CreateBooking(Booking newBooking)
        {
            var existingClass = await classService.GetClass(newBooking.ClassId);
            if(existingClass == null)
            {
                throw new ClassNotFoundException("Class does not exist - Please review the information provided");
            }

            var availableClassSessions = await classService.GetClassSessionsByDate(newBooking.ClassId, newBooking.Date);

            if(availableClassSessions == null)
            {
                throw new ArgumentException("No Class Session available for this Date.");
            }

            newBooking.Id = Guid.NewGuid();

            return await bookingRepository.SaveBooking(newBooking);
        }

        public async Task<List<Booking>> GetBookings(Guid classId)
        {
            return await this.bookingRepository.GetBookings(classId);
        }
    }
}