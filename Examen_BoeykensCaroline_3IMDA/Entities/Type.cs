using System;
using System.Collections.Generic;

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
