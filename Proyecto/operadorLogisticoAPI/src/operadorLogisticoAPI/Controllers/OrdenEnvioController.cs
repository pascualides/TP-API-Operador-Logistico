using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using operadorLogisticoAPI.Repositories.Entities;
using operadorLogisticoAPI.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using RestSharp;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
        [Authorize(Policy = "write:envios")]
        public async Task<IActionResult> CreateAsync([FromBody]Envio envio)
        {

            var result = await _context.Repartidores.FindAsync(envio.contacto.Documento);

            if (result is null)
            {
                await _context.Contacto.AddAsync(envio.contacto);
            }
            

            envio.FechaRecepcion = DateTime.Now;
            envio.Estado = "Creado";
            envio.DniContacto = envio.contacto.Documento;
            
            await _context.Envio.AddAsync(envio);
            await _context.SaveChangesAsync();

            return Ok(envio);
        }

        // POST Envios/5/entrega
        [Route("{envioId}/entrega")]
        [HttpPost]
        [Authorize(Policy = "write:estados_envios")]
        public async Task<IActionResult> UpdateToEntregadoStatus(int envioId)
        {
            // Check that the record exists.
            var entity = await _context.Envio.FindAsync(envioId);

            if (entity is null)
            {
                return NotFound($"Envio no encontrado para el id: {envioId}");
            }

            entity.Estado = "Entregado";
            entity.FechaEntrega = DateTime.Now;

            var updatedEnvio = await this.Update(envioId, entity);



            return Ok(updatedEnvio);
        }

        // Put Envios/5/repartidor
        [Route("{envioId}/repartidor")]
        [HttpPost]
        [Authorize(Policy = "write:estados_envios")]
        public async Task<IActionResult> UpdateToEnTransito(int envioId, [FromBody] Repartidores repartidor)
        {
            // Check that the record exists.
            var entity = await _context.Envio.FindAsync(envioId);

            entity.Estado = "En Transito";
            entity.DniRepartidor = repartidor.Documento;

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

        private async Task notificarCambioEstado(int idEnvio)
        {   
            var token = new tokenResponse();

            var client = new RestClient("https://dev-proc-envios.us.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"dLj4RGx8oemIGcZX8jsyPqJbRHI0AReW\",\"client_secret\":\"zonDuV6RdCoF7pfg3KEiQoZ4ld9S6CKo6LChRvqzATpkiwGbiq-ot1j9eZlsD4pa\",\"audience\":\"https://www.api-procesador-envios.com/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse<tokenResponse> response = await client.ExecuteAsync<tokenResponse>(request);
            
            token = response.Data;

            client = new RestClient($"https://xo2gv4p0wc.execute-api.us-east-1.amazonaws.com/Prod/api/envios/{idEnvio}/novedades/?novedades=Entregado");
            request = new RestRequest(Method.POST);
            request.AddHeader("authorization",$"{token.token_type} {token.access_token}");
            IRestResponse res = await client.ExecuteAsync(request);
        }

    }
}
