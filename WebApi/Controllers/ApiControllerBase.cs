using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Model;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    /// <summary>
    /// The base for all API controllers. Defines some usefull methods.
    /// </summary>
    [ApiController]
    [AuthenticateApi]
    public class ApiControllerBase : ControllerBase
    {
        //private readonly ILogger _logger;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApiControllerBase()
        {
            // todo: init logger
        }

        /// <summary>
        /// The CurrentPrincipal.
        /// </summary>
        protected CurrentPrincipal CurrentPrincipal => (CurrentPrincipal)Thread.CurrentPrincipal;

    }
}
