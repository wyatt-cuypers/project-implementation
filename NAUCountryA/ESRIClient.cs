using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA
{
    public class ESRIClient
    {
        public static string GetContent(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
        public static FormUrlEncodedContent GetEncodedContent(JObject webMapJson)
        {
            return  new FormUrlEncodedContent(new[]
            {
	            new KeyValuePair<string, string>("Web_Map_as_JSON", webMapJson.ToString(Formatting.None)),
	            new KeyValuePair<string, string>("Format", "SVG"),
	            new KeyValuePair<string, string>("Layout_Template", "MAP_ONLY"),
	            new KeyValuePair<string, string>("f", "pjson")
            });
        }

        public static HttpResponseMessage GetResponse(HttpClient client, string url, FormUrlEncodedContent encodedContent)
        {
            return client.PostAsync(url, encodedContent).Result;
        }

        public static JObject GetSVGResult(string content)
        {
            return JObject.Parse(content);
        }

        public static JToken GetURLContent(JObject svgResult)
        {
            return svgResult["results"]?[0]?["value"]?["url"];
        }
        public static JObject LoadJson(string fileName)
        {
            using (StreamReader file = File.OpenText(fileName))
	        using (JsonTextReader reader = new JsonTextReader(file))
	        {
		        return (JObject)JToken.ReadFrom(reader);
	        }
        }

        public static async Task DownloadImage(string url)
        {
	        using (HttpClient client = new HttpClient())
	        using (HttpResponseMessage result = await client.GetAsync(url))
	        {
		        byte[] fileBytes = await result.Content.ReadAsByteArrayAsync();
		        BinaryWriter writer = new BinaryWriter(File.OpenWrite(Guid.NewGuid() + ".svg"));
			    writer.Write(fileBytes);
	        }
        }
    }
}