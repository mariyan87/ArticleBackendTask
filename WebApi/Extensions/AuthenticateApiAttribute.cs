using System;
using System.Linq;
using System.Threading;
using AccessControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;

namespace WebApi.Extensions
{
    /// <summary>
    /// Authentication attribute used to validate the API token and limit access to the API end points.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthenticateApiAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Instantiates the instance of the attribute with configuration.
        /// </summary>
        public AuthenticateApiAttribute()
        {
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

            var individual = authorizeService.ValidateRequestHeaders(filterContext.HttpContext.Request.Headers, out var apiToken);

            var principal = new CurrentPrincipal(individual, apiToken);

            Thread.CurrentPrincipal = principal;

            if (!string.IsNullOrEmpty(apiToken.Cookie))
            {
                authorizeService.ExtendTokenExpiration(apiToken);
            }
        }
    }
}
