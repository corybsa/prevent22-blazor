using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prevent22.Server.Data;
using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Server.Controllers
{
	[Auth]
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly Helper _helper;

		public RolesController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[HttpGet]
		public async Task<IActionResult> GetRoles()
		{
			DbResponse<SystemRole> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<SystemRole>("sp_Roles", parameters);
			}
			catch (ApiException<SystemRole> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
