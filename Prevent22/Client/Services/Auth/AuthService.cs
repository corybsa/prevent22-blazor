using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System;
using System.Linq;
using System.Net;
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
		private readonly IToastService _toast;

		public AuthService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage, IToastService toast)
		{
			_http = http;
			_nav = nav;
			_localStorage = localStorage;
			_toast = toast;
		}

		public async Task<AuthResponse<User>> Login(AuthInfo auth)
		{
			var result = await _http.PostAsJsonAsync("api/auth", auth);
			var data = await result.Content.ReadFromJsonAsync<AuthResponse<User>>();
			UserService.user = data.Data;

			return data;
		}

		public async Task<DbResponse<User>> Check(int userId)
		{
			var result = await _http.PostAsJsonAsync("api/auth/check", userId);

			if (result.StatusCode == HttpStatusCode.Unauthorized)
			{
				IsLoggedIn = false;
				UserService.user = null;
				await _localStorage.ClearAsync();
				_nav.NavigateTo("/", forceLoad: true);
				return null;
			}

			var data = await result.Content.ReadFromJsonAsync<DbResponse<User>>();

			if (data.SqlState == SqlState.UserIsBanned)
			{
				IsLoggedIn = false;
				UserService.user = null;
				await _localStorage.ClearAsync();
				_nav.NavigateTo("/", forceLoad: true);
				return null;
			}

			if (data.Data != null)
			{
				UserService.user = data.Data.First();
			}

			return data;
		}

		public async Task<AuthResponse<User>> Register(UserRegister user)
		{
			var result = await _http.PostAsJsonAsync("api/auth/register", user);
			var data = await result.Content.ReadFromJsonAsync<AuthResponse<User>>();
			UserService.user = data.Data;

			return data;
		}
	}
}
