using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prevent22.Server.Data;
using Prevent22.Shared;
using System;
using System.Threading.Tasks;

namespace Prevent22.Server.Controllers
{
	[Auth]
	[Route("api/[controller]")]
	[ApiController]
	public class ThreadsController : ControllerBase
	{
		private readonly Helper _helper;

		public ThreadsController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetThreads()
		{
			DbResponse<Thread> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<Thread>("sp_Threads", parameters);
			}
			catch (ApiException<Thread> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("{threadId}")]
		public async Task<IActionResult> GetThread(int threadId)
		{
			DbResponse<Thread> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("ThreadId", threadId);
				response = await _helper.ExecStoredProcedure<Thread>("sp_Threads", parameters);
			}
			catch (ApiException<Thread> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("{threadId}/posts")]
		public async Task<IActionResult> GetThreadPosts(int threadId)
		{
			DbResponse<Post> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("ThreadId", threadId);
				response = await _helper.ExecStoredProcedure<Post>("sp_Posts", parameters);
			}
			catch (ApiException<Post> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateThread(Thread thread)
		{
			DbResponse<Thread> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("BoardId", thread.BoardId);
				parameters.Add("ThreadName", thread.ThreadName);
				parameters.Add("CreatedBy", thread.CreatedBy);
				response = await _helper.ExecStoredProcedure<Thread>("sp_Threads", parameters);
			}
			catch (ApiException<Thread> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateThread(Thread thread)
		{
			DbResponse<Thread> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Update);
				parameters.Add("ThreadId", thread.ThreadId);
				parameters.Add("BoardId", thread.BoardId);
				parameters.Add("ThreadName", thread.ThreadName);
				parameters.Add("CreatedBy", thread.CreatedBy);
				parameters.Add("CreatedDate", thread.CreatedDate);
				parameters.Add("LastPostDate", thread.LastPostDate);
				parameters.Add("IsClosed", thread.IsClosed);
				response = await _helper.ExecStoredProcedure<Thread>("sp_Threads", parameters);
			}
			catch (ApiException<Thread> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin, SystemRole.Moderator })]
		[HttpDelete("{threadId}")]
		public async Task<IActionResult> DeleteThread(int threadId)
		{
			DbResponse<Thread> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("ThreadId", threadId);
				response = await _helper.ExecStoredProcedure<Thread>("sp_Threads", parameters);
			}
			catch (ApiException<Thread> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
