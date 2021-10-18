namespace AccessControl.Dto
{
    /// <summary>
    /// Represents the user profile for logged in user.
    /// </summary>
    public class LoggedInUserProfileDto
    {
        /// <summary>
        /// Id of the user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The list of user permissions.
        /// </summary>
        //public List<obj> Permissions { get; set; } TODO
    }
}
