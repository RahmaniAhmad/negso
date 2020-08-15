using System.Collections.Generic;
using repositoryPattern.Entities;

public interface IBaseRepository<T> where T : class
{
    T Find(int id);

    IEnumerable<T> GetAll();

    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);
}
