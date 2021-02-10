using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Prevent22.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class PostService : BaseService, IPostService
	{
		public PostService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage) : base(http, nav, localStorage) { }

		public async Task<DbResponse<Post>> GetPosts()
		{
			return await Get<Post>("api/posts");
		}

		public async Task<DbResponse<Post>> GetPost(int postId)
		{
			return await Get<Post>($"api/posts/{postId}");
		}

		public async Task<DbResponse<Post>> CreatePost(Post post)
		{
			return await Post<Post>("api/posts/", post);
		}

		public async Task<DbResponse<Post>> UpdatePost(Post post)
		{
			return await Put<Post>("api/posts/", post);
		}

		public async Task<DbResponse<Post>> DeletePost(int postId)
		{
			return await Delete<Post>($"api/posts/{postId}");
		}
	}
}
