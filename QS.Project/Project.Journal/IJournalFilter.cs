using System;
using NHibernate.Criterion;
namespace QS.Project.Journal
{
	public interface IJournalFilter
	{
		//ICriterion GetFilter();
		event EventHandler OnFiltered;
		void Update();
	}
}
