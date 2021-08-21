using System;

namespace GymStudioApi.Models.Domain
{
    public class Class
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Capacity { get; set; }
    }
}