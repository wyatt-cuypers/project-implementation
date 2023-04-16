using System.Text.RegularExpressions;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using NAUCountry.ECOMap.Models;

namespace NAUCountry.ECOMap
{
    public class EcoPdfGenerator
    {
        public static void GeneratePDF(ECODataService service, State state, Commodity commodity, int year)
        {
            try {
                Document document = new Document();
                Page page1 = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                document.Pages.Add(page1);
                string labelText1 = $"{state.StateName} {commodity.CommodityName} {year}";
                Label label1 = new Label(labelText1, 0, 0, 504, 100, Font.TimesBold, 20, TextAlign.Center);
                page1.Elements.Add(label1);


            List<PageGroup> pages = new List<PageGroup>();
            foreach (Price price in service.PriceEntries.Values)
            {
                if (price.Offer.County.State.Equals(state) && price.Offer.Type.Commodity.Equals(commodity) && price.Offer.Year == year)
                {
                    NAUType type = price.Offer.Type;
                    Practice practice = price.Offer.Practice;
                    PageGroup pg = new PageGroup(practice, type);
                    foreach (PageGroup p in pages)
                    {
                        if (p.Equals(pg))
                        {
                            pages.Remove(p);
                            pg = p;
                            break;
                        }
                    }
                    pg.AddPrice(price);
                    pages.Add(pg);
                }
            }

                foreach (PageGroup pg in pages)
                {
                    Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                    document.Pages.Add(page);
                    string labelText = $"{pg.Practice.PracticeName} {pg.Type.TypeName}";
                    Label label = new Label(labelText, 0, 0, 504, 100, Font.TimesBold, 18, TextAlign.Center);
                    page.Elements.Add(label);
                    /*TextArea textArea = new TextArea("", 0, 50, 550, 800, ceTe.DynamicPDF.Font.TimesRoman, 12, TextAlign.Left);
                    foreach (Price price in pg.Prices)
                    {
                        textArea.Text = textArea.Text + price.Offer.County.CountyName + ": " + price.ExpectedIndexValue + ";\n";
                    }

                    page.Elements.Add(textArea);*/
                    //page.Elements.Add(new Image(@"C:\data\test.png", 50, 100));
                    ContentArea legend = GetLegend();
                    page.Elements.Add(legend);

                }
                document.Draw(GetPath("PDFOutput/" + state.StateName + "_" + commodity.CommodityName + "_" + year + "_PDF.pdf"));

            } catch(Exception ex) {
                Console.WriteLine(ex);
            }
            
        }
        public static ContentArea GetLegend()
        {
            ContentArea legend = new ContentArea(400, 300, 200, 200);
            legend.Add(new Label("Percent Change", 0, 0, 200, 20, Font.TimesBold, 14, TextAlign.Left));
            legend.Add(new Rectangle(0, 25, 20, 20, RgbColor.Gray, RgbColor.Red, 2, LineStyle.Solid));
            legend.Add(new Label("< -4%", 30, 25, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 45, 20, 20, RgbColor.Gray, RgbColor.Coral, 2, LineStyle.Solid));
            legend.Add(new Label("-4% to -2%", 30, 45, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 65, 20, 20, RgbColor.Gray, RgbColor.LightPink, 2, LineStyle.Solid));
            legend.Add(new Label("-2% to 0%", 30, 65, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 85, 20, 20, RgbColor.Gray, RgbColor.AntiqueWhite, 2, LineStyle.Solid));
            legend.Add(new Label("No Change", 30, 85, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 105, 20, 20, RgbColor.Gray, RgbColor.DarkSeaGreen, 2, LineStyle.Solid));
            legend.Add(new Label("0% to 2%", 30, 105, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 125, 20, 20, RgbColor.Gray, RgbColor.SeaGreen, 2, LineStyle.Solid));
            legend.Add(new Label("2% to 4%", 30, 125, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 145, 20, 20, RgbColor.Gray, RgbColor.DarkGreen, 2, LineStyle.Solid));
            legend.Add(new Label("> 4%", 30, 145, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            return legend;
        }
        public static void TestLegend()
        {
            Document document = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            ContentArea legend = GetLegend();
            page.Elements.Add(legend);
            document.Draw(GetPath("PDFOutput/TestLegend.pdf"));

        }
        public static string GetPath(string filePath)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, filePath);
        }

        public static void GeneratePDFGroup(ECODataService service, string stateName, int year)
        {
            HashSet<Commodity> commodities = new HashSet<Commodity>();
            State state = null;
            Parallel.ForEach(service.StateEntries, stateIter =>
            {
                if(stateIter.Value.StateName.Equals(stateName)) {
                    state = new State(stateIter.Value.StateCode, stateIter.Value.StateName, stateIter.Value.StateAbbreviation, stateIter.Value.RecordType.RecordTypeCode, stateIter.Value.RecordType);
                }
            });
            
            foreach (Price price in service.PriceEntries.Values)
            {
                if (price.Offer.County.State.Equals(state) && price.Offer.Year == year && !commodities.Contains(price.Offer.Type.Commodity))
                {
                    commodities.Add(price.Offer.Type.Commodity);
                }
            }
            
            Parallel.ForEach(commodities, commodity =>
            {
                Console.WriteLine(commodity.CommodityName, "It made it this far!");
                GeneratePDF(service, state, commodity, year);
            });
            

        }

        public static void GenerateAllPDFs(ECODataService service, int year)
        {
            Parallel.ForEach(service.StateEntries.Values, state =>
            {
                GeneratePDFGroup(service, state.StateName, year);
                Console.WriteLine(state.StateName);
            });
        }
    }
}
