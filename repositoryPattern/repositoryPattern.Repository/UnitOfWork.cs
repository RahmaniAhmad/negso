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

        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
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
