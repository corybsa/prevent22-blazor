using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Prevent22.Client.Services
{
	public class UserService : IUserService
	{
		private readonly HttpClient _http;
		public static User user { get; set; }

		public UserService(HttpClient http)
		{
			_http = http;
		}

		public async Task<DbResponse<User>> GetUsers(GridReadEventArgs args)
		{
			var res = await _http.PostAsJsonAsync("api/users", args);
			return await res.Content.ReadFromJsonAsync<DbResponse<User>>();
		}

		public async Task<DbResponse<User>> GetUser(int userId)
		{
			return await _http.GetFromJsonAsync<DbResponse<User>>($"api/test/user/{userId}");
		}
	}
}
