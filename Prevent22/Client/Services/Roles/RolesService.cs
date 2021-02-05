using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class RolesService : BaseService, IRolesService
	{
		public RolesService(HttpClient http) : base(http) { }

		public async Task<DbResponse<SystemRole>> GetRoles()
		{
			return await Get<DbResponse<SystemRole>>("api/roles");
		}
	}
}
