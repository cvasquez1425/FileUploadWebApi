using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileUploadWebApi
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
            #region CORS
            // Angular application to call the Web Api Methods, you must tell the Web API that your are allowing
            // Cross Origin Resourse Sharing (CORS). To use CORS 
            // dotnet add package Microsoft.AspNetCore.Cors
            #endregion
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //Allow Web API to accept requests only from localhost 4200.
            app.UseCors(
                options => options.WithOrigins
                  ("http://localhost:4200")
                  .AllowAnyMethod().AllowAnyHeader()
            );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
