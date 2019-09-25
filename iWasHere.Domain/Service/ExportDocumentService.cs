using System;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
     public class ExportDocumentService
    {
        private readonly MissMarvelContext _dbContext;
        public ExportDocumentService(MissMarvelContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public void LandmarkDetailsDocument(int id)
        {
        //    string savePath = Server.MapPath("~/savedoc/PersonInfo.doc");
        //    string templatePath = Server.MapPath("~/word/wordTemplate.doc");
        //    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
        //    Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
        //    doc = app.Documents.Open(templatePath);
        //    doc.Activate();
        //    if (doc.Bookmarks.Exists("Name"))
        //    {
        //        doc.Bookmarks["Name"].Range.Text = Name;
        //    }
        //    if (doc.Bookmarks.Exists("Age"))
        //    {
        //        doc.Bookmarks["Age"].Range.Text = Age;
        //    }
        //    if (doc.Bookmarks.Exists("Time"))
        //    {
        //        doc.Bookmarks["Time"].Range.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //    }
        //    doc.SaveAs2(savePath);
        //    app.Application.Quit();
        //    Response.Write("Success");
        }

    }
}
