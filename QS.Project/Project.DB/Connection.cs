﻿using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace QS.Project.DB
{
	public static class Connection
	{
		internal static Func<string> GetConnectionString;
		public static string ConnectionString => GetConnectionString();

		internal static Func<DbConnection> GetConnectionDB;
		public static DbConnection ConnectionDB => GetConnectionDB();
	}
}
