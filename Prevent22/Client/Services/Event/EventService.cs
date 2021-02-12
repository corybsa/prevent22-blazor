using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class EventService : BaseService, IEventService
	{
		public EventService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage) : base(http, nav, localStorage) { }

		public async Task<DbResponse<Event>> GetEvents()
		{
			return await Get<Event>("api/events");
		}

		public async Task<DbResponse<Event>> GetEvent(int eventId)
		{
			return await Get<Event>($"api/events/{eventId}");
		}

		public async Task<DbResponse<Event>> CreateEvent(Event e)
		{
			return await Post<Event>("api/events", e);
		}

		public async Task<DbResponse<Event>> UpdateEvent(Event e)
		{
			return await Put<Event>("api/events", e);
		}

		public async Task<DbResponse<Event>> DeleteEvent(int eventId)
		{
			return await Delete<Event>($"api/events/{eventId}");
		}
	}
}
