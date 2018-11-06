using Microsoft.AspNetCore.Mvc;
using Onion.Service;
using System.Linq;

namespace Onion.API.Controllers
{
    public class CrudController<TEnt> : ControllerBase
        where TEnt : class
    {
        private readonly IEntityService<TEnt> entityService;

        public CrudController(IEntityService<TEnt> entityService)
        {
            this.entityService = entityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(entityService.GetEntities().ToList());
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            TEnt entity = entityService.GetEntity(id);

            if (entity != null)
                return Ok(entity);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TEnt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entityService.CreateEntity(entity);
            entityService.Save();

            return CreatedAtAction(nameof(Get), entity);
        }

        [HttpPost]
        public IActionResult Update([FromBody] TEnt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entityService.UpdateEntity(entity);
            entityService.Save();

            return CreatedAtAction(nameof(Get), entity);
        }

        [HttpPost]
        public IActionResult Delete([FromBody] TEnt entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entityService.DeleteEntity(entity);
            entityService.Save();

            return Ok();
        }
    }
}
