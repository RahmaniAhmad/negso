using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using repositoryPattern.Api.Models;
using repositoryPattern.Api.Services;
using repositoryPattern.Business;
using repositoryPattern.Data;
using repositoryPattern.Repository;

namespace repositoryPattern.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services
                .AddCors (options => {
                    options
                        .AddPolicy ("ReactCorsPolicy",
                            builder =>
                            builder
                            .AllowAnyMethod ()
                            .AllowAnyHeader ()
                            .WithOrigins ("http://localhost:3000")
                            .AllowCredentials ()
                            .Build ());
                });
            services.AddOptions<BearerTokensOptions> ().Bind (Configuration.GetSection ("BearerTokens"));

            services.AddTransient<ApplicationContext> ();
            services.AddTransient<IStudentService, StudentService> ();
            services.AddTransient<IStudentRepository, StudentRepository> ();
            services.AddTransient<IGradeService, GradeService> ();
            services.AddTransient<IGradeRepository, GradeRepository> ();
            services.AddTransient<IUserService, UserService> ();
            services.AddTransient<IUserRepository, UserRepository> ();
            services.AddTransient<ITokenFactoryService, TokenFactoryService> ();
            services.AddTransient<ISecurityService, SecurityService> ();
            services.AddTransient<IUnitOfWork, UnitOfWork> ();
            services
                .AddScoped (typeof (IBaseRepository<>),
                    typeof (BaseRepository<>));
            services.AddControllers ().AddNewtonsoftJson (options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            // Needed for jwt auth.
            services
                .AddAuthentication (options => {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters {
                        ValidIssuer = Configuration["BearerTokens:Issuer"], // site that makes the token
                        ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                        ValidAudience = Configuration["BearerTokens:Audience"], // site that consumes the token
                        ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration["BearerTokens:Key"])),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    };
                    cfg.Events = new JwtBearerEvents {
                        OnAuthenticationFailed = context => {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory> ().CreateLogger (nameof (JwtBearerEvents));
                                logger.LogError ("Authentication failed.", context.Exception);
                                return Task.CompletedTask;
                            },
                            OnChallenge = context => {
                                var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory> ().CreateLogger (nameof (JwtBearerEvents));
                                logger.LogError ("OnChallenge error", context.Error, context.ErrorDescription);
                                return Task.CompletedTask;
                            }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();
            app.UseCors ("ReactCorsPolicy");

            app.UseAuthentication ();
            app.UseAuthorization ();

            app
                .UseEndpoints (endpoints => {
                    endpoints.MapControllers ();
                });
        }
    }
}