
// This file has been generated by the GUI designer. Do not modify.
namespace QSPhones
{
	public partial class PhonesView
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.DataBindings.DataTable datatablePhones;
		
		private global::Gtk.Button buttonAdd;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget QSPhones.PhonesView
			global::Stetic.BinContainer.Attach (this);
			this.Name = "QSPhones.PhonesView";
			// Container child QSPhones.PhonesView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			// Container child vbox1.Gtk.Box+BoxChild
			this.datatablePhones = new global::Gtk.DataBindings.DataTable (((uint)(1)), ((uint)(6)), false);
			this.datatablePhones.Name = "datatablePhones";
			this.datatablePhones.ColumnSpacing = ((uint)(6));
			this.datatablePhones.InheritedDataSource = false;
			this.datatablePhones.InheritedBoundaryDataSource = false;
			this.datatablePhones.InheritedDataSource = false;
			this.datatablePhones.InheritedBoundaryDataSource = false;
			this.vbox1.Add (this.datatablePhones);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.datatablePhones]));
			w1.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.buttonAdd = new global::Gtk.Button ();
			this.buttonAdd.Sensitive = false;
			this.buttonAdd.CanFocus = true;
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.UseUnderline = true;
			this.buttonAdd.Label = global::Mono.Unix.Catalog.GetString ("Добавить");
			global::Gtk.Image w2 = new global::Gtk.Image ();
			w2.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			this.buttonAdd.Image = w2;
			this.vbox1.Add (this.buttonAdd);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.buttonAdd]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonAdd.Clicked += new global::System.EventHandler (this.OnButtonAddClicked);
		}
	}
}
