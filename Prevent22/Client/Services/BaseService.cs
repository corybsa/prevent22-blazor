using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using Telerik.DataSource;

namespace Prevent22.Client.Services
{
	public class BaseService
	{
		private readonly HttpClient _http;

		public BaseService(HttpClient http)
		{
			_http = http;
		}

		public static GridReadEventArgs GetEmptyGridArgs()
		{
			var args = new GridReadEventArgs();
			args.Request = new DataSourceRequest()
			{
				Page = 1,
				PageSize = 10,
				Skip = 0,
				Filters = new List<IFilterDescriptor>(),
				Groups = new List<GroupDescriptor>(),
				Sorts = new List<SortDescriptor>(),
				Aggregates = new List<AggregateDescriptor>()
			};

			return args;
		}

		protected async Task<DbResponse<T>> Get<T>(string url)
		{
			try
			{
				var res = await _http.GetAsync(url);
				CheckStatus(url, res.StatusCode);
				var content = await res.Content.ReadFromJsonAsync<DbResponse<T>>();
				CheckSqlState(content.SqlState);
				return content;
			}
			catch (JsonException)
			{
				throw new Exception("Malformed JSON data. Check URL.");
			}
		}

		protected async Task<DbResponse<T>> Post<T>(string url, object data)
		{
			try
			{
				var res = await _http.PostAsJsonAsync(url, data);
				CheckStatus(url, res.StatusCode);
				var content = await res.Content.ReadFromJsonAsync<DbResponse<T>>();
				CheckSqlState(content.SqlState);
				return content;
			}
			catch (JsonException)
			{
				throw new Exception("Malformed JSON data. Check URL.");
			}
		}

		protected async Task<DbResponse<T>> Put<T>(string url, object data)
		{
			try
			{
				var res = await _http.PutAsJsonAsync(url, data);
				CheckStatus(url, res.StatusCode);
				var content = await res.Content.ReadFromJsonAsync<DbResponse<T>>();
				CheckSqlState(content.SqlState);
				return content;
			}
			catch (JsonException)
			{
				throw new Exception("Malformed JSON data. Check URL.");
			}
		}

		protected async Task<DbResponse<T>> Delete<T>(string url)
		{
			try
			{
				var res = await _http.DeleteAsync(url);
				CheckStatus(url, res.StatusCode);
				var content = await res.Content.ReadFromJsonAsync<DbResponse<T>>();
				CheckSqlState(content.SqlState);
				return content;
			}
			catch (JsonException)
			{
				throw new Exception("Malformed JSON data. Check URL.");
			}
		}

		private void CheckStatus(string url, HttpStatusCode code)
		{
			switch (code)
			{
				case HttpStatusCode.NotFound:
					throw new Exception($"API not found: {url}");
				case HttpStatusCode.Unauthorized:
					throw new Exception("Not authorized to perform this action.");
			}
		}

		private void CheckSqlState(byte state)
		{
			Console.WriteLine(state);

			switch (state)
			{
				case SqlState.UserIsBanned:
					Console.WriteLine("log out");
					break;
			}
		}
	}
}
