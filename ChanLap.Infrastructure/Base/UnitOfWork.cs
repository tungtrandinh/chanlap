using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ChanLap.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IDatabaseFactory databaseFactory;
        public ChanLapEntity dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public ChanLapEntity DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }

        public DbSet<T> CreateSet<T>()
           where T : class
        {
            return DataContext.Set<T>();
        }

        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return DataContext.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return DataContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }
    }
}
