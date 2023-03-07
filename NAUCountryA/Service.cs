using Microsoft.VisualBasic.FileIO;
using NAUCountryA.Models;
using NAUCountryA.Tables;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System.Text.RegularExpressions;

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

        public static IReadOnlyDictionary<Offer,Price> PriceEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<string,RecordType> RecordTypeEntries
        {
            get;
            private set;
        }

        public static IReadOnlyDictionary<int,State> StateEntries
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

        public static void GeneratePDF(State state, Practice practice, NAUType type)
        {
            Document doc = new Document();
            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            doc.Pages.Add(page);
            ICollection<Price> prices = new List<Price>();
            foreach (Price price in PriceEntries.Values)
            {
                if (price.Offer.County.State == state && price.Offer.Practice == practice && price.Offer.Type == type && price.Offer.Year == 2023)
                {
                    prices.Add(price);
                }
            }
            string labelText = $"{state.StateName} {practice.Commodity.CommodityName} {practice.PracticeName} {type.TypeName} {2023}";
            Label label = new Label(labelText, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);
            TextArea textArea = new TextArea("", 100, 100, 400, 30, ceTe.DynamicPDF.Font.HelveticaBoldOblique, 18);
            foreach (Price price in prices)
            {
                textArea.Text = textArea.Text + price.Offer.County + ": " + price.ExpectedIndexValue + "\n";
            }
            page.Elements.Add(textArea);
            doc.Draw(GetPath("PDFOutput/CreatePDF.pdf")); ;
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
            // Console.WriteLine("Record Type Table Loaded");
            CommodityEntries = new CommodityTable();
            // Console.WriteLine("Commodity Table Loaded");
            // StateEntries = new StateTable();
            // Console.WriteLine("State Table Loaded");
            // CountyEntries = new CountyTable();
            // Console.WriteLine("County Table Loaded");
            // TypeEntries = new NauTypeTable();
            // Console.WriteLine("Type Table Loaded");
            // PracticeEntries = new PracticeTable();
            // Console.WriteLine("Practice Table Loaded");
            // OfferEntries = new OfferTable();
            // Console.WriteLine("Offer Table Loaded");
            // PriceEntries = new PriceTable();
            // Console.WriteLine("Price Table Loaded");
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

        public static string ToString(double number)
        {
            if (number == 0.0)
            {
                return "";
            }
            try
            {
                string temp = number.ToString().Split('.')[1];
                switch (temp.Length)
                {
                    case 1: return $"{number.ToString()}000";
                    case 2: return $"{number.ToString()}00";
                    case 3: return $"{number.ToString()}0";
                }
                return $"'{number.ToString()}'";
            }
            catch (IndexOutOfRangeException)
            {
                return $"{number.ToString()}.0000";
            }
        }

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

        private static bool IsDouble(string value)
        {
            try
            {
                Convert.ToDouble(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool IsInt(string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
