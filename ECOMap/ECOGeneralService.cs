using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMap
{
    public class EcoGeneralService
    {
        public static string InitialPathLocation
		{
			get
			{
				return GetInitialPathLocation(System.IO.Path.GetFullPath("."));
			}
		}
        private static string GetInitialPathLocation(string currentLocation)
		{
			DirectoryInfo temp = Directory.GetParent(currentLocation);
			if (temp.Name.Equals("NAU"))
			{
				return temp.FullName;
			}
			return GetInitialPathLocation(temp.FullName);
		}

		public static string RemoveQuotationsFromCurrentFormat(string format)
		{
			int length = format.Length - 2;
			return format.Substring(1, length);
		}
    }
}