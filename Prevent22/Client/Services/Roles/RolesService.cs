using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class RolesService : BaseService, IRolesService
	{
		public RolesService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage) : base(http, nav, localStorage) { }

		public async Task<DbResponse<SystemRole>> GetRoles()
		{
			return await Get<SystemRole>("api/roles");
		}
	}
}
