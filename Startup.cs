using GreatBooks.Data;
using GreatBooks.Interfaces;
using GreatBooks.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Configuration.Internal;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreatBooks
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
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Great Books API",
                    Version = "v1"
                });
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options =>
           {
               options.AddDefaultPolicy(
                   builder =>
                   {
                       builder.AllowAnyOrigin();
                   }
               );
           });

            services.AddRouting();

            services.AddSingleton<IBookRepository>(InitializeCosmosClientInstanceAsync(
                Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

            string requestUri = Configuration.GetSection("OpenLibrary").GetSection("RequestUri").Value;
            services.AddTransient<IOpenLibraryService, OpenLibraryService>();
            services.AddTransient<HttpClient>(h => new HttpClient() { BaseAddress = new System.Uri(requestUri) });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Great Books API V1");
            });

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

            app.UseCors();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "api/{controller}/{action=Index}/{id?}");
                routes.MapRoute(name: "search", template: "api/{controller}/{action=Index}/{query}");
            });

            // app.UseSpa(spa =>
            // {
            //     // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //     // see https://go.microsoft.com/fwlink/?linkid=864501

            //     spa.Options.SourcePath = "ClientApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseAngularCliServer(npmScript: "start");
            //         //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            //     }
            // });
        }

        private static async Task<BookRepository> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();
            BookRepository repository = new BookRepository(client, databaseName, containerName);
            Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.CreateContainerIfNotExistsAsync(containerName, "/key");

            return repository;
        }
    }
}
