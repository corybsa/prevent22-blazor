using System;
using System.Collections.Generic;
using System.Text;

namespace Prevent22.Shared
{
	public static class Utils
	{
		public static string GetHumanDate(DateTime? dateTime)
		{
			if (dateTime == null)
			{
				return "No posts";
			}

			TimeSpan ts = DateTime.Now.Subtract(dateTime.GetValueOrDefault(DateTime.Now));

			// The trick: make variable contain date and time representing the desired timespan,
			// having +1 in each date component.
			DateTime date = DateTime.MinValue + ts;

			return ProcessPeriod(date.Year - 1, date.Month - 1, "year")
				   ?? ProcessPeriod(date.Month - 1, date.Day - 1, "month")
				   ?? ProcessPeriod(date.Day - 1, date.Hour, "day", "Yesterday")
				   ?? ProcessPeriod(date.Hour, date.Minute, "hour")
				   ?? ProcessPeriod(date.Minute, date.Second, "minute")
				   ?? ProcessPeriod(date.Second, 0, "second")
				   ?? "Right now";
		}

		private static string ProcessPeriod(int value, int subValue, string name, string singularName = null)
		{
			if (value == 0)
			{
				return null;
			}

			if (value == 1)
			{
				if (!string.IsNullOrEmpty(singularName))
				{
					return singularName;
				}

				string articleSuffix = name[0] == 'h' ? "n" : string.Empty;
				return subValue == 0
					? string.Format("A{0} {1} ago", articleSuffix, name)
					: string.Format("About a{0} {1} ago", articleSuffix, name);
			}

			return subValue == 0
				? string.Format("{0} {1}s ago", value, name)
				: string.Format("About {0} {1}s ago", value, name);
		}
	}
}
