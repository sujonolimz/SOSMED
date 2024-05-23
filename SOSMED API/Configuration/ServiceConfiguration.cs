using SOSMED_API.Helpers;
using SOSMED_API.Services;

namespace SOSMED_API.Configuration
{
    public class ServiceConfiguration
    {
        public static void configure(IServiceCollection services)
        {
            //Add Sql Server helper
            services.AddSingleton<SqlServerConnector> ();

            //Add Form Service
            services.AddScoped<IFormService, FormService>();

            //Add Group Service
            services.AddScoped<IGroupService, GroupService>();

            //Add GroupAccess Service
            services.AddScoped<IGroupAccessService, GroupAccessService>();

            //Add User Service
            services.AddScoped<IUserService, UserService>();

            //Add PostLimit Service
            services.AddScoped<IPostLimitService, PostLimitService>();

            //Add PostLimit Service
            services.AddScoped<IPostingService, PostingService>();
        }
    }
}
