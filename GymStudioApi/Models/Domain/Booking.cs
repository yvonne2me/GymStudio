using System;

namespace GymStudioApi.Models.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}