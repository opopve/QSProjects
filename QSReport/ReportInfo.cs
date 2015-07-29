﻿using System;
using System.Collections;
using System.Collections.Generic;
using QSProjectsLib;

namespace QSReport
{
	public class ReportInfo
	{
		public string Identifier { get; set;}

		public string Title { get; set;}

		public Dictionary<string, object> Parameters;

		public string GetPath()
		{
			return System.IO.Path.Combine (System.IO.Directory.GetCurrentDirectory (), "Reports", Identifier + ".rdl");
		}

		public Uri GetReportUri()
		{
			return new Uri(GetPath ());
		}

		public string GetParametersString()
		{
			if (Parameters == null)
				return String.Empty;
			var parametersBuild = new DBWorks.SQLHelper ();
			foreach(var param in Parameters)
			{
				string value;
				if (param.Value is IEnumerable)
					value = BuildMiltiValue (param.Value as IEnumerable);
				else
					value = param.Value.ToString ();

				parametersBuild.AddAsList (String.Format ("{0}={1}", param.Key, value), "&");
			}
			return parametersBuild.Text;
		}

		private string BuildMiltiValue(IEnumerable values)
		{
			var valuesBuild = new DBWorks.SQLHelper ();

			foreach(var value in values)
			{
				valuesBuild.AddAsList (value.ToString (), ",");
			}
			return valuesBuild.Text;
		}

		public ReportInfo ()
		{

		}
	}
}

