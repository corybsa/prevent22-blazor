using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Shared
{
	public class DbResponse<T>
	{
		public bool Success { get; set; }
		public List<T> Data { get; set; }
		public string Info { get; set; }
	}
}
