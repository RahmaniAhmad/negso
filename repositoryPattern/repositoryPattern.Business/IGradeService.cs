using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using repositoryPattern.Entities;

namespace repositoryPattern.Business
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetAll();
        Grade Find(int id);
        bool Add(Grade entity);
        bool Update(int id, Grade entity);
        bool Delete(int id);
    }
}
