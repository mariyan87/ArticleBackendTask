using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Model.Entities;

namespace AccessControl
{
    /// <summary>
    /// Handles user authentication and autherization.
    /// </summary>
    public interface IAuthorizeService
    {
        /// <summary>
        /// Validates that the request headers contain valid API token.
        /// </summary>
        /// <param name="headers">The http request headers.</param>
        /// <param name="apiToken">Reruns the API token found in the headers.</param>
        /// <returns>The logged in user.</returns>
        Individual ValidateRequestHeaders(IHeaderDictionary headers, out ApiToken apiToken);

        /// <summary>
        /// Searches valid token by token string.
        /// </summary>
        /// <param name="apiTokenString">The token as string.</param>
        /// <returns>the <see cref="ApiToken"/></returns>
        ApiToken FindValidToken(string apiTokenString);

        /// <summary>
        /// Searches token by token string.
        /// </summary>
        /// <param name="apiTokenString">The token as string.</param>
        /// <returns>the <see cref="ApiToken"/></returns>
        ApiToken FindToken(string apiTokenString);

        /// <summary>
        /// Removes token by token string.
        /// </summary>
        /// <param name="apiToken">The token.</param>
        void RemoveToken(ApiToken apiToken);

        /// <summary>
        /// Extends the expiration time of a token.
        /// </summary>
        /// <param name="apiToken">The api token to extend.</param>
        void ExtendTokenExpiration(ApiToken apiToken);

        /// <summary>
        /// Get all permissions of a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The list of permissions.</returns>
        List<object> GetAllUserPermissions(long userId);
    }
}
