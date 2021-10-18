using System;
using System.Security.Principal;
using Common;
using Model.Entities;

namespace Model
{
    /// <summary>
    /// Defines a custom principal.
    /// </summary>
    public class CurrentPrincipal : IPrincipal, ICurrentPrincipal
    {
        /// <summary>
        /// Current user.
        /// </summary>
        public Individual User { get; }

        /// <summary>
        /// Current API token used for communication with the system.
        /// </summary>
        public ApiToken ApiToken { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentPrincipal"/> class, specifying the user and the token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="apiToken">The api token.</param>
        public CurrentPrincipal(Individual user, ApiToken apiToken)
        {
            User = user;
            ApiToken = apiToken;
            Identity = new GenericIdentity(user.Email);
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public IIdentity Identity { get; private set; }

        /// <inheritdoc/>
        public long? UserId => User?.Id;

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        //public List<obj> Permissions { get; private set; } TODO: add  list of permissions of the current user.
    }
}
