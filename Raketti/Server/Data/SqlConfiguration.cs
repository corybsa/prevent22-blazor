using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Server.Data
{
	public class SqlConfiguration
	{
		public string Value { get; }
		public SqlConfiguration(string value) => Value = value;
	}
}
