using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Shared
{
	public class DbResponse<T>
	{
		public bool Success { get; set; }
		public List<T> Data { get; set; }
		public string Info { get; set; }
	}
}
