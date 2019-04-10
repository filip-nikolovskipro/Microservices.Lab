using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Web.Record.HttpAggregator.Config;
using Web.Record.HttpAggregator.Services;

namespace Web.Record.HttpAggregator
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
			services.AddHealthChecks()
			.AddCheck("self", () => HealthCheckResult.Healthy());
			//.AddUrlGroup(new Uri(_cfg["AuditLogUrlHc"]), name: "auditlogapi-check", tags: new string[] { "auditlogapi" })
			//.AddUrlGroup(new Uri(_cfg["AuthorizationUrlHc"]), name: "authorizationapi-check", tags: new string[] { "authorizationapi" })

			services.AddCustomMvc(Configuration)
				//.AddCustomAuthentication(Configuration)
				.AddApplicationServices();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			var pathBase = Configuration["PATH_BASE"];
			if (!string.IsNullOrEmpty(pathBase))
			{
				loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
				app.UsePathBase(pathBase);
			}

			app.UseHealthChecks("/hc", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});

			app.UseHealthChecks("/liveness", new HealthCheckOptions
			{
				Predicate = r => r.Name.Contains("self")
			});

			app.UseCors("CorsPolicy");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//TODO: ADD AUTH
			//app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseMvc();

			//TODO: ADD sWAGGER
			//app.UseSwagger()
			//	.UseSwaggerUI(c =>
			//	{
			//		c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Purchase BFF V1");
			//		//c.ConfigureOAuth2("Microsoft.eShopOnContainers.Web.Shopping.HttpAggregatorwaggerui", "", "", "Purchase BFF Swagger UI");
			//	});
		}
	}

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions();
			services.Configure<UrlsConfig>(configuration.GetSection("urls"));

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			//TODO: Add Swagger


			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
					.SetIsOriginAllowed((host) => true)
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});

			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			//register delegating handlers
			//services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//register http services

			services.AddHttpClient<IAuthorizationService, AuthorizationService>();
			//.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
			//.AddPolicyHandler(GetRetryPolicy())
			//.AddPolicyHandler(GetCircuitBreakerPolicy());

			services.AddHttpClient<IAuditLogService, AuditLogService>();

			return services;
		}

	}
}
