using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class EventService : BaseService, IEventService
	{
		public EventService(HttpClient http) : base(http) { }

		public async Task<DbResponse<Event>> GetEvents()
		{
			return await Get<Event>("api/events");
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
