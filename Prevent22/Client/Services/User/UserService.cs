using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Prevent22.Client.Services
{
	public class UserService : BaseService, IUserService
	{
		public static User user { get; set; }

		public UserService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage) : base(http, nav, localStorage) { }

		public async Task<DbResponse<User>> GetUsers(GridReadEventArgs args)
		{
			return await Post<User>("api/users", args);
		}

		public async Task<DbResponse<User>> GetUser(int userId)
		{
			return await Get<User>($"api/test/user/{userId}");
		}

		public async Task<DbResponse<User>> UpdateUser(User user)
		{
			return await Post<User>("api/users/update", user);
		}
	}
}
