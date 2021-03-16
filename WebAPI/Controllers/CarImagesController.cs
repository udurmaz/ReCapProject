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
        [HttpGet("getImagesByCarId")]
        public IActionResult GetAllImagesByCarId(int Id)
        {
            var result = _carImageService.GetAllImagesByCarId(Id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getImagesById")]
        public IActionResult GetAllImagesById(int Id)
        {
            var result = _carImageService.GetById(Id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("CarImage"))] IFormFile objectFile, [FromForm] CarImage carImage)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var newGuidPath = Guid.NewGuid().ToString() + Path.GetExtension(objectFile.FileName);


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = System.IO.File.Create(path + newGuidPath))
            {
                objectFile.CopyTo(fileStream);
                fileStream.Flush();
            }
            if (objectFile == null)
            {
                carImage.ImagePath = path + "default.png";
            }
            var result = _carImageService.Add(new CarImage
            {
                CarId = carImage.CarId,
                Date = DateTime.Now,
                ImagePath = newGuidPath
            });
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
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