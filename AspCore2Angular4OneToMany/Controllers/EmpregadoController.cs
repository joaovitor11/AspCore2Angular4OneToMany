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
    [Route("api/Empregado")]
    public class EmpregadoController : Controller
    {
        private readonly db_teste1Context _context;

        public EmpregadoController(db_teste1Context context)
        {
            _context = context;
        }

        [Route("~/api/GetAllEmpregado")]
        [HttpGet]
        public IEnumerable<Empregado> GetEmpregado()
        {
            return _context.Empregado;
        }

        [Route("~/api/GetEmpregado")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpregado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empregado = await _context.Empregado.SingleOrDefaultAsync(m => m.EmpregadoId == id);

            if (empregado == null)
            {
                return NotFound();
            }

            return Ok(empregado);
        }

        [Route("~/api/UpdateEmpregado")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpregado([FromBody] Empregado empregado)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(empregado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [Route("~/api/AddEmpregado")]
        [HttpPost]
        public async Task<IActionResult> PostEmpregado([FromBody] Empregado empregado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Empregado.Add(empregado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpregado", new { id = empregado.EmpregadoId }, empregado);
        }

        [Route("~/api/DeleteEmpregado/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpregado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empregado = await _context.Empregado.SingleOrDefaultAsync(m => m.EmpregadoId == id);
            if (empregado == null)
            {
                return NotFound();
            }

            _context.Empregado.Remove(empregado);
            await _context.SaveChangesAsync();

            return Ok(empregado);
        }

        private bool EmpregadoExists(int id)
        {
            return _context.Empregado.Any(e => e.EmpregadoId == id);
        }
    }
}