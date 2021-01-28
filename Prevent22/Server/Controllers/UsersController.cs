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
	}
}
