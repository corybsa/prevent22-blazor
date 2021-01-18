using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Prevent22.Client.Shared;

namespace Prevent22.Client.Services
{
	public class FlyoutService : IFlyoutService
	{
		public event Action OnChange;
		public bool IsOpen { get; set; } = false;
		public string Title { get; set; }
		public Type Content { get; set; }
		public dynamic Data { get; set; }

		private void NotifyDataChanged() => OnChange?.Invoke();

		public void Open(string title, Type content)
		{
			Title = title;
			IsOpen = true;
			Content = content;

			NotifyDataChanged();
		}

		public void Open<T>(string title, Type content, T data)
		{
			Data = data;
			Open(title, content);
		}

		public void Close()
		{
			IsOpen = false;
			Title = null;
			Content = null;

			NotifyDataChanged();
		}
	}
}
