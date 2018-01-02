using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Examen_BoeykensCaroline_3IMDA.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_BoeykensCaroline_3IMDA.Models
{
    public class BrandEditViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}