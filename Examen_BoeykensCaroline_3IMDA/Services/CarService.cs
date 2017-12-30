using System.Collections.Generic;
using System.Linq;
using Examen_BoeykensCaroline_3IMDA.Data;
using Examen_BoeykensCaroline_3IMDA.Entities;
using Examen_BoeykensCaroline_3IMDA.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Examen_BoeykensCaroline_3IMDA.Controllers
{
    public class CarService : ICarService
    {
        private readonly EntityContext _entityContext;

        public CarService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        private IIncludableQueryable<Car, Owner> GetFullGraph()
        {
            return _entityContext.Cars.Include(x => x.Cartype).Include(x => x.Owner).ThenInclude(x => x.Owner);
        }

        public List<Car> GetAllCars()
        {
            return GetFullGraph().OrderBy(x => x.Id)
                .ToList();
        }

        public Car GetCarById(int id)
        {
            return GetFullGraph()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Cartype> GetAllTypes()
        {
            return _entityContext.Cartype.ToList();
        }

        public Cartype GetTypeById(int id)
        {
            return _entityContext.Cartype.Find(id);
        }

        public List<Owner> GetAllOwners()
        {
            return _entityContext.Owners.ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _entityContext.Owners.Find(id);
        }

        public void Save(Car car)
        {
            if (car.Id == 0)
                _entityContext.Cars.Add(car);
            else
                _entityContext.Cars.Update(car);
            _entityContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDelete = GetCarById(id);
            if (toDelete != null)
            {
                _entityContext.Cars.Remove(toDelete);
                _entityContext.SaveChanges();
            }
        }
    }
}