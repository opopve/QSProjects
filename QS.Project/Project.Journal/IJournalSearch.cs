using System;
namespace QS.Project.Journal
{
	public interface IJournalSearch
	{
		string[] GetSearchValues();
		event EventHandler OnSearch;
		void Update();
	}
}
