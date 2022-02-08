using ExcerciseApp.API.Domain.Repositories;
using ExcerciseApp.API.Persistence;
using ExcerciseApp.API.Persistence.Repositories;
using ExcerciseApp.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;

using Microsoft.OpenApi.Models;
using ExcerciseApp.API.Mapping;

namespace ExcerciseApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(config =>
            {
                config.AddProfile<ModelToResourceProfile>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExcerciseApp.API", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"));
            });

            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCors((options) =>
            {
                options.AddPolicy("CorsPolicy_AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //services.AddHsts(options =>
            //{
            //    options.MaxAge = TimeSpan.FromDays(60);
            //    options.Preload = true;
            //    options.IncludeSubDomains = true;
            //    options.ExcludedHosts.Add("example.com");
            //    options.ExcludedHosts.Add("www.example.com");
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExcerciseApp.API v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy_AllowAll");

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
