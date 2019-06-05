using System;
using QS.DomainModel.Config;
using QS.Services;
using QS.DomainModel.Entity;
using NHibernate;

namespace QS.Project.Journal
{
	public abstract class SingleEntityJournalViewModelBase<TEntity, TNode, TFilter> : EntityJournalViewModelBase<TNode, TFilter>
		where TEntity : class, IDomainObject
		where TNode : JournalEntityNodeBase
		where TFilter : IJournalFilter
	{
		private readonly ICommonServices commonServices;
		private readonly Type entityType;

		public SingleEntityJournalViewModelBase(IEntityConfigurationProvider entityConfigurationProvider, ICommonServices commonServices) : base(entityConfigurationProvider, commonServices)
		{
			this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			entityType = typeof(TEntity);
			EntityTypes.Add(entityType);
			UpdateAllEntityPermissions();
			if(!GetEntityPermission(entityType).CanRead) {
				AbortOpening($"Нет прав для просмотра документов типа: {entityType.GetSubjectName()}", "Невозможно открыть журнал");
			}
		}

		protected abstract Func<IQueryOver<TEntity>> ItemsSourceQueryFunction { get; set; }
	}
}
