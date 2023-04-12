using Microsoft.AspNetCore.Mvc;
using NAUCountry.ECOMap;
using NAUCountry.ECOMap.Models;
using NAUCountry.ECOMap.Tables;

namespace NAUCountryA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommodityController : ControllerBase
    {
        private readonly ECODataService _service;

        public CommodityController()
        {
            LoadingProcessor loader = new LoadingProcessor(CsvUtility.InitialPathLocation);
            _service = loader.LoadCommodities().GetAwaiter().GetResult();
        }

        [HttpGet]
        public List<string> GetCommodities()
        {
            List<string> commodities = new List<string>();
            for(int i = 0; i < _service.CommodityEntries.Count; i++) {
                commodities.Add(_service.CommodityEntries.ElementAt(i).Value.CommodityName);
            }

            return commodities;
        }
    }
}