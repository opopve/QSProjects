using System;
namespace QS.Project.Journal
{
	public class JournalEntityNodeBase : JournalNodeBase
	{
		public Type EntityType { get; }

		public virtual int Id { get; protected set; }

		protected JournalEntityNodeBase(Type entityType)
		{
			EntityType = entityType;
		}
	}
}
