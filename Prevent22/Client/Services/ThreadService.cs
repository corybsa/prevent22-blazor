using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class ThreadService : IThreadService
	{
		private readonly HttpClient _http;

		public ThreadService(HttpClient http)
		{
			_http = http;
		}

		public async Task<DbResponse<Thread>> GetThreads()
		{
			return await _http.GetFromJsonAsync<DbResponse<Thread>>("api/threads");
		}

		public async Task<DbResponse<Thread>> GetThread(int threadId)
		{
			return await _http.GetFromJsonAsync<DbResponse<Thread>>($"api/threads/{threadId}");
		}

		public async Task<DbResponse<Post>> GetThreadPosts(int threadId)
		{
			return await _http.GetFromJsonAsync<DbResponse<Post>>($"api/threads/{threadId}/posts");
		}

		public async Task<DbResponse<Thread>> CreateThread(Thread thread)
		{
			var result = await _http.PostAsJsonAsync("api/threads", thread);
			return await result.Content.ReadFromJsonAsync<DbResponse<Thread>>();
		}

		public async Task<DbResponse<Thread>> UpdateThread(Thread thread)
		{
			var result = await _http.PutAsJsonAsync("api/threads", thread);
			return await result.Content.ReadFromJsonAsync<DbResponse<Thread>>();
		}

		public async Task<DbResponse<Thread>> DeleteThread(int? threadId)
		{
			var result = await _http.DeleteAsync($"api/threads/{threadId}");
			return await result.Content.ReadFromJsonAsync<DbResponse<Thread>>();
		}
	}
}
