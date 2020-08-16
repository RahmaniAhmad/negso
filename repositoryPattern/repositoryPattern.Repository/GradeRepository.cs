using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using repositoryPattern.Entities;
using repositoryPattern.Data;

namespace repositoryPattern.Repository
{
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        private readonly ApplicationContext _dbContext;

        public GradeRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override IEnumerable<Grade> GetAll()
        {
            return _dbContext.Grades.Include(i => i.Courses).ToList();
        }
    }
}
