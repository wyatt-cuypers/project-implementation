using Microsoft.VisualBasic.FileIO;
using NAUCountryA.Models;
using NAUCountryA.Tables;
using System.Text.RegularExpressions;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;

namespace NAUCountryA
{
    public class Service
    {
        public static IReadOnlyDictionary<int, Commodity> CommodityEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<int, County> CountyEntries
        {
            get;
            private set;
        }
        public static string InitialPathLocation
        {
            get
            {
                return GetInitialPathLocation(System.IO.Path.GetFullPath("."));
            }
        }

        public static IReadOnlyDictionary<int, Offer> OfferEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<int, Practice> PracticeEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<Offer, Price> PriceEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<string, RecordType> RecordTypeEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<int, State> StateEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<int, NAUType> TypeEntries
        {
            get;
            private set;
        }


        public static bool DateTimeEquals(DateTime a, DateTime b)
        {
            return a.Month == b.Month && a.Day == b.Day && a.Year == b.Year;
        }

        public static object ExpressValue(string value)
        {
            if ("\"\"".Equals(value))
            {
                return 0.0;
            }
            string temp = value.Substring(1, value.Length - 2);
            if (IsInt(temp))
            {
                return Convert.ToInt32(temp);
            }
            else if (IsDouble(temp))
            {
                return Convert.ToDouble(temp);
            }
            else if (IsDate(temp))
            {
                return ToDateTime(temp);
            }
            return temp;
        }

        public static void GeneratePDF(State state, Commodity commodity, int year)
        {
            Document document = new Document();
            Page page1 = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page1);
            string labelText1 = $"{state.StateName} {commodity.CommodityName} {year}";
            Label label1 = new Label(labelText1, 0, 0, 504, 100, Font.TimesBold, 20, TextAlign.Center);
            page1.Elements.Add(label1);


            List<PageGroup> pages = new List<PageGroup>();
            foreach (Price price in PriceEntries.Values)
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
                    pg.addPrice(price);
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
                TextArea textArea = new TextArea("", 0, 50, 550, 800, ceTe.DynamicPDF.Font.TimesRoman, 12, TextAlign.Left);
                foreach (Price price in pg.Prices)
                {
                    textArea.Text = textArea.Text + price.Offer.County.CountyName + ": " + price.ExpectedIndexValue + ";\n";
                }

                page.Elements.Add(textArea);
                ContentArea legend = GetLegend();
                page.Elements.Add(legend);

            }
            document.Draw(GetPath("PDFOutput/" + state.StateName + "_" + commodity.CommodityName + "_" + year + "_PDF.pdf"));
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

        public IEnumerable<County> GetStateCounties(State state)
        {
            ICollection<County> counties = new List<County>();
            foreach (County county in CountyEntries.Values)
            {
                if (county.State == state)
                {
                    counties.Add(county);
                }
            }
            return counties;
        }

        public static void LoadTables()
        {
            RecordTypeEntries = new RecordTypeTable();
            Console.WriteLine("Record Type Table Loaded");
            CommodityEntries = new CommodityTable();
            Console.WriteLine("Commodity Table Loaded");
            StateEntries = new StateTable();
            Console.WriteLine("State Table Loaded");
            CountyEntries = new CountyTable();
            Console.WriteLine("County Table Loaded");
            TypeEntries = new NAUTypeTable();
            Console.WriteLine("Type Table Loaded");
            PracticeEntries = new PracticeTable();
            Console.WriteLine("Practice Table Loaded");
            OfferEntries = new OfferTable();
            Console.WriteLine("Offer Table Loaded");
            PriceEntries = new PriceTable();
            Console.WriteLine("Price Table Loaded");

        }

        public static ICollection<string> ToCollection(string csvFileName)
        {
            ICollection<string> lines = new List<string>();
            string filePath = InitialPathLocation + "\\NAUCountryA\\Resources\\" + csvFileName + ".csv";
            TextFieldParser csvParcer = new TextFieldParser(filePath);
            csvParcer.TextFieldType = FieldType.Delimited;
            while (!csvParcer.EndOfData)
            {
                lines.Add(csvParcer.ReadLine());
            }
            csvParcer.Close();
            return lines;
        }

        public static DateTime ToDateTime(string value)
        {
            string[] parts = value.Split("/");
            if (parts.Length != 3)
            {
                throw new FormatException("This is not a date.");
            }
            return new DateTime(Convert.ToInt32(parts[2]), Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
        }

        public static string ToString(DateTime date)
        {
            return date.Month + "/" + date.Day + "/" + date.Year;
        }

        public static string ToString(double number) => number.ToString("0.0000");

        private static string GetInitialPathLocation(string currentLocation)
        {
            DirectoryInfo temp = Directory.GetParent(currentLocation);
            if (temp.Name.Equals("project-implementation"))
            {
                return temp.FullName;
            }
            return GetInitialPathLocation(temp.FullName);
        }

        private static bool IsDate(string value)
        {
            try
            {
                ToDateTime(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool IsDouble(string value) => double.TryParse(value, out _);

        private static bool IsInt(string value) => int.TryParse(value, out _);
    }
}
