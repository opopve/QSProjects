﻿using System;
using System.Collections.Generic;
using System.Linq;
using QS.ViewModels;

namespace QS.Navigation
{
	public class Page<TViewModel> : IPage<TViewModel>, IPageInternal
		where TViewModel : ViewModelBase
	{
		public Page(TViewModel viewModel, string hash)
		{
			ViewModel = viewModel;
			PageHash = hash;
		}

		public TViewModel ViewModel { get; private set; }

		ViewModelBase IPage.ViewModel => ViewModel;

		public event EventHandler PageClosed;

		public string PageHash { get; private set; }

		public IEnumerable<MasterToSlavePair> SlavePagesAll { get {
				foreach (var item in slavePages)
					yield return new MasterToSlavePair { MasterPage = this, SlavePage = item };

				foreach (var pair in ChildPagesAll) {
					foreach (var item in pair.ChildPage.SlavePagesAll) {
						yield return item;
					}
				}
			}
		}

		public IEnumerable<ParentToChildPair> ChildPagesAll {
			get {
				foreach (var child in childPages) {
					yield return new ParentToChildPair { ParentPage = this, ChildPage = child};
					foreach (var item in child.ChildPagesAll) {
						yield return item;
					}
				}
			}
		}

		public IEnumerable<IPage> SlavePages => slavePages;

		public IEnumerable<IPage> ChildPages => childPages;

		#region Приватное

		private readonly List<IPage> slavePages = new List<IPage>();

		private readonly List<IPage> childPages = new List<IPage>();

		#endregion

		#region IPageInternal

		void IPageInternal.OnClosed()
		{
			PageClosed?.Invoke(this, EventArgs.Empty);
		}

		void IPageInternal.AddSlavePage(IPage page)
		{
			slavePages.Add(page);
		}

		bool IPageInternal.RemoveSlavePage(IPage page)
		{
			var result = slavePages.Remove(page);
			if (result)
				return result;

			var pair = SlavePagesAll.FirstOrDefault(x => x.SlavePage == page);
			if (pair == null)
				return false;
			return (pair.MasterPage as IPageInternal).RemoveSlavePage(page);
		}

		void IPageInternal.AddChildPage(IPage page)
		{
			childPages.Add(page);
		}

		bool IPageInternal.RemoveChildPage(IPage page)
		{
			var result = childPages.Remove(page);
			if (result)
				return result;

			var pair = ChildPagesAll.FirstOrDefault(x => x.ChildPage == page);
			if (pair == null)
				return false;
			return (pair.ParentPage as IPageInternal).RemoveChildPage(page);
		}

		#endregion
	}
}
