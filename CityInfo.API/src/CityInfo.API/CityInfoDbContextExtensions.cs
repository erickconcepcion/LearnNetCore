using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoDbContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoDbContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }
            //init seed data
            var cities = new List<City>()
            {
                new City()
                {
                    Name="Santo Domingo",
                    Description="La Capital",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Zona Colonial",
                            Description="Un lugar como de antaño"
                        },
                        new PointOfInterest()
                        {
                            Name="El conde",
                            Description="La gran calle peatonal"
                        }
                    }
                },
                new City()
                {
                    Name="Santiago",
                    Description="La principal en el cibao",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Centro Leon",
                            Description="De los mejores museos del pais"
                        },
                        new PointOfInterest()
                        {
                            Name="El monumento de los 30 caballeros",
                            Description="El simbolo por exelencia de la ciudad corazon"
                        }
                    }
                },
                new City()
                {
                    Name="La Romana",
                    Description="Buen polo turistico",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Isla Catalina",
                            Description="Un lugar paradisiaco con aguas cristalinas"
                        },
                        new PointOfInterest()
                        {
                            Name="Casa de Campo",
                            Description="Solo para millonarios XD"
                        }
                    }
                },
                new City()
                {
                    Name="Higuey",
                    Description="La ciudad religiosa",
                    PointsOfInterest=new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="La Basilica",
                            Description="Iglecia catolica de gran magnitud"
                        }
                    }
                }
            };
            context.AddRange(cities);
            context.SaveChanges();
        }
    }
}
