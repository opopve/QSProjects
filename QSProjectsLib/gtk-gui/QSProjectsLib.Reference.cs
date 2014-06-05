
// This file has been generated by the GUI designer. Do not modify.
namespace QSProjectsLib
{
	public partial class Reference
	{
		private global::Gtk.UIManager UIManager;
		private global::Gtk.Action addAction1;
		private global::Gtk.Action editAction1;
		private global::Gtk.Action removeAction1;
		private global::Gtk.VBox vbox2;
		private global::Gtk.Toolbar toolbar1;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Label label1;
		private global::Gtk.Entry entryFilter;
		private global::Gtk.Button buttonClean;
		private global::Gtk.HBox hboxOrdinal;
		private global::Gtk.Label label2;
		private global::Gtk.Button buttonOrdinalUp;
		private global::Gtk.Button buttonOrdinalDown;
		private global::Gtk.EventBox eventboxOrdinalInfo;
		private global::Gtk.HBox hboxOrdinalInfo;
		private global::Gtk.Label label3;
		private global::Gtk.Button buttonSaveOrdinal;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView treeviewref;
		private global::Gtk.Button buttonClose;
		private global::Gtk.Button buttonCancel;
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget QSProjectsLib.Reference
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
			this.addAction1 = new global::Gtk.Action ("addAction1", null, null, "gtk-add");
			this.addAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Добавить");
			w1.Add (this.addAction1, null);
			this.editAction1 = new global::Gtk.Action ("editAction1", null, null, "gtk-edit");
			this.editAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Изменить");
			w1.Add (this.editAction1, null);
			this.removeAction1 = new global::Gtk.Action ("removeAction1", null, null, "gtk-remove");
			this.removeAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Удалить");
			w1.Add (this.removeAction1, null);
			this.UIManager.InsertActionGroup (w1, 0);
			this.AddAccelGroup (this.UIManager.AccelGroup);
			this.Name = "QSProjectsLib.Reference";
			this.Title = global::Mono.Unix.Catalog.GetString ("Справочник");
			this.Icon = global::Stetic.IconLoader.LoadIcon (this, "gtk-directory", global::Gtk.IconSize.LargeToolbar);
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child QSProjectsLib.Reference.VBox
			global::Gtk.VBox w2 = this.VBox;
			w2.Name = "dialog1_VBox";
			w2.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><toolbar name='toolbar1'><toolitem name='addAction1' action='addAction1'/><toolitem name='editAction1' action='editAction1'/><toolitem name='removeAction1' action='removeAction1'/></toolbar></ui>");
			this.toolbar1 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar1")));
			this.toolbar1.Name = "toolbar1";
			this.toolbar1.ShowArrow = false;
			this.toolbar1.ToolbarStyle = ((global::Gtk.ToolbarStyle)(2));
			this.toolbar1.IconSize = ((global::Gtk.IconSize)(3));
			this.vbox2.Add (this.toolbar1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.toolbar1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Быстрый поиск:");
			this.hbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryFilter = new global::Gtk.Entry ();
			this.entryFilter.CanFocus = true;
			this.entryFilter.Name = "entryFilter";
			this.entryFilter.IsEditable = true;
			this.entryFilter.InvisibleChar = '●';
			this.hbox1.Add (this.entryFilter);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryFilter]));
			w5.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonClean = new global::Gtk.Button ();
			this.buttonClean.TooltipMarkup = "Очистить";
			this.buttonClean.CanFocus = true;
			this.buttonClean.Name = "buttonClean";
			this.buttonClean.UseUnderline = true;
			global::Gtk.Image w6 = new global::Gtk.Image ();
			w6.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-clear", global::Gtk.IconSize.Menu);
			this.buttonClean.Image = w6;
			this.hbox1.Add (this.buttonClean);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.buttonClean]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.hboxOrdinal = new global::Gtk.HBox ();
			this.hboxOrdinal.Name = "hboxOrdinal";
			this.hboxOrdinal.Spacing = 6;
			// Container child hboxOrdinal.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Порядок");
			this.hboxOrdinal.Add (this.label2);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hboxOrdinal [this.label2]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hboxOrdinal.Gtk.Box+BoxChild
			this.buttonOrdinalUp = new global::Gtk.Button ();
			this.buttonOrdinalUp.CanFocus = true;
			this.buttonOrdinalUp.Name = "buttonOrdinalUp";
			this.buttonOrdinalUp.UseUnderline = true;
			global::Gtk.Image w9 = new global::Gtk.Image ();
			w9.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-up", global::Gtk.IconSize.Menu);
			this.buttonOrdinalUp.Image = w9;
			this.hboxOrdinal.Add (this.buttonOrdinalUp);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hboxOrdinal [this.buttonOrdinalUp]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hboxOrdinal.Gtk.Box+BoxChild
			this.buttonOrdinalDown = new global::Gtk.Button ();
			this.buttonOrdinalDown.CanFocus = true;
			this.buttonOrdinalDown.Name = "buttonOrdinalDown";
			this.buttonOrdinalDown.UseUnderline = true;
			global::Gtk.Image w11 = new global::Gtk.Image ();
			w11.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-down", global::Gtk.IconSize.Menu);
			this.buttonOrdinalDown.Image = w11;
			this.hboxOrdinal.Add (this.buttonOrdinalDown);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hboxOrdinal [this.buttonOrdinalDown]));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
			this.hbox1.Add (this.hboxOrdinal);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.hboxOrdinal]));
			w13.Position = 3;
			w13.Expand = false;
			w13.Fill = false;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox1]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.eventboxOrdinalInfo = new global::Gtk.EventBox ();
			this.eventboxOrdinalInfo.Name = "eventboxOrdinalInfo";
			// Container child eventboxOrdinalInfo.Gtk.Container+ContainerChild
			this.hboxOrdinalInfo = new global::Gtk.HBox ();
			this.hboxOrdinalInfo.Name = "hboxOrdinalInfo";
			this.hboxOrdinalInfo.Spacing = 6;
			this.hboxOrdinalInfo.BorderWidth = ((uint)(3));
			// Container child hboxOrdinalInfo.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Порядок сортировки был изменен.");
			this.hboxOrdinalInfo.Add (this.label3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hboxOrdinalInfo [this.label3]));
			w15.Position = 0;
			// Container child hboxOrdinalInfo.Gtk.Box+BoxChild
			this.buttonSaveOrdinal = new global::Gtk.Button ();
			this.buttonSaveOrdinal.CanFocus = true;
			this.buttonSaveOrdinal.Name = "buttonSaveOrdinal";
			this.buttonSaveOrdinal.UseUnderline = true;
			this.buttonSaveOrdinal.Xalign = 1F;
			this.buttonSaveOrdinal.Label = global::Mono.Unix.Catalog.GetString ("Сохранить");
			global::Gtk.Image w16 = new global::Gtk.Image ();
			w16.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			this.buttonSaveOrdinal.Image = w16;
			this.hboxOrdinalInfo.Add (this.buttonSaveOrdinal);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hboxOrdinalInfo [this.buttonSaveOrdinal]));
			w17.PackType = ((global::Gtk.PackType)(1));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			this.eventboxOrdinalInfo.Add (this.hboxOrdinalInfo);
			this.vbox2.Add (this.eventboxOrdinalInfo);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.eventboxOrdinalInfo]));
			w19.Position = 2;
			w19.Expand = false;
			w19.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewref = new global::Gtk.TreeView ();
			this.treeviewref.CanFocus = true;
			this.treeviewref.Name = "treeviewref";
			this.GtkScrolledWindow.Add (this.treeviewref);
			this.vbox2.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
			w21.Position = 3;
			w2.Add (this.vbox2);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(w2 [this.vbox2]));
			w22.Position = 0;
			// Internal child QSProjectsLib.Reference.ActionArea
			global::Gtk.HButtonBox w23 = this.ActionArea;
			w23.Name = "dialog1_ActionArea";
			w23.Spacing = 10;
			w23.BorderWidth = ((uint)(5));
			w23.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonClose = new global::Gtk.Button ();
			this.buttonClose.CanFocus = true;
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.UseUnderline = true;
			this.buttonClose.Label = global::Mono.Unix.Catalog.GetString ("_Закрыть");
			global::Gtk.Image w24 = new global::Gtk.Image ();
			w24.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-close", global::Gtk.IconSize.Menu);
			this.buttonClose.Image = w24;
			this.AddActionWidget (this.buttonClose, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w25 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonClose]));
			w25.Expand = false;
			w25.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("О_тменить");
			global::Gtk.Image w26 = new global::Gtk.Image ();
			w26.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w26;
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w27 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonCancel]));
			w27.Position = 1;
			w27.Expand = false;
			w27.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.Sensitive = false;
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = global::Mono.Unix.Catalog.GetString ("_OK");
			global::Gtk.Image w28 = new global::Gtk.Image ();
			w28.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-ok", global::Gtk.IconSize.Menu);
			this.buttonOk.Image = w28;
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w29 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonOk]));
			w29.Position = 2;
			w29.Expand = false;
			w29.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 465;
			this.DefaultHeight = 455;
			this.hboxOrdinal.Hide ();
			this.eventboxOrdinalInfo.Hide ();
			this.Show ();
			this.addAction1.Activated += new global::System.EventHandler (this.OnAddActionActivated);
			this.editAction1.Activated += new global::System.EventHandler (this.OnEditActionActivated);
			this.removeAction1.Activated += new global::System.EventHandler (this.OnRemoveActionActivated);
			this.entryFilter.Changed += new global::System.EventHandler (this.OnEntryFilterChanged);
			this.buttonClean.Clicked += new global::System.EventHandler (this.OnButtonCleanClicked);
			this.buttonOrdinalUp.Clicked += new global::System.EventHandler (this.OnButtonOrdinalUpClicked);
			this.buttonOrdinalDown.Clicked += new global::System.EventHandler (this.OnButtonOrdinalDownClicked);
			this.buttonSaveOrdinal.Clicked += new global::System.EventHandler (this.OnButtonSaveOrdinalClicked);
			this.treeviewref.CursorChanged += new global::System.EventHandler (this.OnTreeviewrefCursorChanged);
			this.treeviewref.RowActivated += new global::Gtk.RowActivatedHandler (this.OnTreeviewrefRowActivated);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}
