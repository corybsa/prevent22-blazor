using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IVolunteerService
	{
		Task<DbResponse<Event>> CreateVolunteer(VolunteerRegister volunteer);
		Task<DbResponse<Volunteer>> DeleteVolunteer(int volunteerId);
		Task<DbResponse<Volunteer>> DeleteVolunteerByCode(int eventId, string code);
	}
}
