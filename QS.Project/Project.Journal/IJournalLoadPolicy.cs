using System;
namespace QS.Project.Journal
{
	public interface IJournalLoadPolicy
	{
		bool DynamicLoadingEnabled { get; }
		int PageSize { get; set; }
	}
}
