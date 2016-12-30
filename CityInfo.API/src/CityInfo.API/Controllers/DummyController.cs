using CityInfo.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    public class DummyController:Controller
    {
        private CityInfoDbContext _context;

        public DummyController(CityInfoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GenerateDb()
        {
           
            return Ok();
        }
    }
}
