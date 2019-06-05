using System;
using System.Collections.Generic;
using QS.DomainModel.Config;
using QS.DomainModel.Entity;
using QS.Utilities.Text;
using QS.Services;
using QS.Tdi;
using System.Linq;
using QS.Deletion;

namespace QS.Project.Journal
{
	public abstract class EntityJournalViewModelBase<TNode, TFilter> : JournalViewModelBase<TFilter>
		where TNode : JournalEntityNodeBase
		where TFilter : IJournalFilter
	{
		private readonly IEntityConfigurationProvider entityConfigurationProvider;
		private readonly ICommonServices commonServices;

		protected List<Type> EntityTypes { get; private set; }

		protected EntityJournalViewModelBase(IEntityConfigurationProvider entityConfigurationProvider, ICommonServices commonServices) : base(commonServices?.InteractiveService)
		{
			this.entityConfigurationProvider = entityConfigurationProvider ?? throw new ArgumentNullException(nameof(entityConfigurationProvider));
			this.commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			entityPermissions = new Dictionary<Type, IPermissionResult>();
			EntityTypes = new List<Type>();
		}

		protected void EndConfiguration()
		{
			CreateNodeActions();
			CreatePopupActions();
		}

		public sealed override void Refresh()
		{
			allRowsCount = null;
			loadedCount = 0;
			cachedItemsList.Clear();
			TryLoad();
		}

		#region Dynamic load

		protected int PageSize { get; set; }
		List<TNode> cachedItemsList = new List<TNode>();
		private int? allRowsCount = null;
		private int loadedCount = 0;
		/*
		public override bool TryLoad()
		{
			if(allRowsCount.HasValue && allRowsCount.Value == loadedCount) {
				return false;
			}
			var query = ItemsSourceQueryFunction.Invoke();

			if(!allRowsCount.HasValue) {
				var rowCountQuery = query.Clone().ClearOrders();
				allRowsCount = rowCountQuery.FutureValue<int>().Value;
			}
			var currentPageSize = (allRowsCount.Value - loadedCount) < PageSize ? (allRowsCount.Value - loadedCount) : PageSize;
			if(currentPageSize < 0) {
				currentPageSize = 0;
			}
			var rowsFutureValue = query.Skip(loadedCount).Take(currentPageSize).Future<TNode>();
			loadedCount += currentPageSize;
			cachedItemsList.AddRange(rowsFutureValue.ToList());
			Items = cachedItemsList;
			return !allRowsCount.HasValue || allRowsCount.Value != loadedCount;
		}*/

		#endregion

		#region Permissions

		protected Dictionary<Type, IPermissionResult> entityPermissions;

		protected void UpdateAllEntityPermissions()
		{
			foreach(var entityType in EntityTypes) {
				UpdateEntityPermissions(entityType);
			}
		}

		protected virtual void UpdateEntityPermissions<TEntity>()
		{
			UpdateEntityPermissions(typeof(TEntity));
		}

		protected virtual void UpdateEntityPermissions(Type entityType)
		{
			IPermissionResult entityPermissionResult = commonServices.PermissionService.ValidateUserPermission(entityType, commonServices.UserService.CurrentUserId);

			if(entityPermissions.ContainsKey(entityType)) {
				entityPermissions[entityType] = entityPermissionResult;
			} else {
				entityPermissions.Add(entityType, entityPermissionResult);
			}
		}

		protected virtual IPermissionResult GetEntityPermission(Type entityType)
		{
			if(!entityPermissions.ContainsKey(entityType)) {
				UpdateEntityPermissions(entityType);
			}
			return entityPermissions[entityType];
		}

		#endregion Permissions

		#region Actions

		protected virtual void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultAddAction();
			CreateDefaultEditAction();
			CreateDefaultDeleteAction();
		}

		protected virtual void CreatePopupActions()
		{
		}

