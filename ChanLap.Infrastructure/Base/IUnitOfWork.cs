using System.Collections.Generic;
using System.Data.Entity;

namespace ChanLap.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        DbSet<T> CreateSet<T>() where T : class;
        IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters);
        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
