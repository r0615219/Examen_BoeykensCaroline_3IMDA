using System.Collections.Generic;
using Examen_BoeykensCaroline_3IMDA.Entities;

namespace Examen_BoeykensCaroline_3IMDA.Services
{
    public interface IOwnerService
    {
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        void Save(Owner owner);
        void Delete(int id);
    }
}