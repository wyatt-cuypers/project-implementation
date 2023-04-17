using Microsoft.VisualBasic.FileIO;

namespace NAUCountry.ECOMap
{
	public class CsvUtility
	{
		public static ICollection<string> ToCollection(string csvFileName)
		{
			ICollection<string> lines = new List<string>();
			string filePath = Path.Combine(ECOGeneralService.InitialPathLocation, "NAUCountryA", "Resources", csvFileName + ".csv");
			TextFieldParser csvParcer = new TextFieldParser(filePath);
			csvParcer.TextFieldType = FieldType.Delimited;
			while (!csvParcer.EndOfData)
			{
				lines.Add(csvParcer.ReadLine());
			}
			csvParcer.Close();
			return lines;
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

		public static bool DateTimeEquals(DateTime a, DateTime b)
		{
			return a.Month == b.Month && a.Day == b.Day && a.Year == b.Year;
		}
	}
}