		private void CreateDefaultAddAction()
		{
			var accessibleEntityType = EntityTypes.Where(x => GetEntityPermission(x).CanCreate);
			if(!accessibleEntityType.Any()) {
				return;
			}
			if(accessibleEntityType.Count() > 1) {
				var addParentNodeAction = new JournalAction("Добавить", (selected) => true, (selected) => true, (selected) => { });
				foreach(var entityType in accessibleEntityType) {
					var entityNames = DomainHelper.GetSubjectNames(entityType);
					string title = entityType.Name;
					if(entityNames != null) {
						title = entityNames.Nominative.StringToTitleCase();
					}
					var childNodeAction = new JournalAction(title,
						(selected) => GetEntityPermission(entityType).CanCreate,
						(selected) => GetEntityPermission(entityType).CanCreate,
						(selected) => OpenSlaveEntityCreateDlgTab(entityType));
					addParentNodeAction.ChildActionsList.Add(childNodeAction);
				}
				NodeActionsList.Add(addParentNodeAction);
			} else {
				var nodeEntityType = EntityTypes.First();
				var addAction = new JournalAction("Добавить",
					(selected) => GetEntityPermission(nodeEntityType).CanCreate,
					(selected) => GetEntityPermission(nodeEntityType).CanCreate,
					(selected) => OpenSlaveEntityCreateDlgTab(nodeEntityType));
			};
		}

		private void CreateDefaultEditAction()
		{
			var accessibleEntityType = EntityTypes.Where(x => GetEntityPermission(x).CanUpdate);
			if(!accessibleEntityType.Any()) {
				return;
			}
			var editAction = new JournalAction("Изменить",
				(selected) => {
					var selectedNodes = selected.OfType<JournalEntityNodeBase>();
					if(selectedNodes.Count() > 1) {
						return false;
					}
					JournalEntityNodeBase selectedNode = selectedNodes.First();
					return GetEntityPermission(selectedNode.EntityType).CanUpdate;
				},
				(selected) => accessibleEntityType.Any(),
				(selected) => {
					var selectedNodes = selected.OfType<JournalEntityNodeBase>();
					if(selectedNodes.Count() > 1) {
						return;
					}
					JournalEntityNodeBase selectedNode = selectedNodes.First();
					OpenSlaveEntityOpenDlgTab(selectedNode.EntityType, selectedNode.Id);
				}
			);
		}

		private void CreateDefaultDeleteAction()
		{
			var accessibleEntityType = EntityTypes.Where(x => GetEntityPermission(x).CanDelete);
			if(!accessibleEntityType.Any()) {
				return;
			}
			var editAction = new JournalAction("Удалить",
				(selected) => {
					var selectedNodes = selected.OfType<JournalEntityNodeBase>();
					if(selectedNodes.Count() > 1) {
						return false;
					}
					JournalEntityNodeBase selectedNode = selectedNodes.First();
					return GetEntityPermission(selectedNode.EntityType).CanDelete;
				},
				(selected) => accessibleEntityType.Any(),
				(selected) => {
					var selectedNodes = selected.OfType<JournalEntityNodeBase>();
					if(selectedNodes.Count() > 1) {
						return;
					}
					JournalEntityNodeBase selectedNode = selectedNodes.First();
					DeleteHelper.DeleteEntity(selectedNode.EntityType, selectedNode.Id);
				}
			);
		}

		#endregion Actions

		#region TabConstructors

		protected virtual void OpenSlaveEntityCreateDlgTab(Type entityType)
		{
			ITdiTab dialogTab = entityConfigurationProvider.GetEntityConfig(entityType).CreateDialog();
			TabParent.AddSlaveTab(this, dialogTab);
		}

		protected virtual void OpenSlaveEntityOpenDlgTab(Type entityType, int id)
		{
			ITdiTab dialogTab = entityConfigurationProvider.GetEntityConfig(entityType).CreateDialog(id);
			TabParent.AddSlaveTab(this, dialogTab);
		}

		#endregion TabConstructors
	}
}
