using System;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Services;
using QS.ViewModels;

namespace QS.Project.Filter
{
	public abstract class FilterViewModelBase<TFilter> : WidgetViewModelBase, IDisposable, IJournalFilter
		where TFilter : FilterViewModelBase<TFilter>
	{
		public event EventHandler OnFiltered;

		IUnitOfWork uow;

		private bool canNotify = true;

		public IUnitOfWork UoW {
			get => uow;
			set {
				uow = value;
				ConfigureWithUow();
			}
		}

		protected FilterViewModelBase(IInteractiveService interactiveService) : base(interactiveService)
		{
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			PropertyChanged += (sender, e) => OnFiltered?.Invoke(sender, e);
		}

		public void Update()
		{
			if(canNotify)
				OnFiltered?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Для установки свойств фильтра без перезапуска фильтрации на каждом изменении
		/// обновления журналов при каждом выставлении ограничения.
		/// </summary>
		/// <param name="setters">Лямбды ограничений</param>
		public void SetAndRefilterAtOnce(params Action<TFilter>[] setters)
		{
			canNotify = false;
			TFilter filter = this as TFilter;
			foreach(var item in setters) {
				item(filter);
			}
			canNotify = true;
			Update();
		}

		protected virtual void ConfigureWithUow() { }

		public void Dispose() => UoW?.Dispose();
	}
}