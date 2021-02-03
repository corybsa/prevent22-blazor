using Blazored.Toast.Services;
using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class EventService : BaseService, IEventService
	{
		public EventService(HttpClient http, IToastService toastService) : base(http, toastService) { }

		public async Task<DbResponse<Event>> GetEvents()
		{
			return await Get<DbResponse<Event>>("api/events");
		}
	}
}
