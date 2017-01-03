using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using CityInfo.API.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointOfInterestController:Controller
    {
        private ILogger<PointOfInterestController> _logger;
        private ICityInfoRepository _repository;

        public PointOfInterestController(ILogger<PointOfInterestController> logger,
            ICityInfoRepository repository)
        {
            _logger = logger;
            _repository = repository;

        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            if (!_repository.CityExists(cityId))
            {
                _logger.LogInformation($"City with id: {cityId} does not exist");
                return NotFound();
            }
            var pointsOfInterestForCity = _repository.GetPointsOfInterestForCity(cityId);
            if (pointsOfInterestForCity==null)
            {
                return NotFound();
            }
            var result = Mapper.Map<IEnumerable<PointOfInterestDTO>>(pointsOfInterestForCity);
           
            return Ok(result);
            
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId,int id)
        {
            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = _repository.GetPointOfInterest(cityId,id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"got your point of interest {pointOfInterest.Name}");

            var result = Mapper.Map<PointOfInterestDTO>(pointOfInterest);
            return Ok(result);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,[FromBody]PointOfInterestCreate pointOfInterest)
        {
            if (pointOfInterest==null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            _repository.AddPointOfInterest(cityId,finalPointOfInterest);
            if (!_repository.Save())
            {
                return StatusCode(500,"Something was wrong on server");
            }
            var createdPointOfInterest = Mapper.Map<PointOfInterestDTO>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",new { citId=cityId,id= createdPointOfInterest.Id}, createdPointOfInterest);

        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointsOfInterest(int cityId, int id, [FromBody]PointOfInterestUpdate pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterestFromStore = _repository.GetPointOfInterest(cityId,id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            Mapper.Map(pointOfInterest,pointOfInterestFromStore);
            if (!_repository.Save())
            {
                return StatusCode(500, "Something was wrong on server");
            }
            return NoContent();
        }
        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointsOfInterest(int cityId,int id, 
            [FromBody] JsonPatchDocument<PointOfInterestUpdate>patchDoc)
        {
            if (patchDoc==null)
            {
                return BadRequest();
            }
            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterestFromStore = _repository.GetPointOfInterest(cityId,id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch = Mapper.Map<PointOfInterestUpdate>(pointOfInterestFromStore);
            patchDoc.ApplyTo(pointOfInterestToPatch,ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (pointOfInterestToPatch.Name==pointOfInterestToPatch.Description)
            {
                ModelState.AddModelError("Description", "El nombre y descripcion no pueden ser identicos");
            }
            TryValidateModel(pointOfInterestToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Mapper.Map(pointOfInterestToPatch,pointOfInterestFromStore);
            if (!_repository.Save())
            {
                return StatusCode(500, "Something was wrong on server");
            }
            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }
            var pointOfInterestFromStore = _repository.GetPointOfInterest(cityId, id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            _repository.DeletePointOfInterest(pointOfInterestFromStore);
            if (!_repository.Save())
            {
                return StatusCode(500, "Something was wrong on server");
            }
            return NoContent();
        }


    }
}
