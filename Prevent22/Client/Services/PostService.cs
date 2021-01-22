using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class PostService : IPostService
	{
		private readonly HttpClient _http;

		public PostService(HttpClient http)
		{
			_http = http;
		}

		public async Task<DbResponse<Post>> GetPosts()
		{
			return await _http.GetFromJsonAsync<DbResponse<Post>>("api/posts");
		}

		public async Task<DbResponse<Post>> GetPost(int postId)
		{
			return await _http.GetFromJsonAsync<DbResponse<Post>>($"api/posts/{postId}");
		}

		public async Task<DbResponse<Post>> CreatePost(Post post)
		{
			var result = await _http.PostAsJsonAsync("api/posts", post);
			return await result.Content.ReadFromJsonAsync<DbResponse<Post>>();
		}

		public async Task<DbResponse<Post>> UpdatePost(Post post)
		{
			var result = await _http.PutAsJsonAsync("api/posts", post);
			return await result.Content.ReadFromJsonAsync<DbResponse<Post>>();
		}

		public async Task<DbResponse<Post>> DeletePost(int postId)
		{
			var result = await _http.DeleteAsync($"api/posts/{postId}");
			return await result.Content.ReadFromJsonAsync<DbResponse<Post>>();
		}
	}
}
