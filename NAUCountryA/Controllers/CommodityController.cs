using System;
using Microsoft.AspNetCore.Mvc;
using ECOMap;
using ECOMap.Models;

namespace NAUCountryA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommodityController : ControllerBase
    {
        private readonly ECODataService _service;
        public CommodityController(ECODataService service)
        {
            // LoadingProcessor loader = new LoadingProcessor(ECOMap.EcoGeneralService.InitialPathLocation);
            // _service = loader.LoadCommodities().GetAwaiter().GetResult();
            _service = service;
        }

        [HttpGet("{state}/{year}")]
        //[HttpGet]
        public List<string> GetCommodities(string state, int year)
        {
            //Program.
            List<string> commodities = new List<string>();
            foreach (Price price in _service.PriceEntries.Values)
            {
                if (price.Offer.County.State.StateName.Equals(state) && price.Offer.Year == year && !commodities.Contains(price.Offer.Type.Commodity.CommodityName))
                {
                    commodities.Add(price.Offer.Type.Commodity.CommodityName);
                }
            }
            foreach (string commod in commodities) {
                Console.WriteLine(commod);
            }
            // for(int i = 0; i < _service.CommodityEntries.Count; i++) {
            //     commodities.Add(_service.CommodityEntries.ElementAt(i).Value.CommodityName);
            // }

            return commodities;
        }
    }
}