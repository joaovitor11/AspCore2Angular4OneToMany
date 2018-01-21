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

        // GET: api/Departamento
        [HttpGet]
        public IEnumerable<Departamento> GetDepartamento()
        {
            return _context.Departamento;
        }

        // GET: api/Departamento/5
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

        // PUT: api/Departamento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento([FromRoute] int id, [FromBody] Departamento departamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departamento.DepartamentoId)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departamento
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

        // DELETE: api/Departamento/5
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