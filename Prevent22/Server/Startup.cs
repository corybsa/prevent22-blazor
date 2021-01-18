using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prevent22.Server.Data;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Prevent22.Server
{
	public class Startup
	{
		public IConfiguration _configuration { get; }
		public IWebHostEnvironment _environment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_configuration = configuration;
			_environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			if (_environment.IsDevelopment())
			{
				services.AddSingleton(new SqlConfiguration(_configuration.GetConnectionString("Dev")));
			}
			else
			{
				services.AddSingleton(new SqlConfiguration(_configuration.GetConnectionString("Prod")));
			}

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ClockSkew = TimeSpan.Zero,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true
					};

					// check jwt expiration
					options.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
							{
								context.Response.Headers.Add("Token-Expired", "true");
							}
							return Task.CompletedTask;
						}
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.Use(async (context, next) =>
			{
				double hstsExpire = TimeSpan.FromDays(30).TotalSeconds;
				context.Response.Headers.Add("X-Frame-Options", "DENY");
				context.Response.Headers.Add("Content-Security-Policy", "" +
				"default-src 'self';" +
				"font-src 'self' data:;" +
				"img-src 'self' data: https:;" +
				"object-src 'none';" +
				"script-src 'self' blob: 'sha256-9mThMC8NT3dPbcxJOtXiiwevtWTAPorqkXGKqI388cI=' 'sha256-v8v3RKRPmN4odZ1CWM5gw80QKPCCWMcpNeOmimNL2AA=' 'sha256-Nf/DChZ0c94B4rwuAMUxo8txJpPuJsZdlwmpQgdolC0=' 'unsafe-eval';" +
				"style-src 'self' 'unsafe-inline';" +
				"connect-src 'self' ws:;" +
				"upgrade-insecure-requests;");
				context.Response.Headers.Add("Strict-Transport-Security", $"max-age={hstsExpire};includeSubDomains;preload");
				await next();
			});

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();
			app.UseCookiePolicy(new CookiePolicyOptions
			{
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = CookieSecurePolicy.Always,
				MinimumSameSitePolicy = SameSiteMode.Strict
			});

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
