using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using API.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using StructureMap;
using TokenAuthASPCore;

namespace API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {   
            // Setup the client
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            
            services.AddIdentityServer()
                // .AddTemporarySigningCredential() // Can be used for testing until a real cert is available
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
               // .AddInMemoryApiResources(Config.GetApis())
                .AddSigningCredential(new X509Certificate2(Path.Combine(".", "certs", "IdentityServer4Auth.pfx"),"Lespaul10"))
                .AddInMemoryApiResources(ResourceToken.GetAllResources())
                .AddAspNetIdentity<ApplicationUser>();
            
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                                                    {
                                                        ValidIssuer = "Guess",
                                                        ValidAudience = "What",
                                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Lespaul10")),
                                                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                                                    };
                });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"API.xml"));
            });
            services.AddMvc();
            
            //StructureMap Container
            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    // Registering to allow for Interfaces to be dynamically mapped
                    _.AssemblyContainingType(typeof(Startup));
                    //List assemblys here
                    _.Assembly("API");
                    _.Assembly("Logic");
                    _.Assembly("Models");
                    _.WithDefaultConventions();
                });
                config.Populate(services);
            });
            
            return container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            
            app.UseIdentityServer();
            
            app.UseSwagger();
            
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}