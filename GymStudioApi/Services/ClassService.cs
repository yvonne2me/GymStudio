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

        //TODO: Fix naming of classRequest var -  confusing
        public async Task<Class> CreateClass(Class classRequest)
        {
            CheckClassRequestInfo(classRequest);

            classRequest.Id = Guid.NewGuid();

            return await this.classRepository.SaveClass(classRequest); 

        }

        public bool DeleteClass(Guid classId)
        {
            throw new NotImplementedException();
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