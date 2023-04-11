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
            _service = loader.LoadAll().GetAwaiter().GetResult();
            Console.WriteLine(_service.CommodityEntries);
        }

        [HttpGet]
        public IActionResult GetCommodities()
        {
            IReadOnlyDictionary<int, Commodity> commodities = _service?.CommodityEntries;

            if (commodities == null || commodities.Count == 0)
            {
                return NotFound();
            }

            return Ok(commodities);
        }
    }
}