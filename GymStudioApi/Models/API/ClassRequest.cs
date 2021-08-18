using System;

namespace GymStudioApi.Models.API
{
    public class ClassRequest
    {
        public string ClassName { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public int Capacity { get; set; }
    }
}