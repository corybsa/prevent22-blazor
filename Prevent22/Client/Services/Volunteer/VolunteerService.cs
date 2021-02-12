using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class VolunteerService : BaseService, IVolunteerService
	{
		public VolunteerService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage) : base(http, nav, localStorage) { }

		public async Task<DbResponse<Volunteer>> CreateVolunteer(VolunteerRegister volunteer)
		{
			return await Post<Volunteer>("api/volunteers", volunteer);
		}

		public async Task<DbResponse<Volunteer>> DeleteVolunteer(int volunteerId)
		{
			return await Delete<Volunteer>($"api/volunteers/{volunteerId}");
		}
	}
}
