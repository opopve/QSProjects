
// This file has been generated by the GUI designer. Do not modify.
namespace QS.Validation.GtkUI
{
	public partial class ResultsListDlg
	{
		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.VBox vboxMessages;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget QS.Validation.GtkUI.ResultsListDlg
			this.Name = "QS.Validation.GtkUI.ResultsListDlg";
			this.Title = global::Mono.Unix.Catalog.GetString("Проверка корректности данных");
			this.Icon = global::Stetic.IconLoader.LoadIcon(this, "gtk-apply", global::Gtk.IconSize.Menu);
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			// Internal child QS.Validation.GtkUI.ResultsListDlg.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			global::Gtk.Viewport w2 = new global::Gtk.Viewport();
			w2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport1.Gtk.Container+ContainerChild
			this.vboxMessages = new global::Gtk.VBox();
			this.vboxMessages.Name = "vboxMessages";
			this.vboxMessages.Spacing = 10;
			this.vboxMessages.BorderWidth = ((uint)(6));
			w2.Add(this.vboxMessages);
			this.GtkScrolledWindow.Add(w2);
			w1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(w1[this.GtkScrolledWindow]));
			w5.PackType = ((global::Gtk.PackType)(1));
			w5.Position = 1;
			// Internal child QS.Validation.GtkUI.ResultsListDlg.ActionArea
			global::Gtk.HButtonBox w6 = this.ActionArea;
			w6.Name = "dialog1_ActionArea";
			w6.Spacing = 10;
			w6.BorderWidth = ((uint)(5));
			w6.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(1));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-close";
			this.AddActionWidget(this.buttonOk, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6[this.buttonOk]));
			w7.Expand = false;
			w7.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 436;
			this.DefaultHeight = 335;
			this.Show();
		}
	}
}
