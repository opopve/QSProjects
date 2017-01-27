
// This file has been generated by the GUI designer. Do not modify.
namespace QSOsm
{
	public partial class JsonAddressEntry
	{
		private global::Gtk.Expander expander1;
		
		private global::Gtk.Table table1;
		
		private global::QSOsm.HouseEntry entryBuilding;
		
		private global::QSOsm.CityEntry entryCity;
		
		private global::QSOsm.StreetEntry entryStreet;
		
		private global::Gtk.HBox hbox9;
		
		private global::Gamma.Widgets.yEnumComboBox comboRoomType;
		
		private global::Gtk.Label label5;
		
		private global::Gamma.GtkWidgets.yEntry entryRoom;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.Label label4;
		
		private global::Gtk.Label label6;
		
		private global::Gtk.Label label7;
		
		private global::Gtk.Label label8;
		
		private global::Gtk.Label label9;
		
		private global::Gamma.GtkWidgets.ySpinButton spinFloor;
		
		private global::Gamma.GtkWidgets.yEntry yentryAddition;
		
		private global::Gamma.GtkWidgets.yLabel ylabelRegion;
		
		private global::Gtk.Label ExpanderLabel;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget QSOsm.JsonAddressEntry
			global::Stetic.BinContainer.Attach (this);
			this.Name = "QSOsm.JsonAddressEntry";
			// Container child QSOsm.JsonAddressEntry.Gtk.Container+ContainerChild
			this.expander1 = new global::Gtk.Expander (null);
			this.expander1.CanFocus = true;
			this.expander1.Name = "expander1";
			this.expander1.Expanded = true;
			// Container child expander1.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(4)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryBuilding = new global::QSOsm.HouseEntry ();
			this.entryBuilding.CanFocus = true;
			this.entryBuilding.Name = "entryBuilding";
			this.entryBuilding.IsEditable = true;
			this.entryBuilding.WidthChars = 10;
			this.entryBuilding.InvisibleChar = '●';
			this.table1.Add (this.entryBuilding);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryBuilding]));
			w1.TopAttach = ((uint)(1));
			w1.BottomAttach = ((uint)(2));
			w1.LeftAttach = ((uint)(3));
			w1.RightAttach = ((uint)(4));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryCity = new global::QSOsm.CityEntry ();
			this.entryCity.CanFocus = true;
			this.entryCity.Name = "entryCity";
			this.entryCity.IsEditable = true;
			this.entryCity.InvisibleChar = '●';
			this.table1.Add (this.entryCity);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryCity]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryStreet = new global::QSOsm.StreetEntry ();
			this.entryStreet.CanFocus = true;
			this.entryStreet.Name = "entryStreet";
			this.entryStreet.IsEditable = true;
			this.entryStreet.InvisibleChar = '●';
			this.entryStreet.CityId = 0;
			this.table1.Add (this.entryStreet);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.entryStreet]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox9 = new global::Gtk.HBox ();
			this.hbox9.Name = "hbox9";
			this.hbox9.Spacing = 6;
			// Container child hbox9.Gtk.Box+BoxChild
			this.comboRoomType = new global::Gamma.Widgets.yEnumComboBox ();
			this.comboRoomType.Name = "comboRoomType";
			this.comboRoomType.ShowSpecialStateAll = false;
			this.comboRoomType.ShowSpecialStateNot = false;
			this.comboRoomType.UseShortTitle = false;
			this.comboRoomType.DefaultFirst = false;
			this.hbox9.Add (this.comboRoomType);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.comboRoomType]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox9.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("№:");
			this.hbox9.Add (this.label5);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.label5]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox9.Gtk.Box+BoxChild
			this.entryRoom = new global::Gamma.GtkWidgets.yEntry ();
			this.entryRoom.CanFocus = true;
			this.entryRoom.Name = "entryRoom";
			this.entryRoom.IsEditable = true;
			this.entryRoom.MaxLength = 20;
			this.entryRoom.InvisibleChar = '●';
			this.hbox9.Add (this.entryRoom);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.entryRoom]));
			w6.Position = 2;
			this.table1.Add (this.hbox9);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.hbox9]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Регион:");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w8.LeftAttach = ((uint)(2));
			w8.RightAttach = ((uint)(3));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Город:");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.Xalign = 1F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Улица:");
			this.table1.Add (this.label6);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.label6]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Дом:");
			this.table1.Add (this.label7);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.label7]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.LeftAttach = ((uint)(2));
			w11.RightAttach = ((uint)(3));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.Xalign = 1F;
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Дополнение:");
			this.table1.Add (this.label8);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1 [this.label8]));
			w12.TopAttach = ((uint)(3));
			w12.BottomAttach = ((uint)(4));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label ();
			this.label9.Name = "label9";
			this.label9.Xalign = 1F;
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString ("Этаж:");
			this.table1.Add (this.label9);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1 [this.label9]));
			w13.TopAttach = ((uint)(2));
			w13.BottomAttach = ((uint)(3));
			w13.LeftAttach = ((uint)(2));
			w13.RightAttach = ((uint)(3));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinFloor = new global::Gamma.GtkWidgets.ySpinButton (-5, 100, 1);
			this.spinFloor.WidthRequest = 0;
			this.spinFloor.HeightRequest = 0;
			this.spinFloor.CanFocus = true;
			this.spinFloor.Name = "spinFloor";
			this.spinFloor.Adjustment.PageIncrement = 10;
			this.spinFloor.ClimbRate = 1;
			this.spinFloor.Numeric = true;
			this.spinFloor.Value = 4;
			this.spinFloor.ValueAsDecimal = 0m;
			this.spinFloor.ValueAsInt = 0;
			this.table1.Add (this.spinFloor);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinFloor]));
			w14.TopAttach = ((uint)(2));
			w14.BottomAttach = ((uint)(3));
			w14.LeftAttach = ((uint)(3));
			w14.RightAttach = ((uint)(4));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryAddition = new global::Gamma.GtkWidgets.yEntry ();
			this.yentryAddition.CanFocus = true;
			this.yentryAddition.Name = "yentryAddition";
			this.yentryAddition.IsEditable = true;
			this.yentryAddition.MaxLength = 100;
			this.yentryAddition.InvisibleChar = '●';
			this.table1.Add (this.yentryAddition);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1 [this.yentryAddition]));
			w15.TopAttach = ((uint)(3));
			w15.BottomAttach = ((uint)(4));
			w15.LeftAttach = ((uint)(1));
			w15.RightAttach = ((uint)(4));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelRegion = new global::Gamma.GtkWidgets.yLabel ();
			this.ylabelRegion.Name = "ylabelRegion";
			this.ylabelRegion.LabelProp = global::Mono.Unix.Catalog.GetString ("не определён");
			this.table1.Add (this.ylabelRegion);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1 [this.ylabelRegion]));
			w16.LeftAttach = ((uint)(3));
			w16.RightAttach = ((uint)(4));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			this.expander1.Add (this.table1);
			this.ExpanderLabel = new global::Gtk.Label ();
			this.ExpanderLabel.Name = "ExpanderLabel";
			this.ExpanderLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("АДРЕС ПУСТОЙ");
			this.ExpanderLabel.Selectable = true;
			this.expander1.LabelWidget = this.ExpanderLabel;
			this.Add (this.expander1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
