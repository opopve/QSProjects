
// This file has been generated by the GUI designer. Do not modify.
namespace QS.Widgets.Gtk
{
	public partial class UserPermissionWidget
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.Notebook notebook;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget QS.Widgets.Gtk.UserPermissionWidget
			global::Stetic.BinContainer.Attach(this);
			this.Name = "QS.Widgets.Gtk.UserPermissionWidget";
			// Container child QS.Widgets.Gtk.UserPermissionWidget.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.notebook = new global::Gtk.Notebook();
			this.notebook.CanFocus = true;
			this.notebook.Name = "notebook";
			this.notebook.CurrentPage = -1;
			this.vbox1.Add(this.notebook);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.notebook]));
			w1.Position = 0;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
