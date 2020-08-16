using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using repositoryPattern.Entities;
using repositoryPattern.Data;

namespace repositoryPattern.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly ApplicationContext _dbContext;

        public StudentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override IEnumerable<Student> GetAll()
        {
            var dt = new DataTable();
            var connection = new SqlConnection(@"Server=.\;Database=SchoolDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            SqlCommand command = new SqlCommand("select * from students", connection);
            command.CommandType = CommandType.Text;
            command.Connection.Open();
            dt.Load(command.ExecuteReader());
            command.Connection.Close();
            List<Student> students = new List<Student>() { };
            students = ConvertDataTable<Student>(dt);
            return students;

            //return _dbContext.Grades.Include(i => i.Courses).ToList();
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
