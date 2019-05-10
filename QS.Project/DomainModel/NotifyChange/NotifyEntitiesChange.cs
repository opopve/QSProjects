﻿using System;
using QS.DomainModel.Tracking;

namespace QS.DomainModel.NotifyChange
{
	public static class NotifyEntitiesChange
	{
		public static IEntityChangeWatcher Instance { get; private set; }

		static NotifyEntitiesChange()
		{

		}

		/// <summary>
		/// Вызываем инициализацию механизма подписки на обновления.
		/// </summary>
		public static void Enable()
		{
			if (Instance != null)
				return;

			var listner = new AppLevelChangeListener();
			GlobalUowEventsTracker.RegisterGlobalListener(listner);
			SingleUowEventsTracker.RegisterSingleUowListnerFactory(listner);

			Instance = listner;
		}
	}
}
