using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace MISA.CukCuk.Web.Controllers {
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase {
        public static IWebHostEnvironment _environment;
        public ImagesController(IWebHostEnvironment environment) {
            _environment = environment;
        }
        public class FIleUploadAPI {
            public IFormFile files {
                get;
                set;
            }
            public string name { get; set; }
        }
        [HttpGet]
        public IActionResult Get() {
            return Ok("1");
        }
        [HttpPost]
        public async Task<IActionResult> ImageUpload(FIleUploadAPI formUpload) {
            if (formUpload.files.Length > 0) {
                try {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\")) {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + formUpload.files.FileName)) {
                        formUpload.files.CopyTo(filestream);
                        filestream.Flush();
                        return Ok("\\uploads\\" + formUpload.files.FileName);
                    }
                }
                catch (Exception ex) {
                    return BadRequest(ex.ToString());
                }
            }
            else {
                return BadRequest("Unsuccessful");
            }
        }
    }
}