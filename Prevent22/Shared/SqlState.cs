using System;
using System.Collections.Generic;
using System.Text;

namespace Prevent22.Shared
{
	public static class SqlState
	{
		public const byte UsernameInUse = 1;
		public const byte UserIsBanned = 2;
		public const byte Other = 255;
	}
}
