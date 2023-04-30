using Microsoft.AspNetCore.Mvc;
using System; 
using System.IO; 
using System.Net; 
using System.Net.Http; 
using ECOMap;


namespace NAUCountryA.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class DownloadPdfController : ControllerBase
    {
        private readonly ECODataService _service;

        public DownloadPdfController(ECODataService service)
        {
            _service = service;
            // LoadingProcessor loader = new LoadingProcessor(ECOMap.EcoGeneralService.InitialPathLocation);
            // _service = loader.LoadAll().GetAwaiter().GetResult();
        }

        [HttpPost("{state}/{year}")]
        public IActionResult DownloadPdf(string state, int year)
        {
            try
            {
                ECOMap.EcoPdfGenerator.GeneratePDFGroup(_service, state, year);
                return Ok("PDF generation completed successfully.");
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error response if there was an error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("{year}")]
        public IActionResult DownloadAllPdf(string state, int year)
        {
            try
            {
                ECOMap.EcoPdfGenerator.GenerateAllPDFs(_service, year);
                return Ok("PDF generation completed successfully.");
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error response if there was an error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
