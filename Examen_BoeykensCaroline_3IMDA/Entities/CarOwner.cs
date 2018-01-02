using System;
using System.Collections.Generic;

namespace Examen_BoeykensCaroline_3IMDA.Entities
{
    public class CarOwner
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
