using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IPostService
	{
		Task<DbResponse<Post>> GetPosts();
		Task<DbResponse<Post>> GetPost(int postId);
		Task<DbResponse<Post>> CreatePost(Post post);
		Task<DbResponse<Post>> UpdatePost(Post post);
		Task<DbResponse<Post>> DeletePost(int postId);
	}
}
