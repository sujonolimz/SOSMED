using SOSMED_API.Helpers;
using SOSMED_API.Interface;
using SOSMED_API.Services;

namespace SOSMED_API.Configuration
{
    public static class ServiceConfiguration
    {
        public static void UseAvailableServices(this IServiceCollection services)
        {
            //Add Sql Server helper
            services.AddSingleton<SqlServerConnector> ();

            //Add Form Service
            services.AddScoped<IForm, FormService>();

            //Add Group Service
            services.AddScoped<IGroup, GroupService>();

            //Add GroupAccess Service
            services.AddScoped<IGroupAccess, GroupAccessService>();

            //Add User Service
            services.AddScoped<IUser, UserService>();

            //Add PostLimit Service
            services.AddScoped<IPostLimit, PostLimitService>();

            //Add Posting Service
            services.AddScoped<IPosting, PostingService>();

            //Add Auth Service
            services.AddScoped<IAuth, AuthService>();

            //Add Token Service 
            services.AddSingleton<TokenService>();

            //Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

        }
    }
}
