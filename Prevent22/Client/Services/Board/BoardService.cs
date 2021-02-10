using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class BoardService : BaseService, IBoardService
	{
		public BoardService(HttpClient http) : base(http) { }

		public async Task<DbResponse<Board>> GetBoards()
		{
			return await Get<Board>("api/boards");
		}

		public async Task<DbResponse<Board>> GetBoard(int boardId)
		{
			return await Get<Board>($"api/boards/{boardId}");
		}

		public async Task<DbResponse<Thread>> GetBoardThreads(int boardId)
		{
			return await Get<Thread>($"api/boards/{boardId}/threads");
		}

		public async Task<DbResponse<Board>> CreateBoard(Board board)
		{
			return await Post<Board>("api/boards", board);
		}

		public async Task<DbResponse<Board>> UpdateBoard(Board board)
		{
			return await Put<Board>("api/boards", board);
		}

		public async Task<DbResponse<Board>> DeleteBoard(int boardId)
		{
			return await Delete<Board>($"api/boards/{boardId}");
		}
	}
}
