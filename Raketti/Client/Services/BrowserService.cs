﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public class BrowserService
	{
		private readonly IJSRuntime _js;
		public static event Func<Task> OnResize;

		public BrowserService(IJSRuntime js)
		{
			_js = js;
		}

		[JSInvokable]
		public static async Task OnBrowserResize()
		{
			await OnResize?.Invoke();
		}

		public async Task<int> GetHeight()
		{
			return await _js.InvokeAsync<int>("raketti.getHeight");
		}

		public async Task<int> GetWidth()
		{
			return await _js.InvokeAsync<int>("raketti.getWidth");
		}
	}
}
