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
	[Auth(Roles = new[] { SystemRole.Admin, SystemRole.Moderator })]
	[Route("api/[controller]")]
	[ApiController]
	public class WarningsController : ControllerBase
	{
		private readonly Helper _helper;

		public WarningsController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[HttpGet]
		public async Task<IActionResult> GetWarnings()
		{
			DbResponse<Warning> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<Warning>("sp_Warnings", parameters);
			}
			catch (ApiException<Warning> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpGet("{warningId}")]
		public async Task<IActionResult> GetWarning(int warningId)
		{
			DbResponse<Warning> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("WarningId", warningId);
				response = await _helper.ExecStoredProcedure<Warning>("sp_Warnings", parameters);
			}
			catch (ApiException<Warning> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateWarning(Warning warning)
		{
			DbResponse<Warning> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("WarningReason", warning.WarningReason);
				parameters.Add("UserId", warning.UserId);
				parameters.Add("CreatedById", warning.CreatedById);
				parameters.Add("PostId", warning.PostId);
				parameters.Add("ShouldBanUser", warning.ShouldBanUser);
				parameters.Add("BanUntil", warning.BanUntil);
				response = await _helper.ExecStoredProcedure<Warning>("sp_Warnings", parameters);

				// TODO: email user
			}
			catch (ApiException<Warning> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateWarning(Warning warning)
		{
			DbResponse<Warning> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Update);
				parameters.Add("WarningId", warning.WarningId);
				parameters.Add("WarningReason", warning.WarningReason);
				parameters.Add("WarningDate", warning.WarningDate);
				parameters.Add("UserId", warning.UserId);
				parameters.Add("CreatedById", warning.CreatedById);
				parameters.Add("PostId", warning.PostId);
				response = await _helper.ExecStoredProcedure<Warning>("sp_Warnings", parameters);
			}
			catch (ApiException<Warning> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpDelete("{warningId}")]
		public async Task<IActionResult> DeleteWarning(int warningId)
		{
			DbResponse<Warning> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("WarningId", warningId);
				response = await _helper.ExecStoredProcedure<Warning>("sp_Warnings", parameters);
			}
			catch (ApiException<Warning> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
