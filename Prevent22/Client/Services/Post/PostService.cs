using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class PostService : BaseService, IPostService
	{
		public PostService(HttpClient http) : base(http) { }

		public async Task<DbResponse<Post>> GetPosts()
		{
			return await Get<DbResponse<Post>>("api/posts");
		}

		public async Task<DbResponse<Post>> GetPost(int postId)
		{
			return await Get<DbResponse<Post>>($"api/posts/{postId}");
		}

		public async Task<DbResponse<Post>> CreatePost(Post post)
		{
			return await Post<DbResponse<Post>>("api/posts/", post);
		}

		public async Task<DbResponse<Post>> UpdatePost(Post post)
		{
			return await Put<DbResponse<Post>>("api/posts/", post);
		}

		public async Task<DbResponse<Post>> DeletePost(int postId)
		{
			return await Delete<DbResponse<Post>>($"api/posts/{postId}");
		}
	}
}
