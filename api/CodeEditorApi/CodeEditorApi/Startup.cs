using CodeEditorApi.Errors;
using CodeEditorApi.Helpers;
using CodeEditorApiDataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeEditorApi
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
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: "dev",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                );

                options.AddPolicy(name: "staging",
                    builder =>
                    {
                        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                            .WithOrigins("https://*.siucode.io")
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .Build();
                    }
                );
            });

            services.AddHttpClient("GoApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://goapi:8081");
            });

            services.AddControllers().ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    return ApiError.BadRequest(context.ModelState.Values.First().Errors.First().ErrorMessage);
                };
            }).AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Code Editor API",
                    Description = "API For Code Editor Project",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // add auth header for [Authorize] endpoints, ignoring [AllowAnonymous]
                c.OperationFilter<AddAuthHeaderOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Remove microsoft's strange mapping from normal JWT to microsoft claimTypes
            // See: https://stackoverflow.com/a/55740879
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();



            services.AddDbContext<CodeEditorApiContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Will DI any interfaces within the Features folder
            var types = typeof(Program).Assembly.GetTypes().Where(t => t.Namespace != null && (t.Namespace.Contains("Features") || t.Namespace.Contains("Services")));
            foreach(var intfc in types.Where(t => t.IsInterface))
            {
                var impl = types.FirstOrDefault(c => c.IsClass && intfc.IsAssignableFrom(c) && !c.IsAbstract);
                if (impl != null) services.AddScoped(intfc, impl);
            }

            // JWT Auth
            services.AddAuthentication( auth => {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            // Model messages
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            if(env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Editor API V1");
                    c.RoutePrefix = "api/swagger";
                });
            }
            else
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api/v1/swagger.json", "Code Editor API V1");
                    c.RoutePrefix = "api/swagger";
                });
            }
            

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.


            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                logger.LogInformation("Using dev cors");
                app.UseCors("dev");
            }
            else
            {
                logger.LogInformation("Using staging cors");
                app.UseCors("staging");
            }

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
