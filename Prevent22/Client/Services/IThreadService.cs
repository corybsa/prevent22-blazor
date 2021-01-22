using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IThreadService
	{
		Task<DbResponse<Thread>> GetThreads();
		Task<DbResponse<Thread>> GetThread(int threadId);
		Task<DbResponse<Post>> GetThreadPosts(int threadId);
		Task<DbResponse<Thread>> CreateThread(Thread thread);
		Task<DbResponse<Thread>> UpdateThread(Thread thread);
		Task<DbResponse<Thread>> DeleteThread(int? threadId);
	}
}
