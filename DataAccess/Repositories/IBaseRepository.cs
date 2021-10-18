using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Search;
using Model.Entities;

namespace DataAccess.Repositories
{
    /// <summary>
    /// To be extended by all repositories. 
    /// Contains some usefull methods that can be reused.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
		/// Gets all the entities.
		/// </summary>
		/// <returns><see cref="List{T}"/> object.</returns>
		List<T> FindAll<T>(Expression<Func<T, bool>> predicate = null) where T : Entity;

        /// <summary>
		/// Finds Entity by given Id.
		/// </summary>
		/// <returns><see cref="{T}"/> object.</returns>
        T FindById<T>(long id) where T : Entity;

        /// <summary>
        /// Finds Entity by given Id asynchronous.
        /// </summary>
        /// <returns><see cref="{T}"/> object.</returns>
        Task<T> FindByIdAsync<T>(long id) where T : Entity;

        /// <summary>
        /// Order and paginate given Entities in searchQuery parameter.
        /// </summary>        
        /// <returns>The <see cref="IQueryable{T}"/></returns>
        IQueryable<T> OrderAndPaginate<T>(IQueryable<T> searchQuery, List<SearchOrderDto> order, int pageNumber, int itemsPerPage) where T : Entity;
    }
}
