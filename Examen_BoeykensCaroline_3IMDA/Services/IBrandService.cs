using System.Collections.Generic;
using Examen_BoeykensCaroline_3IMDA.Entities;

namespace Examen_BoeykensCaroline_3IMDA.Services
{
    public interface IBrandService
    {
        List<Cartype> GetAllTypes();
        Cartype GetTypeById(int id);
        void Save(Cartype cartype);
        void Delete(int id);
    }
}