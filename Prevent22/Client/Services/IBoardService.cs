using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IBoardService
	{
		Task<DbResponse<Board>> GetBoards();
		Task<DbResponse<Board>> GetBoard(int boardId);
		Task<DbResponse<Thread>> GetBoardThreads(int boardId);
		Task<DbResponse<Board>> CreateBoard(Board board);
		Task<DbResponse<Board>> UpdateBoard(Board board);
		Task<DbResponse<Board>> DeleteBoard(int boardId);
	}
}
