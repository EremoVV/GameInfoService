using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityService.Models.Authorization;
using IdentityService.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IdentityServer4.AspNetIdentity;
using Microsoft.OpenApi.Models;


namespace IdentityService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("Authorization");
            if (connectionString != null)
            {
                services.AddDbContext<AuthorizationContext>(options => options.UseSqlServer(connectionString));
            }
            else
            {
                throw new NullReferenceException("AuthorizationConnection string is not founded in config file");
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "IdentityService API", Version = "v1"});
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AuthorizationContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddJwtBearerClientAuthentication()
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(Config.ApiResources)
                .AddDeveloperSigningCredential();


            //services.AddAuthentication("token")
            //    .AddJwtBearer("token", options =>
            //    {
            //        options.Authority = "localhost:5000";

            //        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            //    })
            //    .AddOAuth2Introspection("Bearer", options =>
            //    {
            //        options.Authority = "localhost:5000";
            //    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseIdentity;
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityService V1");
                    c.RoutePrefix = string.Empty;
                });
            }

        }
    }
}
