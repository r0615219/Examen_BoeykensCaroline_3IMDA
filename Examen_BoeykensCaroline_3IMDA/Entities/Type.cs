using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Examen_BoeykensCaroline_3IMDA.Entities
{
    public class Cartype
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
