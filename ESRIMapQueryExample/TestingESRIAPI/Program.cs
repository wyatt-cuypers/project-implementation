// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

HttpClient client = new HttpClient();

var webMapJson = LoadJson();

var encodedContent = new FormUrlEncodedContent(new[]
{
	new KeyValuePair<string, string>("Web_Map_as_JSON", webMapJson.ToString(Formatting.None)),
	new KeyValuePair<string, string>("Format", "SVG"),
	new KeyValuePair<string, string>("Layout_Template", "MAP_ONLY"),
	new KeyValuePair<string, string>("f", "pjson")
});

// Toggle to see output of encoded request
//Console.WriteLine(await encodedContent.ReadAsStringAsync());

var response = await client.PostAsync("https://sampleserver6.arcgisonline.com/arcgis/rest/services/Utilities/PrintingTools/GPServer/Export%20Web%20Map%20Task", encodedContent);

string content = await response.Content.ReadAsStringAsync();

Console.WriteLine(content);

var svgResult = JObject.Parse(content);
Console.WriteLine("SVG Result: " + svgResult);
Console.WriteLine(svgResult["results"]?[0]?["value"]?["url"]);

var urlContent = svgResult["results"]?[0]?["value"]?["url"];
Console.WriteLine("Url Content: " + urlContent);
if (urlContent != null)
{
	await DownloadImage(urlContent.ToString());
}

Console.Read();

JObject LoadJson()
{
	using (StreamReader file = File.OpenText("ESRIrequest.json"))
	using (JsonTextReader reader = new JsonTextReader(file))
	{
		return (JObject)JToken.ReadFrom(reader);
	}
}

async Task DownloadImage(string url)
{
	using (var client = new HttpClient())
	using (var result = await client.GetAsync(url))
	{
		var fileBytes = await result.Content.ReadAsByteArrayAsync();

		using var writer = new BinaryWriter(File.OpenWrite(Guid.NewGuid() + ".svg"));
			writer.Write(fileBytes);
	}
}