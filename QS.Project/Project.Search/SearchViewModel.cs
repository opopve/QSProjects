using System;
using QS.Services;
using QS.ViewModels;
using QS.Project.Journal;
namespace QS.Project.Search
{
	public class SearchViewModel : WidgetViewModelBase, IJournalSearch
	{
		#region IJournalSearch implementation

		public event EventHandler OnSearch;

		public string[] GetValuesToSearch() => new[] { SearchString };

		public void Update() => OnSearch?.Invoke(this, new EventArgs());

		#endregion IJournalSearch implementation

		public SearchViewModel(IInteractiveService interactiveService) : base(interactiveService)
		{ }

		string searchString;
		public string SearchString {
			get => searchString;
			set => SetField(ref searchString, value, () => SearchString);
		}
	}
}
