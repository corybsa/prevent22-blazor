using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public class BoardService : IBoardService
	{
		private readonly HttpClient _http;

		public BoardService(HttpClient http)
		{
			_http = http;
		}

		public async Task<DbResponse<Board>> GetBoards()
		{
			return await _http.GetFromJsonAsync<DbResponse<Board>>("api/boards");
		}

		public async Task<DbResponse<Board>> GetBoard(int boardId)
		{
			return await _http.GetFromJsonAsync<DbResponse<Board>>($"api/boards/{boardId}");
		}

		public async Task<DbResponse<Thread>> GetBoardThreads(int boardId)
		{
			return await _http.GetFromJsonAsync<DbResponse<Thread>>($"api/boards/{boardId}/threads");
		}

		public async Task<DbResponse<Board>> CreateBoard(Board board)
		{
			var result = await _http.PostAsJsonAsync("api/boards", board);
			return await result.Content.ReadFromJsonAsync<DbResponse<Board>>();
		}

		public async Task<DbResponse<Board>> UpdateBoard(Board board)
		{
			var result = await _http.PutAsJsonAsync("api/boards", board);
			return await result.Content.ReadFromJsonAsync<DbResponse<Board>>();
		}

		public async Task<DbResponse<Board>> DeleteBoard(int boardId)
		{
			var result = await _http.DeleteAsync($"api/boards/{boardId}");
			return await result.Content.ReadFromJsonAsync<DbResponse<Board>>();
		}
	}
}
