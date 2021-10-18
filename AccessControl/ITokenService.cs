using System.Security.Claims;

namespace AccessControl
{
    /// <summary>
    /// Handles token generation and validation.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a cryptographically secure GUID.
        /// </summary>
        /// <returns>The GUID as string.</returns>
        string CreateCryptographicallySecureGuid();

        /// <summary>
        /// Generates API token for a given UserId.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The token string.</returns>
        string GenerateApiToken(long userId);

        /// <summary>
        /// Generates API token for a given ClaimsIdentity.
        /// </summary>
        /// <param name="subject">The ClaimsIdentity.</param>
        /// <returns></returns>
        string GenerateApiToken(ClaimsIdentity subject);

        /// <summary>
        /// Get Api token ClaimsPrincipal.
        /// </summary>
        /// <param name="token">The token to get claims from.</param>
        /// <returns></returns>
        ClaimsPrincipal GetApiTokenClaims(string token);

        /// <summary>
        /// Validates the provided API token.
        /// </summary>
        /// <param name="token">The provided API token.</param>
        /// <returns>The ID of the user in the token if token is valid.</returns>
        long ValidateApiToken(string token);
    }
}
