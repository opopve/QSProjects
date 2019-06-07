using System;
using System.Collections.Generic;
using QS.DomainModel.Config;
using QS.DomainModel.Entity;
using QS.Services;
using QS.Tdi;
using System.Linq;
using QS.Deletion;
using System.ComponentModel;
using NHibernate;

namespace QS.Project.Journal
{
	public abstract class EntityJournalViewModelBase<TNode, TFilter> : JournalViewModelBase<TFilter>
		where TNode : JournalEntityNodeBase
		where TFilter : IJournalFilter
	{
		private readonly IEntityConfigurationProvider entityConfigurationProvider;
		private readonly ICommonServices commonServices;

		protected Dictionary<Type, JournalEntityConfig<TNode>> EntityConfigs { get; private set; }
		protected bool DynamicLoadingEnabled { get; set; }

		protected EntityJournalViewModelBase(IEntityConfigurationProvider entityConfigurationProvider, ICommonServices commonServices) : base(commonServices?.InteractiveService)
		{
			this.entityConfigurationProvider = entityConfigurationProvider ?? throw new ArgumentNullException(nameof(entityConfigurationProvider));
			this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			EntityConfigs = new Dictionary<Type, JournalEntityConfig<TNode>>();
			DynamicLoadingEnabled = true;
		}


		#region Order

		private bool isDescOrder = false;
		private Func<TNode, object> OrderFunction = x => x.Id;

		protected void SetOrder<TKey>(Func<TNode, object> orderFunc, bool desc = false)
		{
			OrderFunction = orderFunc;
			isDescOrder = desc;
		}

		#endregion Order

		protected JournalEntityConfigurator<TEntity, TNode> RegisterEntity<TEntity>(Func<IQueryOver<TEntity>> queryFunction)
			where TEntity : class, IDomainObject, INotifyPropertyChanged, new()
		{
			if(queryFunction == null) {
				throw new ArgumentNullException(nameof(queryFunction));
			}
			AddQuery(queryFunction);

			var configurator = new JournalEntityConfigurator<TEntity, TNode>();
			configurator.OnConfigurationFinished += (sender, e) => {
				var config = e.Config;
				if(EntityConfigs.ContainsKey(config.EntityType)) {
					throw new InvalidOperationException($"Конфигурация для сущности ({config.EntityType.Name}) уже была добавлена.");
				}
				EntityConfigs.Add(config.EntityType, config);
			};
			return configurator;
		}

		protected void EndConfiguration()
		{
			UpdateAllEntityPermissions(); 
			CreateNodeActions();
			CreatePopupActions();
		}

		public sealed override void Refresh()
		{
			ClearCachedItems();
			TryLoad();
		}

		protected virtual void ClearCachedItems()
		{
			allRowsCount = null;
			loadedCount = 0;
			cachedItemsList.Clear();
		}

		#region Dynamic load

		protected int PageSize { get; set; }
		List<TNode> cachedItemsList = new List<TNode>();

		private int? allRowsCount = null;
		private int loadedCount = 0;

		public override bool TryLoad()
		{
			if(!EntityConfigs.Any()) {
				return false;
			}

			List<TNode> summaryItems = new List<TNode>();

			if(!DynamicLoadingEnabled) {
				foreach(var entityType in EntityConfigs.Keys) {
					summaryItems.AddRange(LoadAll(entityType));
				}
				OrderAndUpdateItems(summaryItems);
				return false;
			}

			if(allRowsCount.HasValue && allRowsCount.Value == loadedCount) {
				return false;
			}
			int localAllRowCount = 0;
			int currentPageSize = PageSize / EntityConfigs.Count;
			int currentLoadedCount = 0;
			foreach(var entityType in EntityConfigs.Keys) {
				var loadResult = Load(entityType, currentPageSize);
				localAllRowCount += loadResult.AllRowsCount;
				summaryItems.AddRange(loadResult.LoadedItems);
				currentLoadedCount += loadResult.LoadedRowsCount;
			}
			loadedCount = currentLoadedCount;

			if(!allRowsCount.HasValue) {
				allRowsCount = localAllRowCount;
			}

			OrderAndUpdateItems(summaryItems);

			return !allRowsCount.HasValue || allRowsCount.Value >= loadedCount;
		}

		private void OrderAndUpdateItems(List<TNode> items)
		{
			if(isDescOrder) {
				cachedItemsList.AddRange(items.OrderByDescending(OrderFunction).ToList());
			} else {
				cachedItemsList.AddRange(items.OrderBy(OrderFunction).ToList());
			}

			UpdateItems(cachedItemsList);
		}

		#region Entity load configuration

		private Dictionary<Type, EntityLoadInfo> entityLoadInfoItems = new Dictionary<Type, EntityLoadInfo>();

		private void AddQuery<TEntity>(Func<IQueryOver<TEntity>> queryFunc)
			where TEntity : class, IDomainObject, INotifyPropertyChanged, new()
		{
			var entityType = typeof(TEntity);
			if(entityLoadInfoItems.ContainsKey(entityType)) {
				return;
			}
			var query = queryFunc.Invoke();
			var info = new EntityLoadInfo(null, 0, 
				(skip, take) => query.Skip(skip).Take(take).Future<TNode>(), 
				() => query.Clone().ClearOrders().FutureValue<int>(),
				() => query.Future<TNode>()
			);
			entityLoadInfoItems.Add(entityType, info);
		}

		private EntityLoadResult Load(Type entityType, int pageSize)
		{
			if(!entityLoadInfoItems.ContainsKey(entityType)) {
				return null;
			}
			var entityLoadInfo = entityLoadInfoItems[entityType];
			if(entityLoadInfo.AllRowsCount.HasValue && entityLoadInfo.LoadedRowsCount >= entityLoadInfo.AllRowsCount.Value) {
				return null;
			}

			entityLoadInfo.EvaluateRowCount();

			var currentPageSize = (entityLoadInfo.AllRowsCount.Value - entityLoadInfo.LoadedRowsCount) < pageSize ? (entityLoadInfo.AllRowsCount.Value - entityLoadInfo.LoadedRowsCount) : pageSize;
			if(currentPageSize < 0) {
				currentPageSize = 0;
			}
			List<TNode> loadedItems = entityLoadInfo.GetItems(entityLoadInfo.LoadedRowsCount, currentPageSize);
			entityLoadInfo.LoadedRowsCount += currentPageSize;

			EntityLoadResult result = new EntityLoadResult(entityLoadInfo.AllRowsCount.Value, entityLoadInfo.LoadedRowsCount, loadedItems);
			return result;
		}

		private List<TNode> LoadAll(Type entityType)
		{
			if(!entityLoadInfoItems.ContainsKey(entityType)) {
				return null;
			}
			var entityLoadInfo = entityLoadInfoItems[entityType];
			return entityLoadInfo.GetAllItems();
		}

		private class EntityLoadResult
		{
			public int AllRowsCount { get; private set; }
			public int LoadedRowsCount { get; private set; }
			public List<TNode> LoadedItems { get; private set; }

			public EntityLoadResult(int allRowsCount, int loadedRowsCount, List<TNode> loadedItems)
			{
				AllRowsCount = allRowsCount;
				LoadedRowsCount = loadedRowsCount;
				LoadedItems = loadedItems;
			}
		}

		private class EntityLoadInfo
		{
			private readonly Func<int, int, IFutureEnumerable<TNode>> itemsQueryFunc;
			private readonly Func<IFutureValue<int>> rowCountQueryFunc;
			private readonly Func<IFutureEnumerable<TNode>> allItemsQueryFunc;

			public int? AllRowsCount { get; private set; }
			public int LoadedRowsCount { get; set; }
			private IFutureEnumerable<TNode> itemsQuery;
			private IFutureValue<int> rowCountQuery;

			public void EvaluateRowCount()
			{
				if(AllRowsCount.HasValue) {
					return;
				}
				itemsQuery = itemsQueryFunc.Invoke(0, 0);
				rowCountQuery = rowCountQueryFunc.Invoke();
				AllRowsCount = rowCountQuery.Value;
			}

			public List<TNode> GetItems(int skip, int take)
			{
				if(!AllRowsCount.HasValue) {
					rowCountQuery = rowCountQueryFunc.Invoke();
				}
				itemsQuery = itemsQueryFunc.Invoke(skip, take);
				return itemsQuery.ToList();
			}

			public List<TNode> GetAllItems()
			{
				itemsQuery = allItemsQueryFunc.Invoke();
				return itemsQuery.ToList();
			}

			public EntityLoadInfo(int? allRowsCount, int loadedRowsCount, Func<int, int, IFutureEnumerable<TNode>> itemsQueryFunc, Func<IFutureValue<int>> rowCountQueryFunc, Func<IFutureEnumerable<TNode>> allItemsQueryFunc)
			{
				AllRowsCount = allRowsCount;
				LoadedRowsCount = loadedRowsCount;
				this.itemsQueryFunc = itemsQueryFunc;
				this.rowCountQueryFunc = rowCountQueryFunc;
				this.allItemsQueryFunc = allItemsQueryFunc;
			}
		}

		#endregion Entity load configuration

		#endregion Dynamic load

		#region Permissions

		protected void UpdateAllEntityPermissions()
		{
			foreach(var entityConfig in EntityConfigs) {
				UpdateEntityPermissions(entityConfig.Key);
			}
		}

		protected virtual void UpdateEntityPermissions<TEntity>()
		{
			UpdateEntityPermissions(typeof(TEntity));
		}

		protected virtual void UpdateEntityPermissions(Type entityType)
		{
			IPermissionResult entityPermissionResult = commonServices.PermissionService.ValidateUserPermission(entityType, commonServices.UserService.CurrentUserId);

			if(EntityConfigs.ContainsKey(entityType)) {
				EntityConfigs[entityType].PermissionResult = entityPermissionResult;
			}
		}

		#endregion Permissions

		#region Actions

		protected virtual void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultAddActions();
			CreateDefaultEditAction();
			CreateDefaultDeleteAction();
		}

