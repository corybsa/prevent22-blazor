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
			DbResponse<Event> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<Event>("sp_Events", parameters);
			}
			catch (ApiException<Event> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("{eventId}")]
		public async Task<IActionResult> GetEvent(int eventId)
		{
			DbResponse<Event> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("EventId", eventId);
				response = await _helper.ExecStoredProcedure<Event>("sp_Events", parameters);
			}
			catch (ApiException<Event> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin })]
		[HttpPost]
		public async Task<IActionResult> CreateEvents(Event e)
		{
			DbResponse<Event> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("Title", e.Title);
				parameters.Add("Description", e.Description);
				parameters.Add("Location", e.Location);
				parameters.Add("Start", e.Start);
				parameters.Add("End", e.End);
				parameters.Add("IsAllDay", e.IsAllDay);
				parameters.Add("RecurrenceRule", e.RecurrenceRule);
				response = await _helper.ExecStoredProcedure<Event>("sp_Events", parameters);
			}
			catch (ApiException<Event> ex)
			{
				response = ex.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin })]
		[HttpPut]
		public async Task<IActionResult> UpdateEvents(Event e)
		{
			DbResponse<Event> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Update);
				parameters.Add("EventId", e.EventId);
				parameters.Add("Title", e.Title);
				parameters.Add("Description", e.Description);
				parameters.Add("Location", e.Location);
				parameters.Add("Start", e.Start);
				parameters.Add("End", e.End);
				parameters.Add("IsAllDay", e.IsAllDay);
				parameters.Add("RecurrenceRule", e.RecurrenceRule);
				response = await _helper.ExecStoredProcedure<Event>("sp_Events", parameters);
			}
			catch (ApiException<Event> ex)
			{
				response = ex.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin })]
		[HttpDelete("{eventId}")]
		public async Task<IActionResult> UpdateEvents(int eventId)
		{
			DbResponse<Event> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("EventId", eventId);
				response = await _helper.ExecStoredProcedure<Event>("sp_Events", parameters);
			}
			catch (ApiException<Event> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
