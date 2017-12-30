using System;
using System.Collections.Generic;
using Examen_BoeykensCaroline_3IMDA.Entities;

namespace Examen_BoeykensCaroline_3IMDA.Models
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public string LicensePlate { get; set; }
        public string Owner { get; set; }
        public string Cartype { get; set; }
    }
}