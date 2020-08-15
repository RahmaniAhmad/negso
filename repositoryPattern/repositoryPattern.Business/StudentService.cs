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
        private readonly IStudentRepository _studentRepository;

        private readonly IUnitOfWork _unitOfWork;

        public StudentService(
            IStudentRepository studentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public IEnumerable<Student> GetAllFromStudentRepository()
        {
            return _studentRepository.GetAllStudent();
        }

        public bool Add(Student entity)
        {
            _studentRepository.Add (entity);
            _unitOfWork.Commit();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _studentRepository.Find(id);
            _studentRepository.Delete (entity);

            //_unitOfWork.Commit();
            return true;
        }

        public bool Update(int id, Student entity)
        {
            var newEntity = _studentRepository.Find(id);
            newEntity.FirstName = entity.FirstName;
            newEntity.LastName = entity.LastName;
            _studentRepository.Update (newEntity);

            //_unitOfWork.Commit();
            return true;
        }
    }
}
