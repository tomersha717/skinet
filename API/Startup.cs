using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;            
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(_config.GetConnectionString("IdentityConnection")));

            // services.AddSingleton<ConnectionMultiplexer>(c => {
            //     var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
            //     return ConnectionMultiplexer.Connect(configuration);
            // });

            services.AddApplicationServices(); //our custom extension
            services.AddIdentityServices(_config); //our custom extension
            services.AddSwaggerDocumentation(); //our custom extension
            services.AddCors(options => {
                options.AddPolicy("CorePolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseMiddleware<ExeptionMiddleware>();


            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorePolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerDocumentation(); //our custom extension

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
