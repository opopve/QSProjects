using System;
using QS.Services;
namespace QS.Project.Journal
{
	public class JournalEntityConfig
	{
		public Type EntityType { get; }
		public IPermissionResult PermissionResult { get; }
	}
}
