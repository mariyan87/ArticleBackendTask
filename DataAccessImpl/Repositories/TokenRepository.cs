using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Repositories;
using Model.Entities;

namespace DataAccessImpl.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ITokenRepository"/>.
    /// </summary>
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        /// <inheritdoc />
        public TokenRepository(IDbContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public ApiToken FindByTokenString(string token)
        {
            return GetEntities<ApiToken>().Where(i => i.Token == token).FirstOrDefault();
        }

        /// <inheritdoc/>
        public List<ApiToken> GetExpiredTokens()
        {
            // TODO: remove expired tokens
            return GetEntities<ApiToken>().Where(i => i.Expiration < DateTime.UtcNow).ToList();
        }
    }
}
