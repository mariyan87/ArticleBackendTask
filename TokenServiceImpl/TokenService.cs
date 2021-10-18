using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using AccessControl;
using Common;
using Microsoft.IdentityModel.Tokens;

namespace TokenServiceImpl
{
    /// <summary>
    /// Implementation of <see cref="ITokenService"/>
    /// </summary>
    public class TokenService : BaseService, ITokenService
    {
        private const double EXPIRE_HOURS = 1.0;
        private const string TOKEN_SECRET = "7Xr9nywfatly8q83M1mflytDPBmb79h/tEN2E/KNrYR2a1Tyb4aD4T+4tS18WayR5R9RmzmC5GyouhdV4GAugA==";
        private static readonly SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Convert.FromBase64String(TOKEN_SECRET));

        /// <summary>
        /// Default constructor.
        public TokenService()
        {
        }

        /// <inheritdoc/>
        public string GenerateApiToken(long userId)
        {
            var subject = new ClaimsIdentity(new[] {
                        new Claim("UserId", userId.ToString()),
                    });
            return GenerateApiToken(subject);
        }

        /// <inheritdoc/>
        public string GenerateApiToken(ClaimsIdentity subject)
        {
            var now = DateTime.UtcNow;

            subject.AddClaim(new Claim("_id", CreateCryptographicallySecureGuid()));//random id to ensure tokens are unique

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = now,
                NotBefore = now,
                Expires = now.AddHours(EXPIRE_HOURS),
                Subject = subject,
                SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }

        /// <inheritdoc/>
        public ClaimsPrincipal GetApiTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new MissingFieldException("Token cannot be empty.");
            }

            return GetPrincipal(token);
        }

        /// <inheritdoc/>
        public long ValidateApiToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new MissingFieldException("Token cannot be empty.");
            }

            var principal = GetPrincipal(token);
            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                throw new InvalidOperationException("UserId claim is missing.");
            }

            return Convert.ToInt64(userIdClaim.Value);
        }

        /// <inheritdoc/>
        public string CreateCryptographicallySecureGuid()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                provider.GetBytes(bytes);

                return new Guid(bytes).ToString();
            }
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = symmetricKey
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

                return principal;
            }

            catch (Exception e)
            {
                throw new InvalidOperationException("Cannot parse token", e);
            }
        }
    }
}
