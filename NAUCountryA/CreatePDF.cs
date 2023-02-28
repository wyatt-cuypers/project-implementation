using System;
using Microsoft.VisualBasic.FileIO;
using NAUCountryA.Exceptions;
using NAUCountryA.Models;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;

namespace NAUCountryA
{
    public class CreatePDF
    {
        public static void Run()
        {
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            string labelText = "Label";
            Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            document.Draw(Util.GetPath("PDFOutput/CreatePDF.pdf"));
        }

        class Util
        {
            // This is a helper function to get the full path to a file from the root of the project.
            internal static string GetPath(string filePath)
            {
                var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return System.IO.Path.Combine(appRoot, filePath);
            }

        }
    }
}
