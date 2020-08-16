using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repositoryPattern.Data;
using repositoryPattern.Entities;
using repositoryPattern.Repository;

namespace repositoryPattern.Business
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Student> GetAll()
        {
            return _unitOfWork.StudentRepository.GetAll();
        }
        public Student Find(int id)
        {
            return _unitOfWork.StudentRepository.Find(id);
        }

        public bool Add(Student entity)
        {
            _unitOfWork.StudentRepository.Add(entity);
            _unitOfWork.Commit();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _unitOfWork.StudentRepository.Find(id);
            _unitOfWork.StudentRepository.Delete(entity);
            _unitOfWork.Commit();
            return true;
        }

        public bool Update(int id, Student entity)
        {
            var newEntity = _unitOfWork.StudentRepository.Find(id);
            newEntity.FirstName = entity.FirstName;
            newEntity.LastName = entity.LastName;
            _unitOfWork.StudentRepository.Update(newEntity);
            return true;
        }
    }
}
