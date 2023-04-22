using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ECOMap.Models;
using System.Windows;


namespace ECOMap
{
    public class EcoPdfGenerator
    {
        public static void GeneratePDF(ECODataService service, State state, Commodity commodity, int year)
        {
            try
            {
                Document document = new Document();
                Page page1 = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                document.Pages.Add(page1);
                string labelText1 = $"{state.StateName} {commodity.CommodityName} {year}";
                Label label1 = new Label(labelText1, 0, 0, 504, 100, Font.TimesBold, 20, TextAlign.Center);
                page1.Elements.Add(label1);

                List<PageGroup> pages = new List<PageGroup>();
                foreach (Price price in service.PriceEntries.Values)
                {
                    if (price.Offer.County.State.StateName.Equals(state.StateName) && price.Offer.Type.Commodity.CommodityName.Equals(commodity.CommodityName))
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
                        if (price.Offer.Year == year)
                        {
                            pg.Prices.Add(price);
                            pages.Add(pg);
                        }
                        else if (price.Offer.Year == year - 1)
                        {
                            pg.PreviousPrices.Add(price);
                            pages.Add(pg);
                        }

                    }
                }
                foreach (PageGroup pg in pages)
                {
                    Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                    document.Pages.Add(page);
                    string labelText = $"{pg.Practice.PracticeName} {pg.Type.TypeName}";
                    Label label = new Label(labelText, 0, 0, 504, 100, Font.TimesBold, 18, TextAlign.Center);
                    page.Elements.Add(label);
                    ESRIClient client = new ESRIClient(state);
                    foreach (Price price in pg.Prices)
                    {
                        client.RequestParamsList.Add(GetESRIRequstParams(price, pg));
                    }
                    Image image = client.GetImage(0, 150);
                    image.SetSize(400, 400);
                    page.Elements.Add(image);
                    ContentArea legend = GetLegend();
                    page.Elements.Add(legend);
                    document.Draw($"{EcoGeneralService.InitialPathLocation}\\Resources\\Output\\PDFs\\{state.StateName}_{commodity.CommodityName}_{year}_PDF.pdf");
                }
                document.Draw($"{EcoGeneralService.InitialPathLocation}\\Resources\\Output\\PDFs\\{state.StateName}_{commodity.CommodityName}_{year}_PDF.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public static ContentArea GetLegend()
        {
            RgbColor darkRed = new RgbColor(222, 45, 38);
            RgbColor medRed = new RgbColor(255, 165, 120);
            RgbColor lightRed = new RgbColor(254, 224, 210);
            RgbColor lightGreen = new RgbColor(161, 217, 155);
            RgbColor medGreen = new RgbColor(40, 144, 58);
            RgbColor darkGreen = new RgbColor(5, 79, 41);

            ContentArea legend = new ContentArea(400, 300, 200, 200);
            legend.Add(new Label("Percent Change", 0, 0, 200, 20, Font.TimesBold, 14, TextAlign.Left));
            legend.Add(new Rectangle(0, 25, 20, 20, RgbColor.Gray, darkRed, 2, LineStyle.Solid));
            legend.Add(new Label("< -4%", 30, 25, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 45, 20, 20, RgbColor.Gray, medRed, 2, LineStyle.Solid));
            legend.Add(new Label("-4% to -2%", 30, 45, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 65, 20, 20, RgbColor.Gray, lightRed, 2, LineStyle.Solid));
            legend.Add(new Label("-2% to 0%", 30, 65, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 85, 20, 20, RgbColor.Gray, RgbColor.White, 2, LineStyle.Solid));
            legend.Add(new Label("No Change", 30, 85, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 105, 20, 20, RgbColor.Gray, lightGreen, 2, LineStyle.Solid));
            legend.Add(new Label("0% to 2%", 30, 105, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 125, 20, 20, RgbColor.Gray, medGreen, 2, LineStyle.Solid));
            legend.Add(new Label("2% to 4%", 30, 125, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            legend.Add(new Rectangle(0, 145, 20, 20, RgbColor.Gray, darkGreen, 2, LineStyle.Solid));
            legend.Add(new Label("> 4%", 30, 145, 200, 20, Font.TimesRoman, 12, TextAlign.Left));
            return legend;
        }
        public static void TestLegend()
        {
            ceTe.DynamicPDF.Document document = new ceTe.DynamicPDF.Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);
            ContentArea legend = GetLegend();
            page.Elements.Add(legend);
            document.Draw($"{EcoGeneralService.InitialPathLocation}\\Resources\\Output\\PDFs\\TestLegend.pdf");
        }

        public static void GeneratePDFGroup(ECODataService service, string stateName, int year)
        {
            HashSet<Commodity> commodities = new HashSet<Commodity>();
            State state = null;
            Parallel.ForEach(service.StateEntries, stateIter =>
            {
                if (stateIter.Value.StateName.Equals(stateName))
                {
                    state = new State(stateIter.Value.StateCode, stateIter.Value.StateName, stateIter.Value.StateAbbreviation, stateIter.Value.RecordType.RecordTypeCode, stateIter.Value.RecordType);
                }
            });
            foreach (Price price in service.PriceEntries.Values)
            {
                if (price.Offer.County.State.StateName.Equals(stateName) && price.Offer.Year == year && !commodities.Contains(price.Offer.Type.Commodity))
                {
                    commodities.Add(price.Offer.Type.Commodity);
                }
            }

            Parallel.ForEach(commodities, commodity =>
            {
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

        public static ESRIRequestParams GetESRIRequstParams(Price price, PageGroup pg)
        {
            foreach (Price price2 in pg.PreviousPrices)
            {
                if (price.Offer.Practice.Commodity == price2.Offer.Practice.Commodity &&
                price.Offer.County == price2.Offer.County &&
                price.Offer.Practice == price2.Offer.Practice &&
                price.Offer.County.State == price2.Offer.County.State)
                {
                    return new ESRIRequestParams(price.Offer.County, (price.ExpectedIndexValue - price2.ExpectedIndexValue) / price2.ExpectedIndexValue);
                }
            }
            return new ESRIRequestParams(price.Offer.County, 0);
        }
    }
}
