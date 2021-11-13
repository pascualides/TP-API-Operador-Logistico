using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using operadorLogisticoAPI.Repositories.Entities;
using operadorLogisticoAPI.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;

namespace operadorLogisticoAPI.Controllers
{
    
    [Route("ordenes_envio")]
    [ApiController]
    public class OrdenEnvioController : ControllerBase
    {
        private readonly OperadorContext _context;

        public OrdenEnvioController(OperadorContext context)
        {
            _context = context;
        }


        // GET Envios/:id
        [HttpGet("{id}")]
        public async Task<ActionResult<Envio>> GetById(int id)
        {
            var result =await _context.Envio.FindAsync(id);

            if (result is null)
            {
                return NotFound($"Envio no encontrado para el id: {id}");
            }

            return Ok(result);
        }

        // POST Envios/
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Envio envio)
        {
            await _context.Envio.AddAsync(envio);
            await _context.SaveChangesAsync();

            return Ok(envio);
        }

        // POST Envios/5/entrega
        [Route("{envioId}/entrega")]
        [HttpPost]
        public async Task<IActionResult> UpdateToEntregadoStatus([FromQuery] int envioId)
        {
            // Check that the record exists.
            var entity = await _context.Envio.FindAsync(envioId);

            entity.Estado = "Entregado";
            entity.FechaEntrega = DateTime.Now;

            var updatedEnvio = await this.Update(envioId, entity);

            return Ok(updatedEnvio);
        }

        // Put Envios/5/repartidor
        [Route("{envioId}/repartidor")]
        [HttpPost]
        public async Task<IActionResult> UpdateToEnTransito([FromQuery] int envioId, int documentoRepartidor)
        {
            // Check that the record exists.
            var entity = await _context.Envio.FindAsync(envioId);

            entity.Estado = "En Transito";
            entity.DniRepartidor = documentoRepartidor;

            var updatedEnvio = await this.Update(envioId, entity);

            return Ok(updatedEnvio);
        }

        private async Task<Envio> Update(int envioId, Envio updateEnvio)
        {
            // Check that the record exists.
            var entity = await _context.Envio.FindAsync(envioId);

            if (entity == null)
            {
                throw new Exception($"Envio no encontrado para el id: {envioId}");
            }

            // Update changes if any of the properties have been modified.
            _context.Entry(entity).CurrentValues.SetValues(updateEnvio);
            _context.Entry(entity).State = EntityState.Modified;

            if (_context.Entry(entity).Properties.Any(property => property.IsModified))
            {
                await _context.SaveChangesAsync();
            }

            return entity;
        }

    }
}
