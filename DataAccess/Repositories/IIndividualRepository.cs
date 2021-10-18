using Model.Entities;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Provides methods for database access to individuals.
    /// </summary>
    public interface IIndividualRepository : IBaseRepository
    {
        /// <summary>
        /// Find individual by the email.
        /// </summary>
        /// <param name="email">The email to search by.</param>
        /// <returns>The individual.</returns>
        Individual FindByEmail(string email);

    }
}
