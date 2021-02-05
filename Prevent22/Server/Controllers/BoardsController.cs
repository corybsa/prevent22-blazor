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
	public class BoardsController : ControllerBase
	{
		private readonly Helper _helper;

		public BoardsController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetBoards()
		{
			var response = new DbResponse<Board>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<Board>("sp_Boards", parameters);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("{boardId}")]
		public async Task<IActionResult> GetBoard(int boardId)
		{
			var response = new DbResponse<Board>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("BoardId", boardId);
				response = await _helper.ExecStoredProcedure<Board>("sp_Boards", parameters);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("{boardId}/threads")]
		public async Task<IActionResult> GetThread(int boardId)
		{
			var response = new DbResponse<Thread>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("BoardId", boardId);
				response = await _helper.ExecStoredProcedure<Thread>("sp_Threads", parameters);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin, SystemRole.Moderator })]
		[HttpPost]
		public async Task<IActionResult> CreateBoard(Board board)
		{
			var response = new DbResponse<Board>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("BoardName", board.BoardName);
				parameters.Add("BoardDescription", board.BoardDescription);
				response = await _helper.ExecStoredProcedure<Board>("sp_Boards", parameters);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin, SystemRole.Moderator })]
		[HttpPut]
		public async Task<IActionResult> UpdateBoard(Board board)
		{
			var response = new DbResponse<Board>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Update);
				parameters.Add("BoardId", board.BoardId);
				parameters.Add("BoardName", board.BoardName);
				parameters.Add("BoardDescription", board.BoardDescription);
				response = await _helper.ExecStoredProcedure<Board>("sp_Boards", parameters);
			}
			catch (Exception e)
			{
				response.Success = false;
				response.Info = e.Message;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin, SystemRole.Moderator })]
		[HttpDelete("{boardId}")]
		public async Task<IActionResult> DeleteBoard(int boardId)
		{
			var response = new DbResponse<Board>();

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("BoardId", boardId);
				response = await _helper.ExecStoredProcedure<Board>("sp_Boards", parameters);
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
