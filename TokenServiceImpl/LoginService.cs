using System;
using System.Security.Authentication;
using AccessControl;
using AccessControl.Dto;
using Common;
using DataAccess;
using DataAccess.Repositories;
using Model.Entities;
using Model.Enums;

namespace AccessControlImpl
{
    /// <summary>
    /// Implementation of <see cref="ILoginService"/>
    /// </summary>
    public class LoginService : BaseService, ILoginService
    {
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IIndividualRepository individualRepository;

        /// <summary>
        /// Instantiates a new instance of the service.
        /// </summary>
        /// <param name="tokenService">The <see cref="ITokenService"/> implementation.</param>
        /// <param name="tokenService">The <see cref="IIndividualRepository"/> implementation.</param>
        public LoginService(IUnitOfWork unitOfWork, ITokenService tokenService, IIndividualRepository individualRepository)
        {
            this.tokenService = tokenService;
            this.individualRepository = individualRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public ApiToken CreateResetPasswordToken(string email)
        {
            var individual = individualRepository.FindByEmail(email);
            if (individual == null)
            {
                return null;
            }

            var token = tokenService.GenerateApiToken(individual.Id);

            var apiToken = new ApiToken
            {
                Cookie = null,
                Token = token,
                UserId = individual.Id,
                Expiration = DateTime.UtcNow.AddMinutes(60),
                TokenType = TokenType.ResetPassword
            };

            unitOfWork.Add(apiToken);
            unitOfWork.SaveChanges();

            return apiToken;
        }


        /// <inheritdoc />
        public AuthenticatedUserDto Login(string email, string password)
        {

            var individual = individualRepository.FindByEmail(email);
            if (individual == null)
            {
                throw new AuthenticationException("No such user");
            }

            // TODO: validate password

            var token = tokenService.GenerateApiToken(individual.Id);

            var apiToken = new ApiToken
            {
                Cookie = tokenService.CreateCryptographicallySecureGuid(),
                Token = token,
                UserId = individual.Id,
                Expiration = DateTime.UtcNow.AddMinutes(60),
                TokenType = TokenType.Login
            };

            unitOfWork.Add(apiToken);
            unitOfWork.SaveChanges();

            return new AuthenticatedUserDto
            {
                Individual = individual,
                ApiToken = apiToken,
            };
        }

        /// <inheritdoc />
        public void Logout(ApiToken apiToken)
        {
            unitOfWork.Delete(apiToken);
            unitOfWork.SaveChanges();
        }

        public LoggedInUserProfileDto PrepareUserProfile(Individual individual)
        {
            var profile = new LoggedInUserProfileDto()
            {
                Id = individual.Id,
                Email = individual.Email
            };

            return profile;
        }
    }
}
