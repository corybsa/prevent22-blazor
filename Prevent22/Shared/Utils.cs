using System;
using System.Collections.Generic;
using System.Text;
using Ganss.XSS;
using Microsoft.AspNetCore.Components;

namespace Prevent22.Shared
{
	public static class Utils
	{
		public static MarkupString SanitizeHtml(string html)
		{
			return (MarkupString)new HtmlSanitizer().Sanitize(html, "https://iamuncleguy.com");
		}

		public static string ToLocalTime(DateTime date, string format = null)
		{
			return date.ToLocalTime().ToString(format ?? "MM/dd/yyyy hh:mm tt");
		}
	}
}
