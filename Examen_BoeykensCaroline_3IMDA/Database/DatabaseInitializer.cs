using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_BoeykensCaroline_3IMDA.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Examen_BoeykensCaroline_3IMDA.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(EntityContext entityContext)
        {
            if (((entityContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists()).GetValueOrDefault(false))
            {
                return;
            }

            var owners = new List<Owner>();
            for (var i = 0; i < 5; i++)
            {
                owners.Add(new Owner {FirstName = $"Sam", LastName = $"Parker"});
            }
            var cartypes = new List<Cartype>
            {
                new Cartype() {Brand = "Volkswagen", Model = "Golf IV"},
                new Cartype() {Brand = "Jeep", Model = "L200"},
                new Cartype() {Brand = "Skoda", Model = "Superb"},
                new Cartype() {Brand = "Volkswagen", Model = "Fox"}
            };

            var cars = new List<Car>();
            for (var i = 0; i < 5; i++)
            {
                var carOwner = new CarOwner()
                {
                    Owner = owners[i]
                };

                Cartype cartype = null;
                if (i % 4 == 0)
                {
                    cartype = cartypes[0];
                }
                else if (i % 3 == 0)
                {
                    cartype = cartypes[1];
                }
                else if (i % 2 == 0)
                {
                    cartype = cartypes[2];
                }
                else {
                    cartype = cartypes[3];
                }

                cars.Add(new Car
                {
                    Color = $"Red",
                    LicensePlate = $"1NUL999",
                    Date = new DateTime(2002, 05, 11),
                    Owner = new List<CarOwner>() { carOwner },
                    Cartype = cartype
                });
            }

            entityContext.Database.EnsureCreated();
            entityContext.Owners.AddRange(owners);
            entityContext.Cars.AddRange(cars);
            entityContext.Cartype.AddRange(cartypes);
            entityContext.SaveChanges();
        }
    }
}