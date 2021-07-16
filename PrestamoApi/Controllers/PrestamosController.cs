using Microsoft.AspNetCore.Mvc;
using PrestamoApi.BLL;
using PrestamoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrestamosController : ControllerBase
    {
        public PrestamosController() { }

        [HttpGet]
        public ActionResult<List<Prestamos>> GetAll() =>
        PrestamosBLL.GetList();

        [HttpGet("{id}")]
        public ActionResult<Prestamos> Get(int id)
        {
            var prestamo = PrestamosBLL.Buscar(id);

            if (prestamo == null)
                return NotFound();

            return prestamo;
        }

        [HttpPost]
        public IActionResult Create(Prestamos prestamos)
        {
            PrestamosBLL.Guardar(prestamos);
            return CreatedAtAction(nameof(Create), new { id = prestamos.PrestamoId }, prestamos);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Prestamos prestamos)
        {
            if (id != prestamos.PrestamoId)
                return BadRequest();

            var existingPrestamo = PrestamosBLL.Buscar(id);
            if (existingPrestamo is null)
                return NotFound();

            PrestamosBLL.Guardar(prestamos);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prestamo = PrestamosBLL.Buscar(id);

            if (prestamo is null)
                return NotFound();

            PrestamosBLL.Eliminar(id);

            return NoContent();
        }
    }
}
