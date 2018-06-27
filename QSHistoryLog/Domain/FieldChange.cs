﻿using System;
using System.ComponentModel.DataAnnotations;
using DiffPlex;
using DiffPlex.DiffBuilder;
using Gamma.Utilities;
using NHibernate.Event;
using QSOrmProject;

namespace QS.HistoryLog.Domain
{
	public class FieldChange
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		#region Свойства

		public virtual int Id { get; set; }

		public virtual ChangedEntity Entity { get; set; }

		public virtual string Path { get; set; }
		public virtual FieldChangeType Type { get; set; }
		public virtual string OldValue { get; set; }
		public virtual string NewValue { get; set; }
		public virtual int? OldId { get; set; }
		public virtual int? NewId { get; set; }

		string oldPangoText;
		public virtual string OldPangoText {
			get {
				if(!isPangoMade)
					MakeDiffPangoMarkup();
				return oldPangoText;
			}
			protected set {
				oldPangoText = value;
			}
		}

		string newPangoText;
		public virtual string NewPangoText {
			get {
				if(!isPangoMade)
					MakeDiffPangoMarkup();
				return newPangoText;
			}
			protected set {
				newPangoText = value;
			}
		}

		private bool isPangoMade = false;

		public virtual string FieldName {
			get { return HistoryMain.ResolveFieldNameFromPath(Path); }
		}

		public virtual string TypeText {
			get {
				return Type.GetEnumTitle();
			}
		}
		#endregion

		public FieldChange()
		{
		}

		#region Внутренние методы

		private void MakeDiffPangoMarkup()
		{
			var d = new Differ();
			var differ = new SideBySideFullDiffBuilder(d);
			var diffRes = differ.BuildDiffModel(OldValue, NewValue);
			OldPangoText = PangoRender.RenderDiffLines(diffRes.OldText);
			NewPangoText = PangoRender.RenderDiffLines(diffRes.NewText);
			isPangoMade = true;
		}

		private void UpdateType()
		{
			if(OldId.HasValue || NewId.HasValue) {
				if(OldId.HasValue && NewId.HasValue)
					Type = FieldChangeType.Changed;
				else if(OldId.HasValue)
					Type = FieldChangeType.Removed;
				else if(NewId.HasValue)
					Type = FieldChangeType.Added;
			} else {
				if(OldValue != null && NewValue != null)
					Type = FieldChangeType.Changed;
				else if(NewValue != null)
					Type = FieldChangeType.Added;
				else if(OldValue != null)
					Type = FieldChangeType.Removed;
			}
		}

		#endregion

		#region Статические методы

		public static FieldChange CheckChange(int i, PostUpdateEvent ue)
		{
			object valueOld = ue.OldState[i];
			object valueNew = ue.State[i];
			if(valueOld == null && valueNew == null)
				return null;

			var propType = ue.Persister.PropertyTypes[i];
			FieldChange change = null;

			// Проверяем все типы
			if(propType is NHibernate.Type.StringType && !StringCompare(ref change, (string)valueOld, (string)valueNew))
				return null;

			var link = propType as NHibernate.Type.ManyToOneType;
			if(link != null) {
				if(!EntityCompare(ref change, valueOld, valueNew))
					return null;
			}

			if(change != null)
			{
				change.Path = ue.Persister.PropertyNames[i];
				change.UpdateType();
				return change;
			}

			logger.Warn("Трекер не умеет сравнивать изменения в полях типа {0}", propType);
			return null;
		}

		#endregion

		#region Методы сравнения для разных типов

		static bool StringCompare(ref FieldChange change, string valueOld, string valueNew)
		{
			if(String.Equals(valueOld, valueNew))
				return false;

			change = new FieldChange();
			change.OldValue = valueOld;
			change.NewValue = valueNew;
			return true;
		}

		static bool EntityCompare(ref FieldChange change, object valueOld, object valueNew)
		{
			if(DomainHelper.EqualDomainObjects(valueOld, valueNew))
				return false;

			change = new FieldChange();
			if(valueOld != null)
			{
				change.OldValue = DomainHelper.GetObjectTilte(valueOld);
				change.OldId = DomainHelper.GetId(valueOld);
			}
			if(valueNew != null)
			{
				change.NewValue = DomainHelper.GetObjectTilte(valueNew);
				change.NewId = DomainHelper.GetId(valueNew);
			}
			return true;
		}

#endregion
	}

	public enum FieldChangeType
	{
		[Display(Name = "Добавлено")]
		Added,
		[Display(Name = "Изменено")]
		Changed,
		[Display(Name = "Очищено")]
		Removed,
		[Display(Name = "Без изменений")]
		Unchanged
	}

	public class FieldChangeTypeStringType : NHibernate.Type.EnumStringType
	{
		public FieldChangeTypeStringType() : base(typeof(FieldChangeType))
		{
		}
	}
}

