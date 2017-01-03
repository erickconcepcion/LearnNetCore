using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoDbContext _context;

        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context;
        }
        public void AddPointOfInterest(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfInterest.Add(pointOfInterest);

        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c=>c.Id==cityId);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.Remove(pointOfInterest);
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePoI)
        {
            if (includePoI)
            {
                return _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefault();
            }
            return _context.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterest(int cityId, int PointOfInterestId)
        {
            return _context.PointOfInterests.Where(p=>p.Id==PointOfInterestId &&
            p.CityId==cityId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _context.PointOfInterests.Where(p=>p.CityId==cityId).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
