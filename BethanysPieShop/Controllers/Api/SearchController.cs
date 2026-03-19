using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController(IPieRepository pieRepository) : ControllerBase
    {
        private readonly IPieRepository _pieRepository = pieRepository;

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.GetAll();
            return Ok(allPies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pie = _pieRepository.GetById(id);
            if (pie == null)
            {
                return NotFound();
            }
            return Ok(pie);
        }

        [HttpPost]
        public IActionResult SearchPies([FromBody] SearchRequest searchRequest)
        {
            IEnumerable<Pie> pies = [];
            if(!string.IsNullOrWhiteSpace(searchRequest.SearchQuery))
            {
                pies = _pieRepository.SearchPies(searchRequest.SearchQuery);
            }

            return new JsonResult(pies);
        }
    }
}
