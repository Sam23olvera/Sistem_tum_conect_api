using Microsoft.AspNetCore.Mvc;

namespace ConectDB.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files) 
        {
            if (files == null || files.Count == 0)
                return BadRequest("No se seleccionaron archivos.");

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploadPath, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { message = "Archivos subidos correctamente." });
        }
    }
}
