using System;
using System.Collections.Generic;
using Examen_BoeykensCaroline_3IMDA.Entities;

namespace Examen_BoeykensCaroline_3IMDA.Models
{
    public class OwnerDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Car> CarByOwner { get; set; }
    }
}