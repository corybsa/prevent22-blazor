﻿using Blazored.Toast.Services;
using Prevent22.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace Prevent22.Client.Services
{
	public class UserService : BaseService, IUserService
	{
		public static User user { get; set; }

		public UserService(HttpClient http, IToastService toastService) : base(http, toastService) { }

		public async Task<DbResponse<User>> GetUsers(GridReadEventArgs args)
		{
			return await Post<DbResponse<User>>("api/users", args);
		}

		public async Task<DbResponse<User>> GetUser(int userId)
		{
			return await Get<DbResponse<User>>($"api/test/user/{userId}");
		}
	}
}
