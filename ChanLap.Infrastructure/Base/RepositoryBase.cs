using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using ChanLap.Model;
using System.Data.Entity.Infrastructure;
using ChanLap.Common.Logging;
using ChanLap.Resource;
using ChanLap.Infrastructure.Specification.Contract;

namespace ChanLap.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        public ChanLapEntity dataContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDbSet<T> dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public ChanLapEntity DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        public DbSet<T> CreateSet()
        {
            return DataContext.Set<T>();
        }

        public virtual void Insert(T entity)
        {
            if (entity != (T)null)
                dbset.Add(entity); // add new item in this set
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot add null entity into {0} repository", typeof(T).ToString()));

            }
        }

        public virtual void InsertAndSubmit(T entity)
        {
            if (entity != (T)null)
            {
                dbset.Add(entity);// add new item in this set
                this.SaveChanges();
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot add null entity into {0} repository", typeof(T).ToString()));

            }
        }

        public virtual void Update(T entity)
        {
            if (entity != (T)null)
            {
                dbset.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                LoggerFactory.CreateLog()
                            .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }
        
        public virtual void Update(T entity, bool updateRootEntity)
        {
            if (entity != (T)null)
            {
                dbset.Attach(entity);
                dataContext.Entry(entity).State = updateRootEntity
                                                    ? EntityState.Modified : EntityState.Unchanged;
            }
            else
            {
                LoggerFactory.CreateLog()
                            .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }

        public virtual void UpdateAndSubmit(T entity)
        {
            if (entity != (T)null)
            {
                dbset.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
                this.SaveChanges();
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }

        public virtual void UpdateAndSubmit(T entity, bool updateRootEntity)
        {
            if (entity != (T)null)
            {
                dbset.Attach(entity);
                dataContext.Entry(entity).State = updateRootEntity
                                                    ? EntityState.Modified : EntityState.Unchanged;
                this.SaveChanges();
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }

        public virtual void Delete(T entity)
        {
            if (entity != (T)null)
            {
                //attach item if not exist
                if (dataContext.Entry(entity).State == EntityState.Detached)
                    dbset.Attach(entity);
                dbset.Remove(entity); //set as "removed" 
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbset.Find(id);
            if (entityToDelete != (T)null)
            {
                //attach item if not exist
                if (dataContext.Entry(entityToDelete).State == EntityState.Detached)
                    dbset.Attach(entityToDelete);
                dbset.Remove(entityToDelete); //set as "removed" 
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            if (objects.Count() > 0)
                foreach (T obj in objects)
                    dbset.Remove(obj);
            else
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
        }

        public virtual void DeleteAndSubmit(T entity)
        {
            if (entity != (T)null)
            {
                //attach item if not exist
                if (dataContext.Entry(entity).State == EntityState.Detached)
                    dbset.Attach(entity);
                dbset.Remove(entity); //set as "removed" 
                this.SaveChanges();
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(String.Format("Cannot remove null entity into {0} repository", typeof(T).ToString()));
            }
        }

        public virtual void SoftDeleteAndSubmit(T entity)
        {
            if (typeof(T).GetProperty("Deleted") != null)
            {
                entity.GetType().GetProperty("Deleted").SetValue(entity, DateTime.Now, null);
                this.UpdateAndSubmit(entity);
            }
            else
            {
                throw new InvalidOperationException("This entity type does not support soft deletion. Please add a DateTime? property called Deleted and try again.");
            }
        }

        public virtual void SetModified(T item)
        {
            //this operation also attach item in object state manager
            dataContext.Entry<T>(item).State = EntityState.Modified;
        }
        public virtual void Merge(T original, T current)
        {
            //if it is not attached, attach original and set current values
            dataContext.Entry<T>(original).CurrentValues.SetValues(current);
        }
        public virtual IEnumerable<T> AllMatching(ISpecification<T> specification)
        {
            return dbset.Where(specification.SatisfiedBy());
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual IEnumerable<T> GetMany( Expression<Func<T, bool>> filter = null,
                                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                               string includeProperties = "",
                                               int? skip = null,
                                               int? take = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query.ToList();
        }
        
        public virtual IEnumerable<T> GetPage<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<T, KProperty>> orderByExpression, bool ascending)
        {
            var set = dbset;

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public virtual int ExecuteCommand(string command, params object[] parameters)
        {
            return this.dataContext.Database.ExecuteSqlCommand(command, parameters);
        }

        public virtual IEnumerable<T> ExecuteQuery(string sqlQuery, params object[] parameters)
        {
            return this.dataContext.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public virtual void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            this.dataContext.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public virtual void SaveChanges()
        {
            this.dataContext.SaveChanges();
        }

        public virtual void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    this.dataContext.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }


        #region Private Helpers
        /// <summary>
        /// Returns expression to use in expression trees, like where statements. For example query.Where(GetExpression("IsDeleted", typeof(boolean), false));
        /// </summary>
        /// <param name="propertyName">The name of the property. Either boolean or a nulleable typ</param>
        private Expression<Func<T, bool>> GetExpression(string propertyName, object value)
        {
            var param = Expression.Parameter(typeof(T));
            var actualValueExpression = Expression.Property(param, propertyName);

            var lambda = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(actualValueExpression,
                    Expression.Constant(value)),
                param);

            return lambda;
        }

        protected IQueryable<T> DataSource()
        {
            var query = dataContext.Set<T>().AsQueryable<T>();
            var property = typeof(T).GetProperty("Deleted");

            if (property != null)
            {
                query = query.Where(GetExpression("Deleted", null));
            }

            return query;
        }
        #endregion
    }
}
