
namespace Common
{
    /// <summary>
    /// Represents the current logged in user.
    /// </summary>
    public interface ICurrentPrincipal
    {
        /// <summary>
        /// Id of the current authenticated user.
        /// </summary>
        long? UserId { get; }

        // TODO: add list of permissions of the current user.

    }
}
