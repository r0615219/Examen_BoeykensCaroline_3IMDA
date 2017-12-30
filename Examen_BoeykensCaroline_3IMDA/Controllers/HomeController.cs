using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Examen_BoeykensCaroline_3IMDA.Models;
using Examen_BoeykensCaroline_3IMDA.Services;
using Examen_BoeykensCaroline_3IMDA.Entities;

namespace Examen_BoeykensCaroline_3IMDA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;

        public HomeController(ICarService carService)
        {
            _carService = carService;
        }

        protected CarDetailViewModel ConvertCarToCarDetailViewModel(Car car)
        {
            return new CarDetailViewModel()
            {
                Id = car.Id,
                Color = car.Color,
                Date = car.Date,
                LicensePlate = car.LicensePlate,
                Owner = string.Join(";", car.Owner.Select(x => x.Owner.FirstName + " " + x.Owner.LastName)),
                Cartype = car.Cartype?.Brand
            };
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var model = new CarListViewModel(){ Cars = new List<CarDetailViewModel>() };
            var allCars = _carService.GetAllCars();
            model.Cars.AddRange(allCars.Select(ConvertCarToCarDetailViewModel).ToList());
            return View(model);
        }

        public CarEditViewModel ConvertCarToEditViewModel(Car car)
        {
            var editView = new CarEditViewModel
            {
                Id = car.Id,
                LicensePlate = car.LicensePlate,
                Date = car.Date,
                Color = car.Color,
                Cartype = car.Cartype?.Brand,
                CartypeId = car.Cartype?.Id,
                Owner = string.Join(";", car.Owner.Select(x => x.Owner.FirstName + " " + x.Owner.LastName)),
                //OwnerId = car.Owner?.Select(x => x.OwnerId )
                // - - - - - DEFAULT VALUE NEEDS FIX - - - - - - - 
                OwnerId = '2'
            };
            return editView;
        }

        [HttpGet("/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            var editView = ConvertCarToEditViewModel(car);
            editView.Cartypes = _carService.GetAllTypes().Select(x => new SelectListItem
            {
                Text = x.Brand,
                Value = x.Id.ToString(),
            }
            ).ToList();
            editView.Owners = _carService.GetAllOwners().Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id.ToString(),
            }
            ).ToList();
            return View(editView);
        }

        public IActionResult New()
        {
            return View("Detail", new CarEditViewModel()
            {
                LicensePlate = null,
                Cartypes = _carService.GetAllTypes().Select(x => new SelectListItem
                {
                    Text = x.Brand,
                    Value = x.Id.ToString(),
                }).ToList(),
                Owners = _carService.GetAllOwners().Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id.ToString(),
                }).ToList(),
            });
        }

        [HttpPost("/")]
        public IActionResult Save([FromForm] CarEditViewModel editView)
        {
            if (ModelState.IsValid)
            {
                var car = editView.Id == 0 ? new Car() : _carService.GetCarById(editView.Id);
                car.LicensePlate = editView.LicensePlate;
                car.Cartype = editView.CartypeId.HasValue ? _carService.GetTypeById(editView.CartypeId.Value) : null;
                car.Date = editView.Date;
                car.Color = editView.Color;
                //car.Owner = editView.OwnerId.HasValue ? _carService.GetOwnerById(editView.OwnerId.Value) : null,
                _carService.Save(car);

                return Redirect("/");
            }
            return View("Detail", editView);
        }

        [HttpPost("/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _carService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/Brands")]
        public IActionResult Brands()
        {
            ViewData["Message"] = "Brands";

            return View();
        }

        public IActionResult Owners()
        {
            ViewData["Message"] = "Owners";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
