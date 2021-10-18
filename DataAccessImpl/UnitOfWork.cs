using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Model.Entities;

namespace DataAccessImpl
{
    /// <summary>
    /// Implemenation of <see cref="IUnitOfWork"/>.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        protected ILogger logger;
        private readonly IDbContext dbContext;
        private IDbContextTransaction currentDbContextTransaction;

        /// <summary>
        /// Constructs UnitOfWork with database model context.
        /// </summary>
        /// <param name="context">The database model context used.</param>
        public UnitOfWork(IDbContext context, IServiceProvider serviceProvider)
        {
            dbContext = context;
            logger = ((ILoggerFactory)serviceProvider.GetService(typeof(ILoggerFactory)))
               .CreateLogger(typeof(DatabaseModelContext));
        }

        /// <inheritdoc />
        public T Add<T>(T entity) where T : Entity
        {
            dbContext.Set<T>().Add(entity);
            return entity;
        }

        /// <inheritdoc />
        public async Task<T> AddAsync<T>(T entity) where T : Entity
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        /// <inheritdoc />
        public void Add<T>(List<T> entities) where T : Entity
        {
            dbContext.Set<T>().AddRange(entities);
        }

        /// <inheritdoc />
        public async Task AddAsync<T>(List<T> entities) where T : Entity
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public TransactionTracker BeginTransaction()
        {
            if (currentDbContextTransaction == null)
            {
                currentDbContextTransaction = dbContext.Database.BeginTransaction();
                return new TransactionTracker(this, true);
            }
            return new TransactionTracker(this, false);
        }

        /// <inheritdoc />
        public async Task<TransactionTracker> BeginTransactionAsync()
        {
            currentDbContextTransaction = await dbContext.Database.BeginTransactionAsync();
            return new TransactionTracker(this, true);
        }

        /// <inheritdoc />
        public void CommitTransaction(TransactionTracker transactionTracker)
        {
            if (transactionTracker.IsLocal)
            {
                var dbContextTransaction = currentDbContextTransaction;
                currentDbContextTransaction = null;
                dbContextTransaction.Commit();
            }
        }

        /// <inheritdoc />
        public async Task CommitTransactionAsync(TransactionTracker transactionTracker)
        {
            if (transactionTracker.IsLocal)
            {
                var dbContextTransaction = currentDbContextTransaction;
                currentDbContextTransaction = null;

                await dbContextTransaction.CommitAsync();
            }
        }

        /// <inheritdoc />
        public void Delete<T>(IEnumerable<T> entities) where T : Entity
        {
            foreach (var entity in entities.ToArray())
            {
                Delete(entity);
            }
        }

        /// <inheritdoc />
        public void Delete<T>(T entity) where T : Entity
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <inheritdoc />
        public T Detach<T>(T entity) where T : Entity
        {
            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Detached;

            return entity;
        }

        /// <inheritdoc />
        public T MarkUnchanged<T>(T entity) where T : Entity
        {
            var entry = dbContext.Entry(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            return entity;
        }

        /// <inheritdoc />
        public void RollbackTransaction(TransactionTracker transactionTracker)
        {
            if (transactionTracker.IsLocal)
            {
                if (currentDbContextTransaction != null)// && currentDbContextTransaction.UnderlyingTransaction != null && currentDbContextTransaction.UnderlyingTransaction.Connection != null)
                {
                    var dbContextTransaction = currentDbContextTransaction;
                    currentDbContextTransaction = null;
                    dbContextTransaction.Rollback();
                }

            }
        }

        /// <inheritdoc />
        public void SaveChanges()
        {
            var beforeCommit = DateTime.UtcNow;
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, string.Format("An error occured during the database save operation started at {0:MM/dd/yy H:mm:ss} UTC.", beforeCommit));

                throw new Exception(string.Format("An error occured during the database save operation started at {0:MM/dd/yy H:mm:ss} UTC.", beforeCommit), ex);
            }
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            var beforeCommit = DateTime.UtcNow;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, string.Format("An error occured during the database save operation started at {0:MM/dd/yy H:mm:ss} UTC.", beforeCommit));

                throw new Exception(string.Format("An error occured during the database save operation started at {0:MM/dd/yy H:mm:ss} UTC.", beforeCommit), ex);
            }
        }

        /// <inheritdoc />
        public T Update<T>(T entity) where T : Entity
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <inheritdoc />
        public void Update<T>(IEnumerable<T> entities) where T : Entity
        {
            foreach (var entity in entities.ToArray())
            {
                Update(entity);
            }
        }
    }
}
