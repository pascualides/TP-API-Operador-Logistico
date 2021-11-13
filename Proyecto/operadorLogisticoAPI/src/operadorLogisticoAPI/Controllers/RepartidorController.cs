using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;
using operadorLogisticoAPI.Repositories.Contexts;
using operadorLogisticoAPI.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace operadorLogisticoAPI.Controllers
{
    
    [Route("[controller]")]
    public class RepartidorController : ControllerBase
    {
        private readonly OperadorContext _context;

        public RepartidorController(OperadorContext context)
        {
            _context = context;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repartidores>> Get(int id)
        {
            var result = await _context.Repartidores.FindAsync(id);

            if (result is null)
            {
                return NotFound($"Repartidor no encontrado para el id: {id}");
            }

            return Ok(result);
        }

        // POST Repartidor
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Repartidores repartidor)
        {
            await _context.Repartidores.AddAsync(repartidor);
            await _context.SaveChangesAsync();

            return Ok(repartidor);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]Repartidores repartidor)
        {
            // Check that the record exists.
            var entity = await _context.Repartidores.FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"Repartidor no encontrado para el id: {id}");
            }

            // Update changes if any of the properties have been modified.
            _context.Entry(entity).CurrentValues.SetValues(repartidor);
            _context.Entry(entity).State = EntityState.Modified;

            if (_context.Entry(entity).Properties.Any(property => property.IsModified))
            {
                await _context.SaveChangesAsync();
            }

            return Ok(entity);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            // Check that the record exists.
            var entity = await _context.Repartidores.FindAsync(id);

            if (entity is null)
            {
                return NotFound($"Repartidor no encontrado para el id: {id}");
            }

            // Set the deleted flag.
            //entity.IsDeleted = true;
            _context.Entry(entity).State = EntityState.Modified;

            // Save changes to the Db Context.
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
