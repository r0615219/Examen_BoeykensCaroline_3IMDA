using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examen_BoeykensCaroline_3IMDA.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_BoeykensCaroline_3IMDA.Models
{
    public class CarEditViewModel
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public string Owner { get; set; }
        public int? OwnerId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public string Cartype { get; set; }
        public int? CartypeId { get; set; }
        public List<SelectListItem> Cartypes { get; set; }
    }
}