using System;
namespace QS.Project.Journal
{
	public interface IJournalSearch
	{
		string[] GetValuesToSearch();
		event EventHandler OnSearch;
		void Update();
	}
}
