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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Examen_BoeykensCaroline_3IMDA.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //ALL BRANDS
        protected BrandDetailViewModel ConvertBrandToBrandDetailViewModel(Cartype cartype)
        {
            return new BrandDetailViewModel()
            {
                Id = cartype.Id,
                Brand = cartype.Brand,
                Model = cartype.Model
            };
        }

        [HttpGet("/Brand/Brands")]
        public IActionResult Brands()
        {
            var model = new BrandListViewModel() { Brands = new List<BrandDetailViewModel>() };
            var allBrands = _brandService.GetAllTypes();
            model.Brands.AddRange(allBrands.Select(ConvertBrandToBrandDetailViewModel).ToList());
            return View(model);
        }

        //EDIT BRAND
        public BrandEditViewModel ConvertBrandToEditViewModel(Cartype cartype)
        {
            var editView = new BrandEditViewModel
            {
                Id = cartype.Id,
                Brand = cartype.Brand,
                Model = cartype.Model
            };
            return editView;
        }

        [HttpGet("/Brand/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            var brand = _brandService.GetTypeById(id);
            if (brand == null)
            {
                return NotFound();
            }

            var editView = ConvertBrandToEditViewModel(brand);

            return View(editView);
        }

        [HttpGet("/Brand/New")]
        public IActionResult New()
        {
            return View("Detail", new BrandEditViewModel()
            {
                Brand = null,
            });
        }

        [HttpPost("/Brand/Brands")]
        public IActionResult Save([FromForm] BrandEditViewModel editView)
        {
            if (ModelState.IsValid)
            {
                var cartype = editView.Id == 0 ? new Cartype() : _brandService.GetTypeById(editView.Id);
                cartype.Model = editView.Model;
                cartype.Brand = editView.Brand;
                _brandService.Save(cartype);

                return Redirect("/Brand/Brands");
            }
            return View("Detail", editView);
        }

        [HttpPost("/Brand/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _brandService.Delete(id);
            return Redirect("/Brand/Brands");
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
