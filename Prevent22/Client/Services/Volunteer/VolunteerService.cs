using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class VolunteerService : BaseService, IVolunteerService
	{
		public VolunteerService(HttpClient http) : base(http) { }

		public async Task<DbResponse<Volunteer>> CreateVolunteer(VolunteerRegister volunteer)
		{
			return await Post<Volunteer>("api/volunteers", volunteer);
		}
	}
}