		protected virtual void CreatePopupActions()
		{
		}

		private void CreateDefaultAddActions()
		{
			if(!EntityConfigs.Any()) {
				return;
			}

			var totalCreateDialogConfigs = EntityConfigs
				.Where(x => x.Value.PermissionResult.CanCreate)
				.Sum(x => x.Value.EntityDocumentConfigurations
							.Select(y => y.GetCreateEntityDlgConfigs().Count())
							.Sum());

			if(EntityConfigs.Values.Count(x => x.PermissionResult.CanRead) > 1 || totalCreateDialogConfigs > 1) {
				var addParentNodeAction = new JournalAction("Добавить", (selected) => true, (selected) => true, (selected) => { });
				foreach(var entityConfig in EntityConfigs.Values) {
					foreach(var documentConfig in entityConfig.EntityDocumentConfigurations) {
						foreach(var createDlgConfig in documentConfig.GetCreateEntityDlgConfigs()) {
							var childNodeAction = new JournalAction(createDlgConfig.Title,
								(selected) => entityConfig.PermissionResult.CanCreate,
								(selected) => entityConfig.PermissionResult.CanCreate,
								(selected) => TabParent.AddSlaveTab(this, createDlgConfig.OpenEntityDialogFunction())
							);
							addParentNodeAction.ChildActionsList.Add(childNodeAction);
						}
					}
				}
				NodeActionsList.Add(addParentNodeAction);
			} else {
				var entityConfig = EntityConfigs.First().Value;
				var addAction = new JournalAction("Добавить",
					(selected) => entityConfig.PermissionResult.CanCreate,
					(selected) => entityConfig.PermissionResult.CanCreate,
					(selected) => {
						ITdiTab tab = entityConfig.EntityDocumentConfigurations.First().GetCreateEntityDlgConfigs().First().OpenEntityDialogFunction();
						TabParent.AddSlaveTab(this, tab);
					});
			};
		}

