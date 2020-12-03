using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace MISA.CukCuk.Web.Controllers {
    [Route("api/[controller]")]
    public class ImageController : ControllerBase {
        public static IWebHostEnvironment _environment;
        public ImageController(IWebHostEnvironment environment) {
            _environment = environment;
        }
        public class FIleUploadAPI {
            public IFormFile files {
                get;
                set;
            }
        }
        [HttpGet]
        public IActionResult Get() {
            return Ok("1");
        }
        [HttpPost]
        public async Task<string> Post([FromForm] IFormFile file) {
            if (file.Length > 0) {
                try {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\")) {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + file.FileName)) {
                        file.CopyTo(filestream);
                        filestream.Flush();
                        return "\\uploads\\" + file.FileName;
                    }
                }
                catch (Exception ex) {
                    return ex.ToString();
                }
            }
            else {
                return "Unsuccessful";
            }
        }
    }
}