using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IEventService
	{
		Task<DbResponse<Event>> GetEvents();
		Task<DbResponse<Event>> CreateEvent(Event e);
		Task<DbResponse<Event>> UpdateEvent(Event e);
		Task<DbResponse<Event>> DeleteEvent(int eventId);
	}
}
