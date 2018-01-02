using System;
using System.Collections.Generic;

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
