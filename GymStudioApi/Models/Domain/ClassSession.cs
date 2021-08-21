using System;

namespace GymStudioApi.Models.Domain
{
    public class ClassSession
    {
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public string ClassName { get; set; }

        public DateTime ClassDate { get; set; }
        
        public int Capacity { get; set; }

    }
}