using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using repositoryPattern.Data;
using repositoryPattern.Entities;

namespace repositoryPattern.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        private IStudentRepository _studentRepository;
        private IGradeRepository _gradeRepository;
        private IUserRepository _userRepository;
        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IStudentRepository StudentRepository
        {
            get { return _studentRepository = _studentRepository ?? new StudentRepository(_dbContext); }
        }
        public IGradeRepository GradeRepository
        {
            get { return _gradeRepository = _gradeRepository ?? new GradeRepository(_dbContext); }
        }
          public IUserRepository UserRepository
        {
            get { return _userRepository = _userRepository ?? new UserRepository(_dbContext); }
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
