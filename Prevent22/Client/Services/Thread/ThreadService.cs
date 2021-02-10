using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class ThreadService : BaseService, IThreadService
	{
		public ThreadService(HttpClient http) : base(http) { }

		public async Task<DbResponse<Thread>> GetThreads()
		{
			return await Get<Thread>("api/threads");
		}

		public async Task<DbResponse<Thread>> GetThread(int threadId)
		{
			return await Get<Thread>($"api/threads/{threadId}");
		}

		public async Task<DbResponse<Post>> GetThreadPosts(int threadId)
		{
			return await Get<Post>($"api/threads/{threadId}/posts");
		}

		public async Task<DbResponse<Thread>> CreateThread(Thread thread)
		{
			return await Post<Thread>("api/threads", thread);
		}

		public async Task<DbResponse<Thread>> UpdateThread(Thread thread)
		{
			return await Put<Thread>("api/threads", thread);
		}

		public async Task<DbResponse<Thread>> DeleteThread(int? threadId)
		{
			return await Delete<Thread>($"api/threads/{threadId}");
		}
	}
}
