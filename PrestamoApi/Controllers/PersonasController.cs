using Microsoft.AspNetCore.Mvc;
using PrestamoApi.BLL;
using PrestamoApi.Models;
using System.Collections.Generic;

namespace PrestamoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController : ControllerBase
    {
        public PersonasController() { }

        [HttpGet]
        public ActionResult<List<Personas>> GetAll() =>
        PersonasBLL.GetList();

        [HttpGet("{id}")]
        public ActionResult<Personas> Get(int id)
        {
            var persona = PersonasBLL.Buscar(id);

            if (persona == null)
                return NotFound();

            return persona;
        }

        [HttpPost]
        public IActionResult Create(Personas personas)
        {
            PersonasBLL.Guardar(personas);
            return CreatedAtAction(nameof(Create), new { id = personas.PersonaId }, personas);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Personas personas)
        {
            if (id != personas.PersonaId)
                return BadRequest();

            var existingPersona = PersonasBLL.Buscar(id);
            if (existingPersona is null)
                return NotFound();

            PersonasBLL.Guardar(personas);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var persona = PersonasBLL.Buscar(id);

            if (persona is null)
                return NotFound();

            PersonasBLL.Eliminar(id);

            return NoContent();
        }
    }
}
