using System;
using System.Collections.Generic;
using System.Linq;
using AccessControl;
using Common;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Model.Entities;
using Model.Enums;

namespace AccessControlImpl
{
    /// <summary>
    /// Implementation of <see cref="IAuthorizeService"/>.
    /// </summary>
    public class AuthorizeService : BaseService, IAuthorizeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenRepository tokenRepository;
        private readonly ITokenService tokenService;
        private readonly ILoginService loginService;
        private readonly IIndividualRepository individualRepository;
        private readonly double ApiTokenExpirationMinutes = 60;

        /// <summary>
        /// Instantiates a new instance of the service.
        /// </summary>
        /// <param name="unitOfWork">The unit of work implementation.</param>
        /// <param name="tokenRepository">The token repository implementation.</param>
        /// <param name="tokenService">The token service implementaion.</param>
        /// <param name="loginService">The login service implementaion.</param>
        /// <param name="individualRepository">The individual repository implementaion.</param>
        public AuthorizeService(
            IUnitOfWork unitOfWork,
            ITokenRepository tokenRepository,
            ITokenService tokenService,
            ILoginService loginService,
            IIndividualRepository individualRepository)
        {
            this.unitOfWork = unitOfWork;
            this.tokenRepository = tokenRepository;
            this.tokenService = tokenService;
            this.loginService = loginService;
            this.individualRepository = individualRepository;
        }

        /// <inheritdoc/>
        public Individual ValidateRequestHeaders(IHeaderDictionary headers, out ApiToken apiToken)
        {
            var apiTokenString = ParseAuthorizationHeader(headers);
            apiToken = FindValidToken(apiTokenString);

            if (apiToken.TokenType != TokenType.Login)
            {
                throw new FormatException("Provided Api token is not with correct Login type.");
            }

            long userId;
            try
            {
                userId = tokenService.ValidateApiToken(apiToken.Token);
            }
            catch
            {
                throw new InvalidOperationException("Invalid API token.");
            }

            if (userId != apiToken.UserId)
            {
                throw new UnauthorizedAccessException("UserId in the API token does not match.");
            }

            return individualRepository.FindById<Individual>(apiToken.UserId);
        }

        /// <inheritdoc/>
        public ApiToken FindValidToken(string apiTokenString)
        {
            var apiToken = FindToken(apiTokenString);

            if (DateTime.UtcNow >= apiToken.Expiration)
            {
                throw new InvalidOperationException("Expired API token");
            }

            return apiToken;
        }

        /// <inheritdoc/>
        public ApiToken FindToken(string apiTokenString)
        {
            var apiToken = tokenRepository.FindByTokenString(apiTokenString);
            if (apiToken == null)
            {
                throw new InvalidOperationException("API token not in the database.");
            }

            return apiToken;
        }

        /// <inheritdoc/>
        public void ExtendTokenExpiration(ApiToken apiToken)
        {
            apiToken.Expiration = DateTime.UtcNow.AddMinutes(ApiTokenExpirationMinutes);
            unitOfWork.Update(apiToken);
            unitOfWork.SaveChanges();
        }

        private string ParseAuthorizationHeader(IHeaderDictionary headers)
        {
            if (!headers.ContainsKey("Authorization"))
            {
                throw new ArgumentException("Authorization header is missing");
            }

            headers.TryGetValue("Authorization", out var values);

            var authHeader = values.FirstOrDefault();
            if (!authHeader.StartsWith("Bearer"))
            {
                throw new ArgumentException("Bearer is missing");
            }

            var headerParts = authHeader.Split(' ');
            if (headerParts.Length != 2)
            {
                throw new InvalidOperationException("Invalid Authorization header");
            }

            return headerParts[1];
        }

        /// <inheritdoc/>
        public void RemoveToken(ApiToken apiToken)
        {
            unitOfWork.Delete(apiToken);
            unitOfWork.SaveChanges();
        }

        /// <inheritdoc/>
        public List<object> GetAllUserPermissions(long userId)
        {
            throw new NotImplementedException();// TODO
        }
    }
}
