using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;

namespace DataAccess
{
    /// <summary>
    /// Hides the implementation details of the Unit of Work design pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Adds the given entity to the context underlying the set in the Added state such that it will
        /// be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The entity.</returns>
        T Add<T>(T entity) where T : Entity;

        /// <summary>
        /// Adds the given entity asynchronous to the context underlying the set in the Added state such that it will
        /// be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The entity.</returns>
        Task<T> AddAsync<T>(T entity) where T : Entity;

        /// <summary>
        /// Adds a list of entities to the context underlying the set in the Added state such that they will
        /// be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The list of entities to add.</param>
        void Add<T>(List<T> entities) where T : Entity;

        /// <summary>
        /// Adds a list of entities to the context asynchronous underlying the set in the Added state such that they will
        /// be inserted into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The list of entities to add.</param>
        Task AddAsync<T>(List<T> entities) where T : Entity;

        /// <summary>
        /// Marks the given entity as Deleted such that it will be deleted from the database when SaveChanges
        /// is called.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete<T>(T entity) where T : Entity;

        /// <summary>
        /// Marks a list of entities as Deleted such that they will be deleted from the database when SaveChanges
        /// is called.
        /// </summary>
        /// <param name="entities">The list of entities to add.</param>
        void Delete<T>(IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// Marks the given entity as Modified such that it will be updated from the database when SaveChanges
        /// is called.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The entity.</returns>
        T Update<T>(T entity) where T : Entity;

        /// <summary>
        /// Updates a list of entities to the context underlying the set in the Modified state such that they will
        /// be updated into the database when SaveChanges is called.
        /// </summary>
        /// <param name="entities">The list of entities to update.</param>
        void Update<T>(IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// Marks the given entity as Unchanged such that it will be NOT updated to the database when SaveChanges
        /// is called.
        /// </summary>
        /// <param name="entity">The entity to mark as unchanged.</param>
        /// <returns>The entity.</returns>
        T MarkUnchanged<T>(T entity) where T : Entity;

        /// <summary>
        /// The object is no longer tracked by the context.
        /// </summary>
        /// <param name="entity">The entity to detach.</param>
        /// <returns>The entity.</returns>
        T Detach<T>(T entity) where T : Entity;

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes made in this context to the underlying database asynchronous.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Starts a transaction.
        /// </summary>
        /// <returns>A transaction tracker to be used with commit and rollback methods.</returns>
        TransactionTracker BeginTransaction();

        /// <summary>
        /// Starts a transaction asynchronous.
        /// </summary>
        /// <returns>A transaction tracker to be used with commit and rollback methods.</returns>
        Task<TransactionTracker> BeginTransactionAsync();

        /// <summary>
        /// Commits the transaction to the database.
        /// </summary>
        /// <param name="transactionTracker">The transaction tracker returned from the BeginTransaction method.</param>
        void CommitTransaction(TransactionTracker transactionTracker);

        /// <summary>
        /// Commits the transaction to the database asynchronous.
        /// </summary>
        /// <param name="transactionTracker">The transaction tracker returned from the BeginTransaction method.</param>
        Task CommitTransactionAsync(TransactionTracker transactionTracker);

        /// <summary>
        /// Rollbacks the transaction to the database.
        /// </summary>
        /// <param name="transactionTracker">The transaction tracker returned from the BeginTransaction method.</param>
        void RollbackTransaction(TransactionTracker transactionTracker);
    }
}
