using System.Linq;
using DataAccess.Repositories;
using Model.Entities;

namespace DataAccessImpl.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IIndividualRepository"/>.
    /// </summary>
    public class IndividualRepository : BaseRepository, IIndividualRepository
    {
        /// <inheritdoc />
        public IndividualRepository(IDbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public Individual FindByEmail(string email)
        {
            return GetEntities<Individual>().Where(i => i.Email == email).FirstOrDefault();
        }
    }
}
