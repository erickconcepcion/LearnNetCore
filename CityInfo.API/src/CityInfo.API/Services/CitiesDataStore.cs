using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDTO> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id=1,
                    Name="Santo Domingo",
                    Description="La Capital",
                    PointsOfInterest=new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=1,
                            Name="Zona Colonial",
                            Description="Un lugar como de antaño"
                        },
                        new PointOfInterestDTO()
                        {
                            Id=2,
                            Name="El conde",
                            Description="La gran calle peatonal"
                        }
                    }
                },
                new CityDTO()
                {
                    Id=2,
                    Name="Santiago",
                    Description="La principal en el cibao",
                    PointsOfInterest=new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=3,
                            Name="Centro Leon",
                            Description="De los mejores museos del pais"
                        },
                        new PointOfInterestDTO()
                        {
                            Id=4,
                            Name="El monumento de los 30 caballeros",
                            Description="El simbolo por exelencia de la ciudad corazon"
                        }
                    }
                },
                new CityDTO()
                {
                    Id=3,
                    Name="La Romana",
                    Description="Buen polo turistico",
                    PointsOfInterest=new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=5,
                            Name="Isla Catalina",
                            Description="Un lugar paradisiaco con aguas cristalinas"
                        },
                        new PointOfInterestDTO()
                        {
                            Id=6,
                            Name="Casa de Campo",
                            Description="Solo para millonarios XD"
                        }
                    }
                },
                new CityDTO()
                {
                    Id=4,
                    Name="Higuey",
                    Description="La ciudad religiosa",
                    PointsOfInterest=new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=1,
                            Name="La Basilica",
                            Description="Iglecia catolica de gran magnitud"
                        }                        
                    }
                }
            };
        }
    }
}
