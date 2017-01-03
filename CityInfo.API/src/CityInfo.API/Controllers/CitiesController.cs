using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Services;
using CityInfo.API.Models;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController:Controller
    {
        private ICityInfoRepository _repository;

        public CitiesController(ICityInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            //return Ok(_repository.GetCities());
            var cityEntities = _repository.GetCities();
            var result =Mapper.Map<IEnumerable<CityWithoutpointsOfInterestDTO>>(cityEntities);
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePoI=false)
        {
            var city = _repository.GetCity(id, includePoI);
            if (city==null)
            {
                return NotFound();
            }
            if (includePoI)
            {
                var result = Mapper.Map<CityDTO>(city);
                return Ok(result);
            }

            var resultNotPointOfInterest = Mapper.Map<CityWithoutpointsOfInterestDTO>(city);
            return Ok(resultNotPointOfInterest);
            //var temp = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            //if (temp==null)
            //{
            //    return NotFound();
            //}
            //return Ok(temp);


        }

        
    }
}
