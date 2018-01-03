using System.Collections.Generic;
using System.Linq;
using Examen_BoeykensCaroline_3IMDA.Data;
using Examen_BoeykensCaroline_3IMDA.Entities;
using Examen_BoeykensCaroline_3IMDA.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Examen_BoeykensCaroline_3IMDA.Controllers
{
    public class OwnerService : IOwnerService
    {
        private readonly EntityContext _entityContext;

        public OwnerService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        private IIncludableQueryable<Owner, Car> GetFullGraph()
        {
            return _entityContext.Owners.Include(x => x.Car).ThenInclude(x => x.Car);
        }

        private IIncludableQueryable<Car, Owner> GetFullCars()
        {
            return _entityContext.Cars.Include(x => x.Cartype).Include(x => x.Owner).ThenInclude(x => x.Owner);
        }

        public List<Owner> GetAllOwners()
        {
            return GetFullGraph().OrderBy(x => x.Id).ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return GetFullGraph().FirstOrDefault(x => x.Id == id);
        }

        public List<Car> GetAllCarsByOwner(int ownerId)
        {
            return GetFullCars().Where(x => x.Owner.Select(y => y.OwnerId).FirstOrDefault() == ownerId).ToList();
        }

        public void Save(Owner owner)
        {
            if (owner.Id == 0)
                _entityContext.Owners.Add(owner);
            else
                _entityContext.Owners.Update(owner);
            _entityContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDelete = GetOwnerById(id);
            if (toDelete != null)
            {
                _entityContext.Owners.Remove(toDelete);
                _entityContext.SaveChanges();
            }
        }
    }
}