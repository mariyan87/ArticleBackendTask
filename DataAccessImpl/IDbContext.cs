using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccessImpl
{
    /// <summary>
    /// IDbContext definition.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// The DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The <see cref="DbSet{TEntity}"/>.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Database object, for more info see: <see cref="DbContext.Database"/>.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Tracks CreatedOn, ModifiedOn fields of all entities prior calling SaveChanges of the base DbContext.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        /// Tracks CreatedOn, ModifiedOn fields of all entities prior calling SaveChanges of the base DbContext asynchronous.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a <see cref="DbEntityEntry"/> object for the given entity, for more info see: <see cref="DbContext.Entry(object)"/>.
        /// </summary>
        /// <param name="entity">The Given entity object.</param>
        /// <returns>The <see cref="DbEntityEntry"/>.</returns>
        EntityEntry Entry(object entity);

        /// <summary>
        /// Gets a <see cref="DbEntityEntry"/> object for the given entity and type, for more info see: <see cref="DbContext.Entry{TEntity}(TEntity)"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The current Entity.</param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
