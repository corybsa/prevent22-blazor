using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Client.Services
{
	public interface IWarningService
	{
		Task<DbResponse<Warning>> GetWarnings();
		Task<DbResponse<Warning>> GetWarning(int warningId);
		Task<DbResponse<Warning>> CreateWarning(Warning warning);
		Task<DbResponse<Warning>> UpdateWarning(Warning warning);
		Task<DbResponse<Warning>> DeleteWarning(int warningId);
	}
}