		private void CreateDefaultEditAction()
		{
			var editAction = new JournalAction("Изменить",
				(selected) => {
					var selectedNodes = selected.OfType<TNode>();
					if(selectedNodes.Count() > 1) {
						return false;
					}
					TNode selectedNode = selectedNodes.First();
					if(!EntityConfigs.ContainsKey(selectedNode.EntityType)) {
						return false;
					}
					var config = EntityConfigs[selectedNode.EntityType];
					return config.PermissionResult.CanUpdate;
				},
				(selected) => true,
				(selected) => {
					var selectedNodes = selected.OfType<TNode>();
					if(selectedNodes.Count() > 1) {
						return;
					}
					TNode selectedNode = selectedNodes.First();
					if(!EntityConfigs.ContainsKey(selectedNode.EntityType)) {
						return;
					}
					var config = EntityConfigs[selectedNode.EntityType];
					var foundDocumentConfig = config.EntityDocumentConfigurations.FirstOrDefault(x => x.IsIdentified(selectedNode));
					;
					TabParent.AddSlaveTab(this, foundDocumentConfig.GetOpenEntityDlgFunction().Invoke(selectedNode));
				}
			);
		}

		private void CreateDefaultDeleteAction()
		{
			var editAction = new JournalAction("Удалить",
				(selected) => {
					var selectedNodes = selected.OfType<TNode>();
					if(selectedNodes.Count() > 1) {
						return false;
					}
					TNode selectedNode = selectedNodes.First();
					if(!EntityConfigs.ContainsKey(selectedNode.EntityType)) {
						return false;
					}
					var config = EntityConfigs[selectedNode.EntityType];
					return config.PermissionResult.CanDelete;
				},
				(selected) => true,
				(selected) => {
					var selectedNodes = selected.OfType<TNode>();
					if(selectedNodes.Count() > 1) {
						return;
					}
					TNode selectedNode = selectedNodes.First();
					if(!EntityConfigs.ContainsKey(selectedNode.EntityType)) {
						return;
					}
					var config = EntityConfigs[selectedNode.EntityType];
					if(config.PermissionResult.CanDelete) {
						DeleteHelper.DeleteEntity(selectedNode.EntityType, selectedNode.Id);
					}
				}
			);
		}

		#endregion Actions
	}
}
