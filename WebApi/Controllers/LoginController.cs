using AccessControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers.Rest
{
    /// <summary>
    /// Handles login and logout requests.
    /// </summary>
    [Route("api/user")]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginService loginService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loginService">The <see cref="ILoginService"/> implementaion.</param>
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        /// <summary>
        /// Login user. Generates an api token.
        /// </summary>
        /// <param name="dto">Login request parameters</param>
        /// <returns>Logged in user info and access token.</returns>
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ResponseDto<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var authenticatedUserDto = loginService.Login(dto.Email, dto.Password);

            var responseMessage = new ResponseDto<LoginResponseDto>(new LoginResponseDto
            {
                ApiTokenExirationMinutes = 60, // TODO: extract in config
                ApiToken = authenticatedUserDto.ApiToken.Token,
                UserProfile = loginService.PrepareUserProfile(authenticatedUserDto.Individual),
            });
            return responseMessage;
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>OK</returns>
        [Route("logout")]
        [HttpPost]
        public SimpleResponseDto Logout()
        {
            loginService.Logout(CurrentPrincipal.ApiToken);

            return new SimpleResponseDto();
        }
    }
}
