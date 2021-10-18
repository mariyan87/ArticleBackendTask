using AccessControl.Dto;

namespace WebApi.Dto
{
    /// <summary>
    /// Represents a login response.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// The user profile for logged in user.
        /// </summary>
        public LoggedInUserProfileDto UserProfile { get; set; }

        /// <summary>
        /// The API token to be used for communication with the web API.
        /// </summary>
        public string ApiToken { get; set; }

        /// <summary>
        /// The number of minutes to expire the token, i.e. session.
        /// </summary>
        public int ApiTokenExirationMinutes { get; set; }

    }
}
