using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class WarningService : BaseService, IWarningService
	{
		public WarningService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage) : base(http, nav, localStorage) { }

		public async Task<DbResponse<Warning>> GetWarnings()
		{
			return await Get<Warning>("api/warnings");
		}

		public async Task<DbResponse<Warning>> GetWarning(int warningId)
		{
			return await Get<Warning>($"api/warnings/{warningId}");
		}

		public async Task<DbResponse<Warning>> CreateWarning(Warning warning)
		{
			return await Post<Warning>("api/warnings", warning);
		}

		public async Task<DbResponse<Warning>> UpdateWarning(Warning warning)
		{
			return await Put<Warning>("api/warnings", warning);
		}

		public async Task<DbResponse<Warning>> DeleteWarning(int warningId)
		{
			return await Delete<Warning>($"api/warnings/{warningId}");
		}
	}
}
