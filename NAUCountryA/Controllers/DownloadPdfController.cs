using Microsoft.AspNetCore.Mvc;
using System; 
using System.IO; 
using System.Net; 
using System.Net.Http; 
using NAUCountry.ECOMap;
using NAUCountry.ECOMap.Models;


namespace NAUCountryA.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class DownloadPdfController : ControllerBase
    {
        private readonly ECODataService _service;

        public DownloadPdfController()
        {
            LoadingProcessor loader = new LoadingProcessor(ECOGeneralService.InitialPathLocation);
            _service = loader.LoadAll().GetAwaiter().GetResult();
        }

        [HttpPost("{state}/{year}")]
        public IActionResult DownloadPdf(string state, int year)
        {
            try
            {
                // Delete all files for specified state and year
                string[] fileArray = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "PDFOutput"));
                foreach (string str in fileArray) {
                    if(str.Contains(state) && str.Contains(year.ToString())) {
                        System.IO.File.Delete(str);
                    }
                }
                NAUCountry.ECOMap.EcoPdfGenerator.GeneratePDFGroup(_service, state, year);
                return Ok("PDF generation completed successfully.");;
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
                // Delete all files for specified state and year
                string[] fileArray = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "PDFOutput"));
                foreach (string str in fileArray) {
                    System.IO.File.Delete(str);
                }
                NAUCountry.ECOMap.EcoPdfGenerator.GenerateAllPDFs(_service, year);
                return Ok("PDF generation completed successfully.");;
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error response if there was an error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
