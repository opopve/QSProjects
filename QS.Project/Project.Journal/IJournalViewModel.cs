using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;

namespace QS.Project.Journal
{
	public interface IJournalViewModel : INotifyPropertyChanged
	{
		IJournalFilter Filter { get; }
		IJournalSearch Search { get; }
		void Refresh();
		bool TryLoad();
		IList Items { get; }
		event EventHandler ItemsListUpdated;
		string FooterInfo { get; }
		IEnumerable<IJournalAction> NodeActions { get; }
		IEnumerable<IJournalAction> PopupActions { get; }
	}
}
