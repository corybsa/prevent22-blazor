using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prevent22.Server.Data;
using Prevent22.Shared;

namespace Prevent22.Server.Controllers
{
	[Auth]
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : ControllerBase
	{
		private readonly Helper _helper;

		public EventsController(SqlConfiguration sql) {
			_helper = new Helper(sql);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetEvents()
		{
			var response = new DbResponse<Event>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<Event>("sp_Events", parameters);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
