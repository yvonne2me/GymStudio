using System;
using System.Threading.Tasks;
using GymStudioApi.Models.API;
using GymStudioApi.Models.Domain;
using GymStudioApi.Repositories;

namespace GymStudioApi.Services
{
    public class ClassService : IClassService
    {
        IClassRepository classRepository;

        public ClassService(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public async Task<Class> CreateClass(Class newClass)
        {
            CheckClassRequestInfo(newClass);

            newClass.Id = Guid.NewGuid();

            return await this.classRepository.SaveClass(newClass);
        }

        public bool DeleteClass(Guid classId)
        {
            throw new NotImplementedException();
        }

        public async Task<Class> GetClass(Guid classId)
        {
            return await this.classRepository.GetClass(classId);
        }

        private void CheckClassRequestInfo(Class classRequest)
        {
            if(classRequest.Start_Date > classRequest.End_Date)
            {
                throw new ArgumentException("Start_Date cannot occur before End_Date");
            }
        }
    }
}