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
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GradeService(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Grade> GetAll()
        {
            // ApplicationContext context = new ApplicationContext();
            // return context.Grades.Include(i => i.Courses).ToList();
            return _unitOfWork.GradeRepository.GetAll();
        }
        public Grade Find(int id)
        {
            return _unitOfWork.GradeRepository.Find(id);
        }

        public bool Add(Grade entity)
        {
            _unitOfWork.GradeRepository.Add(entity);
            _unitOfWork.Commit();
            return true;
        }

        public bool Delete(int id)
        {
            var entity = _unitOfWork.GradeRepository.Find(id);
            _unitOfWork.GradeRepository.Delete(entity);
            _unitOfWork.Commit();
            return true;
        }

        public bool Update(int id, Grade entity)
        {
            var newEntity = _unitOfWork.GradeRepository.Find(id);
            newEntity.Title = entity.Title;
            _unitOfWork.GradeRepository.Update(newEntity);
            return true;
        }
    }
}
