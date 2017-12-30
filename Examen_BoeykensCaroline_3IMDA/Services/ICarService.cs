using System.Collections.Generic;
using Examen_BoeykensCaroline_3IMDA.Entities;

namespace Examen_BoeykensCaroline_3IMDA.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        List<Cartype> GetAllTypes();
        Cartype GetTypeById(int id);
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        void Save(Car car);
        void Delete(int id);
    }
}