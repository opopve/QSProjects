﻿using System;
using System.Collections;
using System.ComponentModel;
using Gamma.Utilities;
using Gtk;
using NLog;
using QS.DomainModel.Entity;
using QSProjectsLib;

namespace Gamma.Widgets
{
	[ToolboxItem (true)]
	[Category ("QS Widgets")]
	[Obsolete("Используйте виджет QS.Widgets.GtkUI.SpecialListComboBox")]
	public class ySpecComboBox : yListComboBox
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public override object SelectedItem {
			get {
				if(SelectedItemStrictTyped)
					return base.SelectedItem is SpecialComboState ? null : base.SelectedItem;
				else
					return base.SelectedItem;
			}
			set {
				if (base.SelectedItem == value)
					return;

				TreeIter iter;
				if (value == null)
				{
					if (ShowSpecialStateAll)
						base.SelectedItem = SpecialComboState.All;
					else if(ShowSpecialStateNot)
						base.SelectedItem = SpecialComboState.Not;
					else
						base.SelectedItem = null;
				}
				if (value is IDomainObject) {
					if (ListStoreWorks.SearchListStore<IDomainObject> ((ListStore)Model, item => item.Id == ((IDomainObject)value).Id, 1, out iter))
					{
						base.SelectedItem = ((ListStore)Model).GetValue (iter, (int)comboDataColumns.Item);
					}
					else if(AddIfNotExist)
						base.SelectedItem = value;
				} else {
					base.SelectedItem = value;
				}
			}
		}

		public bool IsSelectedAll{
			get{
				return SpecialComboState.All.Equals(base.SelectedItem);
			}
		}

		public bool IsSelectedNot
		{
			get
			{
				return SpecialComboState.Not.Equals(base.SelectedItem);
			}
		}

		/// <summary>
		/// При установки этого значения в true SelectedItem будет возвращать только значения из списка.
		/// Служебные значения enum-а SpecialComboState будут преобразовываться в null.
		/// </summary>
		public bool SelectedItemStrictTyped = true;

		private bool showSpecialStateAll = false;
		[Browsable(true)]
		public bool ShowSpecialStateAll {
			get {
				return showSpecialStateAll;
			}
			set {
				showSpecialStateAll = value;
				ResetLayout ();
			}
		}

		private bool showSpecialStateNot = false;
		[Browsable(true)]
		public bool ShowSpecialStateNot {
			get {
				return showSpecialStateNot;
			}
			set {
				showSpecialStateNot = value;
				ResetLayout();
			}
		}

		[Browsable(true)]
		public string NameForSpecialStateNot { get; set; } = null;

		IEnumerable itemsList;

		public override IEnumerable ItemsList {
			get{
				if (ShowSpecialStateAll)
					yield return SpecialComboState.All;
				if (ShowSpecialStateNot)
					yield return SpecialComboState.Not;
				if (itemsList == null)
					yield break;
				foreach (var item in itemsList)
					yield return item;
			}
			set {
				if(itemsList == value)
					return;
				itemsList = value;
				ResetLayout ();
			}
		}

		public new Func<object, string> RenderTextFunc;

		public ySpecComboBox () : base()
		{
			base.RenderTextFunc = RenderText;
		}

		protected override void ResetLayout()
		{
			base.ResetLayout ();
			if (ShowSpecialStateAll || ShowSpecialStateNot)
				Active = 0;
		}

		public override void SetRenderTextFunc<TObject> (Func<TObject, string> renderTextFunc)
		{
			RenderTextFunc = o => renderTextFunc ((TObject)o);
			ResetLayout ();
		}

		private string RenderText(object item)
		{
			if(item is SpecialComboState specialState) {
				if(!string.IsNullOrEmpty(NameForSpecialStateNot) && specialState == SpecialComboState.Not)
					return NameForSpecialStateNot;
				return specialState.GetEnumTitle();
			}
			if(RenderTextFunc != null)
				return RenderTextFunc (item);
			return DomainHelper.GetObjectTilte (item);
		}
	}
}

