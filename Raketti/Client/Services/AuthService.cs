using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class AuthService : IAuthService
	{
		public static bool IsLoggedIn { get; set; } = false;
		private readonly HttpClient _http;
		private readonly NavigationManager _nav;
		private readonly ILocalStorageService _localStorage;

		public AuthService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage)
		{
			_http = http;
			_nav = nav;
			_localStorage = localStorage;
		}

		public async Task<AuthResponse<string>> Login(AuthInfo auth)
		{
			var result = await _http.PostAsJsonAsync("api/auth", auth);
			return await result.Content.ReadFromJsonAsync<AuthResponse<string>>();
		}

		public async Task<DbResponse<User>> Check(int userId)
		{
			var result = await _http.PostAsJsonAsync("api/auth/check", userId);
			
			if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				IsLoggedIn = false;
				UserService.user = null;
				await _localStorage.ClearAsync();
				_nav.NavigateTo("/", forceLoad: true);
			}

			return await result.Content.ReadFromJsonAsync<DbResponse<User>>();
		}
	}
}
