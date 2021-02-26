using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        IWebHostEnvironment _webHostEnvironment;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _carImageService = carImageService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var context = _carImageService.GetAll();
            if (!context.Success)
            {
                return BadRequest(context);
            }
            return Ok(context);

        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] ImagesUploadDetail objectFile)
        {
            try
            {
                if (objectFile.files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.files.FileName))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return Ok("Success");
                    }
                }
                else
                {
                    return BadRequest("Yükleme Başarısız");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImage)
        {
            var context = _carImageService.Delete(carImage);
            if (context.Success)
            {
                return Ok(context);
            }
            return BadRequest(context);
        }
        public IFormFile files { get; set; }
    }
}
