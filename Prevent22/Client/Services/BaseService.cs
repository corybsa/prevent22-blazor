using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
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
		private readonly NavigationManager _nav;
		private readonly ILocalStorageService _localStorage;

		public BaseService(HttpClient http, NavigationManager nav, ILocalStorageService localStorage)
		{
			_http = http;
			_nav = nav;
			_localStorage = localStorage;
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

		private async void CheckSqlState(byte state)
		{
			switch (state)
			{
				case SqlState.UserIsBanned:
					AuthService.IsLoggedIn = false;
					UserService.user = null;
					await _localStorage.ClearAsync();
					_nav.NavigateTo("/", forceLoad: true);
					break;
			}
		}
	}
}
