using System;
using System.ComponentModel.DataAnnotations;

namespace GymStudioApi.Models.API
{
    public class BookingRequest
    {
        [Required]
        public Guid ClassId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? Date { get; set;}
    }
}