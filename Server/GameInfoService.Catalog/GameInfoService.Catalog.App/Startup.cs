using GameInfoService.Catalog.App.HostedServices;
using GameInfoService.Catalog.Domain.RepositoryInterfaces;
using GameInfoService.Catalog.Infrastructure.MappingInterfaces;
using GameInfoService.Catalog.Infrastructure.Repositories;
using GameInfoService.Catalog.Infrastructure.Repositories.Contexts;
using GameInfoService.Catalog.Services;
using GameInfoService.Catalog.Services.GameInfoRatingCommunicationServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GameInfoService.Catalog.App
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

            var gameInfoConnectionString = Configuration.GetConnectionString("GameInfoConnectionString");

            services.AddScoped<IGameInfoMapper, GameInfoMapper>();

            services.AddDbContext<GameInfoContext>(options => options.UseSqlServer(gameInfoConnectionString));

            services.AddTransient<IGameInfoRepository, GameInfoRepository>();

            services.AddTransient<IGameInfoRetrieveService, GameInfoRetrieveService>();

            services.AddTransient<IGameInfoRatingUpdatedCommunicationService, GameInfoRatingUpdatedCommunicationService>();

            services.AddHostedService<GameInfoRatingUpdatedCommunicationServiceHostedService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameInfoService.Catalog", Version = "v1" });
            });

            services.AddAuthentication("BearerToken")
                .AddJwtBearer("BearerToken", options =>
                {
                    options.Authority = "https://localhost:5000";

                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidTypes = new[] { "at+jwt" }
                };
                });
            //.AddOAuth2Introspection("IntrospectionBearerToken", options =>
            //{
            //    options.Authority = "https://localhost:5001";

            //});

            //var rabbitMqConfiguration = Configuration.GetSection("RabbitMQ");
            //services.Configure<RabbitMqConfig>(rabbitMqConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
