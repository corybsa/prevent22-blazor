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
	public class PostsController : ControllerBase
	{
		private readonly Helper _helper;

		public PostsController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetPosts()
		{
			DbResponse<Post> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				response = await _helper.ExecStoredProcedure<Post>("sp_Posts", parameters);
			}
			catch (ApiException<Post> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[AllowAnonymous]
		[HttpGet("{postId}")]
		public async Task<IActionResult> GetPost(int postId)
		{
			DbResponse<Post> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Get);
				parameters.Add("PostId", postId);
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
		public async Task<IActionResult> CreatePost(Post post)
		{
			DbResponse<Post> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Create);
				parameters.Add("Message", post.Message);
				parameters.Add("CreatedBy", post.CreatedBy);
				parameters.Add("ThreadId", post.ThreadId);
				response = await _helper.ExecStoredProcedure<Post>("sp_Posts", parameters);
			}
			catch (ApiException<Post> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePost(Post post)
		{
			DbResponse<Post> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Update);
				parameters.Add("PostId", post.PostId);
				parameters.Add("Message", post.Message);
				parameters.Add("CreatedBy", post.CreatedBy);
				parameters.Add("CreatedDate", post.CreatedDate);
				parameters.Add("ThreadId", post.ThreadId);
				response = await _helper.ExecStoredProcedure<Post>("sp_Posts", parameters);
			}
			catch (ApiException<Post> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}

		[Auth(Roles = new[] { SystemRole.Admin, SystemRole.Moderator })]
		[HttpDelete("{postId}")]
		public async Task<IActionResult> DeletePost(Post post)
		{
			DbResponse<Post> response;

			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", StatementType.Delete);
				parameters.Add("PostId", post.PostId);
				response = await _helper.ExecStoredProcedure<Post>("sp_Posts", parameters);
			}
			catch (ApiException<Post> e)
			{
				response = e.Response;
				return BadRequest(response);
			}

			return Ok(response);
		}
	}
}
