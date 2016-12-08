﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using QSProjectsLib;

namespace QSReport
{
	public class DocumentPrinter
	{		
		public static void Print(IPrintableDocument document)
		{
			PrintAll(new IPrintableDocument[]{ document });
		}		


		public static void PrintAll(IEnumerable<IPrintableDocument> documents)
		{
			if(Environment.OSVersion.Platform != PlatformID.MacOSX && Environment.OSVersion.Platform != PlatformID.Unix)
			{
				PrintAll_Win_Workaround(documents);
				return;
			}
				
			PrintOperation printOp;
			printOp = new PrintOperation();
			printOp.Unit = Unit.Points;
			printOp.UseFullPage = true;
			printOp.ShowProgress = true;
			var documentsRDL = documents.Where(doc=>doc.PrintType==PrinterType.RDL).ToList();
			BatchRDLRenderer renderer = new BatchRDLRenderer(documentsRDL);

			var result = LongOperationDlg.StartOperation(
				renderer.PrepareDocuments,
				"Подготовка к печати...",
				documentsRDL.Count()
			);
			if (result == LongOperationResult.Canceled)
				return;
			
			printOp.NPages = renderer.PageCount;
			if (documentsRDL.Any(x => x.Orientation == DocumentOrientation.Landscape))
			{
				printOp.RequestPageSetup += renderer.RequestPageSetup;
			}

			printOp.DrawPage += renderer.DrawPage;
			printOp.Run(PrintOperationAction.PrintDialog, null);
		}

		/// <summary>
		/// Альтернативная релализация печати, специально для обхода бага в кайро при поворачивании(со старой реализацией печати пол Linux)
		/// https://bugzilla.mozilla.org/show_bug.cgi?id=1205854#c16
		/// В новой при установке ориентации для каждой странице на большенстве принтеров обрезается часть выходящая за поворот. Хотя ориентация правильная.
		/// На некоторых принтерах например в водовозе, табличка рисуется за вертикалью листа а текст нет.
		/// используется только на винде, в линуксе такой проблемы нет.
		/// </summary>
		private static void PrintAll_Win_Workaround(IEnumerable<IPrintableDocument> documents)
		{
			PrintOperation printOp;
			var documentsRDL_Portrait = documents.Where(doc => doc.PrintType==PrinterType.RDL && doc.Orientation == DocumentOrientation.Portrait).ToList();
			var documentsRDL_Landscape = documents.Where(doc=>doc.PrintType==PrinterType.RDL && doc.Orientation == DocumentOrientation.Landscape).ToList();

			if (documentsRDL_Portrait.Count > 0)
			{
				printOp = new PrintOperation();
				printOp.Unit = Unit.Points;
				printOp.UseFullPage = true;
				printOp.ShowProgress = true;
				printOp.DefaultPageSetup = new PageSetup();
				printOp.DefaultPageSetup.Orientation = PageOrientation.Portrait;

				BatchRDLRenderer renderer = new BatchRDLRenderer(documentsRDL_Portrait);

				var result = LongOperationDlg.StartOperation(
					            renderer.PrepareDocuments,
					            "Подготовка к печати портретных страниц...",
					            documentsRDL_Portrait.Count()
				            );
				if (result == LongOperationResult.Canceled)
					return;

				printOp.NPages = renderer.PageCount;

				printOp.DrawPage += renderer.DrawPage;
				printOp.Run(PrintOperationAction.PrintDialog, null);
			}

			if(documentsRDL_Landscape.Count > 0)
			{
				printOp = new PrintOperation();
				printOp.Unit = Unit.Points;
				printOp.UseFullPage = true;
				printOp.ShowProgress = true;
				printOp.DefaultPageSetup = new PageSetup();
				printOp.DefaultPageSetup.Orientation = PageOrientation.Landscape;

				BatchRDLRenderer renderer = new BatchRDLRenderer(documentsRDL_Landscape);

				var result = LongOperationDlg.StartOperation(
					renderer.PrepareDocuments,
					"Подготовка к печати альбомных страниц...",
					documentsRDL_Landscape.Count()
				);
				if (result == LongOperationResult.Canceled)
					return;

				printOp.NPages = renderer.PageCount;

				printOp.DrawPage += renderer.DrawPage;
				printOp.Run(PrintOperationAction.PrintDialog, null);
			}
		}			

		public static QSTDI.TdiTabBase GetPreviewTab(IPrintableDocument document)
		{
			return new QSReport.ReportViewDlg (document.GetReportInfoForPreview());				
		}
	}		

	public interface IPrintableDocument
	{
		PrinterType PrintType{ get; }
		DocumentOrientation Orientation{ get; }
		string Name { get;}
		QSReport.ReportInfo GetReportInfo ();
		QSReport.ReportInfo GetReportInfoForPreview();
	}

	public enum PrinterType{
		None, RDL, ODT
	}

	public enum DocumentOrientation{
		Portrait,Landscape
	}


}