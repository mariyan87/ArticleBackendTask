using System;
using System.Linq;
using System.Threading;
using AccessControl;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;

namespace WebApi.Extensions
{
    /// <summary>
    /// Authorization attribute used to limit access to API end points.
    /// </summary>
    public class AuthorizeApiAttribute : Attribute, IAuthorizationFilter
    {
        private Activity[] Activities { get; }

        /// <summary>
        /// Instantiates the instance of the attribute with configuration.
        /// </summary>
        /// <param name="activities">The list of activitie that allow access to this API.</param>
        public AuthorizeApiAttribute(params Activity[] activities)
        {
            Activities = activities;
        }

        /// <summary>
        /// Checks basic authentication request
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            var authorizeService = (IAuthorizeService)filterContext.HttpContext.RequestServices.GetService(typeof(IAuthorizeService));

            if (!IsAuthorized(authorizeService))
            {
                throw new UnauthorizedAccessException();
            }
        }

        /// <summary>
        /// Indicates whether the specified control is authorized.
        /// </summary>
        /// <returns> true if the control is authorized; otherwise, false.</returns>
        /// <param name="authorizeService">The  <see cref="IAuthorizeService"/> implementation.</param>
        private bool IsAuthorized(IAuthorizeService authorizeService)
        {
            if (Thread.CurrentPrincipal is not CurrentPrincipal principal || principal.User == null)
            {
                //not logged in
                return false;
            }

            return true; // TODO: check if user has any of the Activities
        }

    }
}
