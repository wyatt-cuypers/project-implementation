using Microsoft.AspNetCore.Mvc;
using System; 
using System.IO; 
using System.Net; 
using System.Net.Http; 


namespace NAUCountryA.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class EbookController : ControllerBase
    {
        [HttpGet("{state}/{crop}/{year}")]
        public IActionResult DownloadPdf(string state, string crop, int year)
        {
            try
            {
                // Check if the file exists
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFOutput", $"{state}_{crop}_{year}_PDF.pdf");
                if (!System.IO.File.Exists(filePath))
                {
                    // Return 404 Not Found response if file doesn't exist
                    return NotFound("The requested PDF file was not found.");
                }

                // Open the PDF file
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Set the content type and content-disposition headers
                return File(fs, "application/pdf", $"{state}_{crop}_{year}_PDF.pdf");
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error response if there was an error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
