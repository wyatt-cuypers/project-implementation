using ECOMap.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ceTe.DynamicPDF.PageElements;

namespace ECOMap
{
    public class ESRIClient
    {
        private readonly State state;
        public ESRIClient(State state)
        {
            RequestParamsList = new List<ESRIRequestParams>();
            this.state = state;
        }

        public ICollection<ESRIRequestParams> RequestParamsList
        {
            get;
            private set;
        }

        public Image GetImage(float x, float y)
        {
            using (HttpClient client = new HttpClient())
	        using (HttpResponseMessage result = client.GetAsync(UrlConent.ToString()).Result)
	        {
		        byte[] fileBytes = result.Content.ReadAsByteArrayAsync().Result;
                string filePath = $"{EcoGeneralService.InitialPathLocation}\\Resouces\\Output\\Images\\{Guid.NewGuid()}.png";
		        BinaryWriter writer = new BinaryWriter(File.OpenWrite(filePath));
			    writer.Write(fileBytes);
                return new Image(filePath, x, y);
	        }
        }

        private JObject ESRIRequest
        {
            get
            {
                string filePath = $"{EcoGeneralService.InitialPathLocation}\\Resources\\ESRIRequest.json";
                string json = File.ReadAllText(filePath);
                JObject obj = JObject.Parse(json);
                UpdateUniqueValueInfos((JObject)(obj["operationalLayers"]?[0]?["layerDefinition"]?["drawingInfo"]?["renderer"]));
                obj["operationalLayers"][0]["layerDefinition"]["definitionExpression"] = $"CNT_STATE_FIPS = '{state.StateCode}'";
                return obj;
            }
        }

        private JToken UrlConent
        {
            get
            {
                HttpClient client = new HttpClient();
                FormUrlEncodedContent encodedContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Web_Map_as_JSON", ESRIRequest.ToString(Formatting.None)),
                    new KeyValuePair<string, string>("Format", "PNG"),
                    new KeyValuePair<string, string>("Layout_Template", "MAP_ONLY"),
                    new KeyValuePair<string, string>("f", "pjson")
                });
                string mapUrl = "https://sampleserver6.arcgisonline.com/arcgis/rest/services/Utilities/PrintingTools/GPServer/Export%20Web%20Map%20Task/execute";
                HttpResponseMessage response = client.PostAsync(mapUrl, encodedContent).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                JObject pngResult = JObject.Parse(content);
                return pngResult["results"][0]["value"]["url"];
            }
        }

        private void UpdateUniqueValueInfos(JObject renderer)
        {
            renderer.Remove("uniqueValueInfos");
            JArray uniqueValueInfos = new JArray();
            foreach (ESRIRequestParams request in RequestParamsList)
            {
                uniqueValueInfos.Add(request.UniqueValueInfo);
            }
            renderer.Add("uniqueValueInfos", uniqueValueInfos);
        }
    }
}