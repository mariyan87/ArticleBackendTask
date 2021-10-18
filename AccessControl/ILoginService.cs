
using AccessControl.Dto;
using Model.Entities;

namespace AccessControl
{
    /// <summary>
    /// Handles user login.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Handles user logins via the browser. 
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        AuthenticatedUserDto Login(string email, string password);

        /// <summary>
        /// Generates new token for change password.
        /// </summary>
        /// <param name="email">the email of user</param>
        /// <param name="individualName">Returns full name of user.</param>
        /// <returns>Change password token.</returns>
        ApiToken CreateResetPasswordToken(string email);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <param name="apiToken">The api token of the current session.</param>
        void Logout(ApiToken apiToken);

        /// <summary>
        /// Gets the user profile of the logged in user.
        /// </summary>
        /// <param name="individual">The current user.</param>
        /// <param name="fundingPeriodId">The funding period to get permissions for.</param>
        /// <param name="OriginUserId">The origin loged user id.</param>
        /// <returns>The user profile.</returns>
        LoggedInUserProfileDto PrepareUserProfile(Individual individual);
    }
}
