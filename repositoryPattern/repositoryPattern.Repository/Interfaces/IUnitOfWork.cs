using System;
using Microsoft.EntityFrameworkCore;
using repositoryPattern.Data;
using repositoryPattern.Data.Configurations;
using repositoryPattern.Entities;

namespace repositoryPattern.Repository
{
    public interface IUnitOfWork : IDisposable
    {

        IStudentRepository StudentRepository { get; }
        void Commit();

        void Dispose();
    }
}
