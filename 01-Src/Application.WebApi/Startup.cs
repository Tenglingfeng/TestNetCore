using System;
using System.IO;
using System.Reflection;
using Applications.Data.DbContext;
using Applications.WebApi.Config;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using T.Application.Commons.AutoFac;
using T.Application.Commons.AutoMapper;
using T.Application.Commons.Filters.ActionResult;
using T.Application.Commons.Middleware;
using T.Application.IdentityServer4;

namespace Applications.WebApi
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 控制器
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置文件服务
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiActionResultAttribute>();
                options.Filters.Add(new AuthorizeFilter());
            });

            services.AddIdentityServerForConfig(IdentityServerConfig.ApiResources, IdentityServerConfig.Clients,
                IdentityServerConfig.ApiScopes);

            services.AddAuthenticationForJwtBearer(Configuration["applicationUrl"], "api1");

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "T.Core", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
                s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Applications.Service.xml"));
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"}
                        },new string[] { }
                    }
                });
            });
            services.AddDbContext<TestDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddAutoMapper(MapperRegister.MapType());
        }

        /// <summary>
        /// AutoFac 配置
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            AutoFac.AutoFacLoad(builder);
        }

        /// <summary>
        /// 中间件配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStateAutoMapper();

            app.UseRouting();

            app.UseAuthentication();

            app.UseIdentityServer();

            app.UseSwagger();

            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "T.Core"); });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}