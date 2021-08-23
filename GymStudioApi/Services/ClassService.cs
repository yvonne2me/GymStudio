using System;
using System.Collections.Generic;
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

            await CheckIfClassAlreadyExists(newClass);

            return await this.classRepository.SaveClass(newClass);
        }

        public async Task<List<ClassSession>> GetAllClassSessions(Guid classId)
        {
            return await this.classRepository.GetAllClassSessions(classId);
        }

        public async Task<ClassSession> GetClassSessionsByDate(Guid classId, DateTime date)
        {
            return await this.classRepository.GetClassSessionsByDate(classId, date);
        }

        public bool DeleteClass(Guid classId)
        {
            throw new NotImplementedException();
        }

        public async Task<Class> GetClass(Guid classId)
        {
            return await this.classRepository.GetClassById(classId);
        }

        private static void CheckClassRequestInfo(Class classRequest)
        {
            if(classRequest.ClassName == null)
            {
                throw new ArgumentException("A Class Name is required to create classes");
            }

            if(classRequest.Start_Date > classRequest.End_Date)
            {
                throw new ArgumentException("Start_Date cannot occur before End_Date");
            }

            //ToDo: Add limit to how far into future classes can run?
        }

        private async Task CheckIfClassAlreadyExists(Class newClass)
        {
            var classWithSameName = await this.classRepository.GetClassByName(newClass.ClassName);
            var classWithSameId = await this.classRepository.GetClassById(newClass.Id);

            if(classWithSameName != null || classWithSameId != null)
            {
                throw new ArgumentException("Class already exists - Please review the details that you provided.");
            } 
        }
    }
}