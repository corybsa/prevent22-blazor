using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Server.Data
{
	public class ApiException<T> : Exception
	{
		public DbResponse<T> Response { get; set; }

		public ApiException(string message, DbResponse<T> response) : base(message)
		{
			Response = response;
		}
	}
}
