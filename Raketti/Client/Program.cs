using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.Toast;
using Raketti.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Raketti.Client.Auth;

namespace Raketti.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			// 3rd party packages
			builder.Services.AddBlazoredToast();
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddTelerikBlazor();

			// Services
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<BrowserService>();
			builder.Services.AddScoped<IFlyoutService, FlyoutService>();

			// Authorization
			builder.Services.AddOptions();

			// Set up policies and handlers
			builder.Services.AddAuthorizationCore(config =>
			{
				config.AddPolicy(Policies.IsGlobalAdmin, policy => policy.Requirements.Add(new GlobalAdminRequirement()));
				config.AddPolicy(Policies.IsAdmin, policy => policy.Requirements.Add(new AdminRequirement()));
				config.AddPolicy(Policies.IsTeacher, policy => policy.Requirements.Add(new TeacherRequirement()));
			});

			builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

			await builder.Build().RunAsync();
		}
	}
}
