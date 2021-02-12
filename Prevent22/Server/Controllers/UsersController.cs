using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prevent22.Server.Data;
using Prevent22.Shared;
using System;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Prevent22.Server.Controllers
{
	[Auth]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly Helper _helper;

		public UsersController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[HttpPost]
		public async Task<IActionResult> GetUsers(GridReadEventArgs args)
		{
			var response = new DbResponse<User>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<User>("sp_Users", parameters);

				// It's important to call SortGridData first because FilterGridData also
				// truncates the data for paging. So if SortGridData is called second
				// it will only be sorting on one page of data and not the entire data set.
				response = _helper.SortGridData(response, args);
				response = _helper.FilterGridData(response, args);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpGet("warnings/{userId}")]
		public async Task<IActionResult> GetUserWarnings(int userId)
		{
			DbResponse<Warning> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("UserId", userId);
				response = await _helper.ExecStoredProcedure<Warning>("sp_Warnings", parameters);
			}
			catch (ApiException<Warning> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateUser(User user)
		{
			var response = new DbResponse<User>();

			try {
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Update);
				parameters.Add("UserId", user.UserId);
				parameters.Add("RoleId", user.RoleId);
				parameters.Add("FirstName", user.FirstName);
				parameters.Add("LastName", user.LastName);
				parameters.Add("Email", user.Email);
				parameters.Add("Country", user.Country);
				parameters.Add("State", user.State);
				parameters.Add("City", user.City);
				parameters.Add("ZipCode", user.ZipCode);
				parameters.Add("Address", user.Address);
				parameters.Add("Phone", user.Phone);
				parameters.Add("IsBanned", user.IsBanned);
				parameters.Add("BannedUntil", user.BannedUntil);
				parameters.Add("BannedById", user.BannedById);

				response = await _helper.ExecStoredProcedure<User>("sp_Users", parameters);
			} catch(Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
