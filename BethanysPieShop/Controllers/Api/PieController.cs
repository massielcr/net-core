using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieController(IPieRepository pieRepository) : ControllerBase
    {
        private readonly IPieRepository _pieRepository = pieRepository;

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = _pieRepository.GetAll().OrderBy(p => p.PieId);
            return Ok(allPies);
        }

        [HttpPost("search")]
        public IActionResult SearchPies([FromBody] SearchRequest searchRequest)
        {
            IEnumerable<Pie> pies = [];
            if(!string.IsNullOrWhiteSpace(searchRequest.SearchQuery))
            {
                pies = _pieRepository.SearchPies(searchRequest.SearchQuery);
            }

            return new JsonResult(pies);
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
        public IActionResult CreatePie(Pie pie)
        {
            _pieRepository.CreatePie(pie);
            return CreatedAtAction(nameof(GetById), new { id = pie.PieId }, pie);
        }

        public IActionResult UpdatePie(int id, Pie pie)
        {
            if (id != pie.PieId)
            {
                return BadRequest();
            }
            var existingPie = _pieRepository.GetById(id);
            if (existingPie == null)
            {
                return NotFound();
            }
            _pieRepository.UpdatePie(pie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePie(int id)
        {
            var existingPie = _pieRepository.GetById(id);
            if (existingPie == null)
            {
                return NotFound();
            }
            _pieRepository.DeletePie(id);
            return NoContent();
        }
    }
}
