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
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        //ALL OWNERS
        protected OwnerDetailViewModel ConvertOwnerToOwnerDetailViewModel(Owner owner)
        {
            return new OwnerDetailViewModel()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };
        }

        [HttpGet("/Owner/Owners")]
        public IActionResult Owners()
        {
            var model = new OwnerListViewModel() { Owners = new List<OwnerDetailViewModel>() };
            var allOwners = _ownerService.GetAllOwners();
            model.Owners.AddRange(allOwners.Select(ConvertOwnerToOwnerDetailViewModel).ToList());
            return View(model);
        }

        //EDIT OWNER
        public OwnerEditViewModel ConvertOwnerToEditViewModel(Owner owner)
        {
            var editView = new OwnerEditViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };
            return editView;
        }

        [HttpGet("/Owner/{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            var owner = _ownerService.GetOwnerById(id);
            if (owner == null)
            {
                return NotFound();
            }

            var editView = ConvertOwnerToEditViewModel(owner);

            return View(editView);
        }

        [HttpGet("/Owner/New")]
        public IActionResult New()
        {
            return View("Detail", new OwnerEditViewModel()
            {
                FirstName = null,
            });
        }

        [HttpPost("/Owner/Owners")]
        public IActionResult Save([FromForm] OwnerEditViewModel editView)
        {
            if (ModelState.IsValid)
            {
                var owner = editView.Id == 0 ? new Owner() : _ownerService.GetOwnerById(editView.Id);
                owner.FirstName = editView.FirstName;
                owner.LastName = editView.LastName;
                _ownerService.Save(owner);

                return Redirect("/Owner/Owners");
            }
            return View("Detail", editView);
        }

        [HttpPost("/Owner/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _ownerService.Delete(id);
            return Redirect("/Owner/Owners");
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
