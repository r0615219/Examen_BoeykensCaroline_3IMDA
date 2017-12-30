using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examen_BoeykensCaroline_3IMDA.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public virtual Cartype Cartype { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public virtual List<CarOwner> Owner { get; set; }
        public DateTime Date { get; set; }
    }
}
