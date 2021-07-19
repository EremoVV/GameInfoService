using System;
using GameInfoService.Rating.App.MappingInterfaces;
using GameInfoService.Rating.Domain.RepositoryInterfaces;
using GameInfoService.Rating.Infrastructure.Context;
using GameInfoService.Rating.Infrastructure.Repositories;
using GameInfoService.Rating.Services;
using GameInfoService.Rating.Services.MappingInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GameInfoService.Rating.App
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

            var connectionString = Configuration.GetConnectionString("RatingConnectionString");
            if (string.IsNullOrEmpty(connectionString)) throw new NullReferenceException("Connection string is empty");

            services.AddTransient<IGameInfoRatingServiceMapper, GameInfoRatingServiceMapper>();

            services.AddTransient<IGameInfoRatingMapper, GameInfoRatingMapper>();

            services.AddDbContext<GameInfoRatingContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IGameInfoRatingRepository, GameInfoRatingRepository>();

            services.AddTransient<IGameInfoRatingService, GameInfoRatingService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameInfoRating.App", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameInfoRating.App v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
