﻿using System;
using System.Collections.Generic;
using QS.ViewModels;

namespace QS.Navigation
{
	public interface IPage
	{
		/// <summary>
		/// Хеш страницы, используется для поиска запрошенной страницы среди уже открытых и переключения на ее вместо создания новой.
		/// Хеш = null, значит страница не должна проверятся на дубликат.
		/// </summary>
		string PageHash { get; }

		ViewModelBase ViewModel { get; }

		event EventHandler PageClosed;

		IEnumerable<MasterToSlavePair> SlavePagesAll { get; }
		IEnumerable<ParentToChildPair> ChildPagesAll { get; }

		IEnumerable<IPage> SlavePages { get; }
		IEnumerable<IPage> ChildPages { get; }
	}

	internal interface IPageInternal
	{
		void OnClosed();
		void AddSlavePage(IPage page);
		bool RemoveSlavePage(IPage page);
		void AddChildPage(IPage page);
		bool RemoveChildPage(IPage page);
	}

	public interface IPage<TViewModel> : IPage
		where TViewModel : ViewModelBase
	{
		new TViewModel ViewModel { get; }
	}

	public class MasterToSlavePair
	{
		public IPage MasterPage;
		public IPage SlavePage;
	}

	public class ParentToChildPair
	{
		public IPage ParentPage;
		public IPage ChildPage;
	}
}
