using System;
using System.Collections.Generic;
using System.Linq;
using Examen_BoeykensCaroline_3IMDA.Data;
using Examen_BoeykensCaroline_3IMDA.Entities;
using Examen_BoeykensCaroline_3IMDA.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Examen_BoeykensCaroline_3IMDA.Controllers
{
    public class BrandService : IBrandService
    {
        private readonly EntityContext _entityContext;

        public BrandService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        private IIncludableQueryable<Cartype, Cartype> GetFullGraph()
        {
            return _entityContext.Cartype.Include(x => x.Cars).ThenInclude(x => x.Cartype);
        }

        private IIncludableQueryable<Car, Owner> GetFullCars()
        {
            return _entityContext.Cars.Include(x => x.Cartype).Include(x => x.Owner).ThenInclude(x => x.Owner);
        }

        public List<Cartype> GetAllTypes()
        {
            return GetFullGraph().OrderBy(x => x.Id).ToList();
        }

        public Cartype GetTypeById(int id)
        {
            return GetFullGraph().FirstOrDefault(x => x.Id == id);
        }

        public List<Car> GetAllCarsByBrand(string cartype)
        {
            return GetFullCars().Where(x => x.Cartype.Model == cartype).ToList();
        }

        public void Save(Cartype cartype)
        {
            if (cartype.Id == 0)
                _entityContext.Cartype.Add(cartype);
            else
                _entityContext. Cartype.Update(cartype);
            _entityContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDelete = GetTypeById(id);
            if (toDelete != null)
            {
                _entityContext.Cartype.Remove(toDelete);
                _entityContext.SaveChanges();
            }
        }
    }
}