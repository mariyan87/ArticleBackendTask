using System.Collections.Generic;
using Model.Entities;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Provides methods for database access to api tokens.
    /// </summary>
    public interface ITokenRepository : IBaseRepository
    {
        /// <summary>
        /// Finds API token by the token string.
        /// </summary>
        /// <param name="token">The token string.</param>
        /// <returns>The ApiToken or null</returns>
        ApiToken FindByTokenString(string token);

        /// <summary>
        /// Gets all expired tokens.
        /// </summary>
        /// <returns>The list of expired tokens.</returns>
        List<ApiToken> GetExpiredTokens();
    }
}
