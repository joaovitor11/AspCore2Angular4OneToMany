using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspCore2Angular4OneToMany.Models;

namespace AspCore2Angular4OneToMany.Controllers
{
    [Produces("application/json")]
    [Route("api/Departamento")]
    public class DepartamentoController : Controller
    {
        private readonly db_teste1Context _context;

        public DepartamentoController(db_teste1Context context)
        {
            _context = context;
        }

        [Route("~/api/GetAllDepartamento")]
        [HttpGet]
        public IEnumerable<Departamento> GetDepartamento()
        {
            return _context.Departamento;
        }

        [Route("~/api/GetDepartamento")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartamento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync(m => m.DepartamentoId == id);

            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        [Route("~/api/UpdateDepartamento")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento([FromBody] Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        [Route("~/api/AddDepartamento")]
        [HttpPost]
        public async Task<IActionResult> PostDepartamento([FromBody] Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Departamento.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartamento", new { id = departamento.DepartamentoId }, departamento);
        }

        [Route("~/api/DeleteDepartamento/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departamento = await _context.Departamento.SingleOrDefaultAsync(m => m.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();

            return Ok(departamento);
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamento.Any(e => e.DepartamentoId == id);
        }
    }
}