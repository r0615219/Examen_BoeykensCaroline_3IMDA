using System;
using System.Collections.Generic;

namespace Examen_BoeykensCaroline_3IMDA.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<CarOwner> Car { get; set; }
    }
}
