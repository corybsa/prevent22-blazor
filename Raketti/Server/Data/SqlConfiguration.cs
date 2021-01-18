using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prevent22.Server.Data
{
	public class SqlConfiguration
	{
		public string Value { get; }
		public SqlConfiguration(string value) => Value = value;
	}
}
