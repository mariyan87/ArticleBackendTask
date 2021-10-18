using System;
using System.Threading;

namespace Common
{
    /// <summary>
    /// Base Service with common logic that all services will follow.
    /// </summary>
    public class BaseService
    {
        //TODO add logger

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseService()
        {

        }

        /// <summary>
        /// Current Principal
        /// </summary>
        protected ICurrentPrincipal CurrentPrincipal
        {

            private set { }

            get
            {
                if (Thread.CurrentPrincipal is ICurrentPrincipal principal)
                {
                    return principal;
                }
                else
                {
                    throw new InvalidOperationException($"Cannot get current principal.");
                }
            }

        }
    }
}
