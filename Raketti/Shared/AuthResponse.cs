using System;
using System.Collections.Generic;
using System.Text;

namespace Raketti.Shared
{
	public class AuthResponse<T>
	{
		public T Data { get; set; }
		public bool Success { get; set; } = true;
		public string Message { get; set; }
	}
}
