using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Prevent22.Client.Shared;

namespace Prevent22.Client.Services
{
	public interface IFlyoutService
	{
		public event Action OnChange;
		public bool IsOpen { get; set; }
		public string Title { get; set; }
		public Type Content { get; set; }
		public dynamic Data { get; set; }

		public void Open(string title, Type content);
		public void Open<T>(string title, Type content, T data);
		public void Close();
	}
}
