using System;
using System.Collections.Generic;
using Gamma.ColumnConfig;

namespace QS
{
	public static class TreeViewColumnsConfigFactory
	{
		public static FluentColumnsConfig<TNode> Create<TNode>() => new FluentColumnsConfig<TNode>();

		static Dictionary<(Type, Type), IColumnsConfig> columnsConfigs = new Dictionary<(Type, Type), IColumnsConfig>();

		public static void Register<TJournalNode, TJournalViewModel>(IColumnsConfig config)
		{
			Type nodeType = typeof(TJournalNode);
			Type journalType = typeof(TJournalViewModel);
			if(columnsConfigs.ContainsKey((nodeType, journalType)))
				throw new InvalidOperationException($"Комбинация ноды \"{nodeType.Name}\" и модели представления \"{journalType.Name}\" уже зарегистрирована");
			columnsConfigs.Add((nodeType, journalType), config);
		}

		public static IColumnsConfig Resolve<TJournalNode, TJournalViewModel>()
		{
			Type nodeType = typeof(TJournalNode);
			Type journalType = typeof(TJournalViewModel);
			if(!columnsConfigs.ContainsKey((nodeType, journalType)))
				throw new ApplicationException($"Не настроено сопоставление для ноды \"{nodeType.Name}\" и модели представления \"{journalType.Name}\"");
			return columnsConfigs[(nodeType, journalType)];
		}
	}
}