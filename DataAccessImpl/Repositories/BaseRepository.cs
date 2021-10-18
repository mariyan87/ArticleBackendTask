using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common;
using Common.Search;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace DataAccessImpl.Repositories
{
    /// <summary>
    /// To be extended by all repositories. 
    /// Contains some usefull methods that can be reused.
    /// </summary>
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// The database model context used.
        /// </summary>
        protected IDbContext Context { get; set; }

        /// <summary>
        /// Constructs BaseRepository with database model context.
        /// </summary>
        /// <param name="context">The database model context used.</param>
        public BaseRepository(IDbContext context)
        {
            Context = context;
        }

        /// <summary>
		/// Gets all the entities.
		/// </summary>
		/// <returns><see cref="IQueryable{T}"/> object.</returns>
		protected IQueryable<T> GetEntities<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            IQueryable<T> query = Context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }

        /// <inheritdoc/>
		public List<T> FindAll<T>(Expression<Func<T, bool>> predicate = null) where T : Entity
        {
            IQueryable<T> query = Context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        /// <inheritdoc/>
        public T FindById<T>(long id) where T : Entity
        {
            return Context.Set<T>().SingleOrDefault(t => t.Id == id);
        }

        /// <inheritdoc/>
        public async Task<T> FindByIdAsync<T>(long id) where T : Entity
        {
            return await Context.Set<T>().SingleOrDefaultAsync(t => t.Id == id);
        }

        /// <inheritdoc/>
        public IQueryable<T> OrderAndPaginate<T>(IQueryable<T> searchQuery, List<SearchOrderDto> order, int pageNumber, int itemsPerPage) where T : Entity
        {
            searchQuery = searchQuery.OrderBy(order);

            if (pageNumber <= 0)
            {
                throw new ArgumentException("PageNumber must be greater than zero");
            }

            if (itemsPerPage <= 0)
            {
                throw new ArgumentException("ItemsPerPage must be greater than zero");
            }

            var filteredItems = searchQuery
              .Skip((pageNumber - 1) * itemsPerPage)
              .Take(itemsPerPage);

            return filteredItems;
        }
    }
}

