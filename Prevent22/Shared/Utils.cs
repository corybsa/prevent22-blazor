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

			var sb = new StringBuilder();
			var now = DateTime.Now;
			int? years = now.Year - dateTime?.Year;
			int? months = now.Month - dateTime?.Month;
			int? days = now.Day - dateTime?.Day;

			if (years == 1)
			{
				sb.Append($"{years} year, ");
			}
			else if (years > 1)
			{
				sb.Append($"{years} years, ");
			}

			if (months == 1)
			{
				sb.Append($"{months} month, ");
			}
			else if (months > 1)
			{
				sb.Append($"{months} months, ");
			}

			if (days == 0)
			{
				sb.Append($"Today");
			}
			else
			{
				sb.Append($"{days} day ago");
			}

			return sb.ToString();
		}
	}
}
