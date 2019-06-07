using System;
namespace QS.Project.Search.GtkUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SearchView : Gtk.Bin
	{
		SearchViewModel viewModel;
		public SearchView(SearchViewModel viewModel)
		{
			this.Build();
			this.viewModel = viewModel;
			ConfigureDlg();
		}

		void ConfigureDlg() {
			entSearchText.Binding.AddBinding(viewModel, vm => vm.SearchString, w => w.Text).InitializeFromSource();
		}

		public event EventHandler TextChanged;

		protected void OnBtnClearClicked(object sender, EventArgs e)
		{
			viewModel.SearchString = string.Empty;
		}

		protected void OnEntSearchTextChanged(object sender, EventArgs e)
		{
			TextChanged?.Invoke(this, e);
			viewModel.Update();
		}
	}
}