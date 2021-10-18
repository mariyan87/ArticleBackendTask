namespace WebApi.Dto
{
    /// <summary>
    /// Represents a login request from the web UI.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// The e-mail of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
