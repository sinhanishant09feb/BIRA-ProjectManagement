using BIRA_Project_Management.Data;
using BIRA_Project_Management.Models;
using BIRA_Project_Management.Authentication;
using BIRA_Project_Management.Service.DataBase.Implementation;
using BIRA_Project_Management.Service.DataBase.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BIRA_Project_Management {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                ("BasicAuthentication", options => { });
            services.AddAuthorization(options => {
                options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication")
                    .RequireAuthenticatedUser().Build());
            });
            services.AddDbContext<DataBaseContext>(options => 
            options.UseMySQL(Configuration.GetConnectionString("Default")));
            services.AddControllers();
            services.AddScoped<IRepositoryService<Project>, ProjectService>();
            services.AddScoped<IRepositoryService<Issue>, IssueService>();
            services.AddScoped<IIssueUnderProject<Issue>, IssueUnderProjectService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "BIRA_Project_Management", 
                    Description = "BIRA is a software for managing the project by dividing the" +
                    "components(issues) into Story/Bug/Task/Epic. Each issue has a status which" +
                    "idicates the completion and fields for Asignee and Reporter",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            }).AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BIRA_Project_Management v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
