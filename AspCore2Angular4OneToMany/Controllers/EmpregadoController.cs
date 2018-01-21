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
        public IActionResult GetEmpregado()
        {
            var result = from empregado in _context.Empregado
                         join dep in _context.Departamento on empregado.DepartamentoId equals dep.DepartamentoId
                         select new
                         {
                             empid = empregado.EmpregadoId,
                             depid = dep.DepartamentoId,
                             depnome = dep.Nome,
                             empnome = empregado.Nome,
                             empsob = empregado.Sobrenome,
                             empemail = empregado.Email
                                                          
                         };
            return Ok(result);
        }

        [Route("~/api/GetEmpregado")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpregado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = from empregado in _context.Empregado
                         join dep in _context.Departamento on empregado.DepartamentoId equals dep.DepartamentoId where empregado.EmpregadoId == id
                         select new
                         {
                             empid = empregado.EmpregadoId,
                             depid = dep.DepartamentoId,
                             depnome = dep.Nome,
                             empnome = empregado.Nome,
                             empsob = empregado.Sobrenome,
                             empemail = empregado.Email

                         };
            
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("~/api/UpdateEmpregado")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpregado([FromRoute] int id, [FromBody] Empregado empregado)
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