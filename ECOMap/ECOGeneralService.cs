using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOMap
{
    public class ECOGeneralService
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
			if (temp.Name.Equals("project-implementation"))
			{
				return temp.FullName;
			}
			return GetInitialPathLocation(temp.FullName);
		}
    }
}