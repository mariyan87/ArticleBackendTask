using AccessControl;
using AccessControlImpl;
using BusinessProcess;
using BusinessProcessImpl;
using DataAccess;
using DataAccess.Repositories;
using DataAccessImpl;
using DataAccessImpl.Repositories;
using Microsoft.Extensions.DependencyInjection;
using TokenServiceImpl;

namespace WebApi
{
    internal class DependencyInjector
    {
        internal static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IDbContext, DatabaseModelContext>();

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IIndividualRepository, IndividualRepository>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}
