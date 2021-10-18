
using Model.Entities;

namespace AccessControl.Dto
{
    /// <summary>
    /// Holds the result from user login action - user and token.
    /// </summary>
    public class AuthenticatedUserDto
    {
        /// <summary>
        /// The user that is logged in.
        /// </summary>
        public Individual Individual { get; set; }

        /// <summary>
        /// The API token generated for this login.
        /// </summary>
        public ApiToken ApiToken { get; set; }

    }
}
