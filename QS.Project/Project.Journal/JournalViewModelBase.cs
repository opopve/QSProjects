using System;
using System.Collections;
using System.Collections.Generic;
using QS.Services;
using QS.ViewModels;

namespace QS.Project.Journal
{
	public abstract class JournalViewModelBase<TFilter> : TabViewModelBase, IJournalViewModel
		where TFilter : IJournalFilter
	{
		public virtual TFilter Filter { get; set; }

		IJournalFilter IJournalViewModel.Filter => Filter;

		public IJournalSearch Search { get; set; }

		public IList Items { get; set; }

		public event EventHandler ItemsListUpdated;

		public string FooterInfo { get; set; }

		public IEnumerable<IJournalAction> NodeActions => NodeActionsList;

		protected List<IJournalAction> NodeActionsList { get; set; }

		public IEnumerable<IJournalAction> PopupActions => PopupActionsList;

		protected List<IJournalAction> PopupActionsList { get; set; }

		public abstract void Refresh();

		public abstract bool TryLoad();

		protected JournalViewModelBase(IInteractiveService interactiveService) : base(interactiveService)
		{
			NodeActionsList = new List<IJournalAction>();
			PopupActionsList = new List<IJournalAction>();
		}

		protected virtual void UpdateItems(IList items)
		{
			Items = items;
			ItemsListUpdated?.Invoke(this, EventArgs.Empty);
		}
	}
}
