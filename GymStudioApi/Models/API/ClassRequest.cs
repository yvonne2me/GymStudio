using System;
using System.ComponentModel.DataAnnotations;

namespace GymStudioApi.Models.API
{
    public class ClassRequest
    {
        [Required]
        public string ClassName { get; set; }

        [Required]
        public DateTime Start_Date { get; set; }

        [Required]
        public DateTime End_Date { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Capacity { get; set; }
    }
}