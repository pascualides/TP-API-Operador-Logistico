using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;
using System.IO;
using Newtonsoft.Json;

using System.Net;
using Newtonsoft.Json.Linq;


namespace operadorLogisticoAPI.Controllers
{
    
    [Route("[controller]")]
    public class EnviosController : ControllerBase
    {

        public string getConnectionString() {
            return "Data Source=" + "operador-logistico-db.c8f01er7irve.us-east-1.rds.amazonaws.com,1433" +";database = operador"+ ";User ID=" + "admin" + ";Password=" + "tp-iaew-2021;" + "TrustServerCertificate=true;";
        }

        

        // GET api/values
        // [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     using (SqlConnection connection = new SqlConnection(
        //        getConnectionString()))
        //     {
        //         SqlCommand command = new SqlCommand("select * from envios", connection);
        //         command.Connection.Open();
        //         //command.ExecuteNonQuery();
        //         command.BeginExecuteReader();
        //     }
        //     return new string[] { "value1", "value2" };

            
        // }

        
        // GET /Envios
        [HttpGet]
        public async Task<string> Get()
        {
            SqlConnection connection = new SqlConnection(getConnectionString());
            
            SqlCommand command = new SqlCommand("select * from envio e join contacto c on e.dniContacto = c.documento join producto p on e.nroProducto = p.id FOR JSON PATH", connection);
            command.Connection.Open();
                //command.ExecuteNonQuery();
            var result = await command.ExecuteReaderAsync();
            var jsonResult = new StringBuilder();

            if (!result.HasRows)
            {
            }
            else
            {
                while (result.Read())
                {
                //jsonResult.Append(result.GetValue(0).ToString());
                    jsonResult.Append(result[0].ToString());            
                }
            }


            if (connection.State == System.Data.ConnectionState.Open) 
            connection.Close();

            return jsonResult.ToString();
 
        }

        // GET Envios/:id
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            
            SqlConnection connection = new SqlConnection(getConnectionString());
            
            SqlCommand command = new SqlCommand("select * from envio e join contacto c on e.dniContacto = c.documento join producto p on e.nroProducto = p.id where e.id ="+id + " FOR JSON PATH", connection);
            command.Connection.Open();
                //command.ExecuteNonQuery();
            var result = await command.ExecuteReaderAsync();
            var jsonResult = new StringBuilder();

            if (!result.HasRows)
            {
            }
            else
            {
                while (result.Read())
                {
                //jsonResult.Append(result.GetValue(0).ToString());
                    jsonResult.Append(result[0].ToString());            
                }
            }


            if (connection.State == System.Data.ConnectionState.Open) 
            connection.Close();

            return jsonResult.ToString();
        }

        // POST Envios/
        [HttpPost]
        public async Task<string> Post([FromBody]string value)
        {
            Request.Body.Position = 0;

            var body = new StreamReader(Request.Body).ReadToEnd();
            var envio = JObject.Parse(body);

            SqlConnection connection = new SqlConnection(getConnectionString());
    

            var queryEnvio = " insert into envio (provinciaDestino, codPostalDestino, localidadDestino, calleDestino, nroDestino, provinciaOrigen, codPostalOrigen, localidadOrigen, calleOrigen, nroOrigen, dniContacto, estado, fechaRecepcion, pesoProd, tamañoProd, delicado) ";

                queryEnvio += "values ('"+Convert.ToString(envio["destino"]["provincia"])+"', "+Convert.ToString(envio["destino"]["codPostal"])+", '"+Convert.ToString(envio["destino"]["localidad"])+"', '"+Convert.ToString(envio["destino"]["calle"])+"', "+Convert.ToString(envio["destino"]["nro"])+", ";
        
                queryEnvio += "'"+Convert.ToString(envio["origen"]["provincia"])+"', "+Convert.ToString(envio["origen"]["codPostal"])+", '"+Convert.ToString(envio["origen"]["localidad"])+"', '"+Convert.ToString(envio["origen"]["calle"])+"', "+Convert.ToString(envio["origen"]["nro"]);
        
                queryEnvio += ", "+Convert.ToString(envio["contacto"]["documento"])+",'creado', getdate(), "+Convert.ToString(envio["producto"]["peso"]) +", '"+Convert.ToString(envio["producto"]["tamaño"])+"' , '"+Convert.ToString(envio["producto"]["delicado"])+"' )"; 



            
            var queryContacto = "If Not Exists(select * from contacto where documento = "+Convert.ToString(envio["contacto"]["documento"])+")  Begin ";
                queryContacto += "insert into contacto (documento, nombre, apellido, email) values (";
                queryContacto += Convert.ToString(envio["contacto"]["documento"])+", '"+Convert.ToString(envio["contacto"]["nombre"])+"', '"+Convert.ToString(envio["contacto"]["apellido"])+"', '"+Convert.ToString(envio["contacto"]["email"])+"')  End";
            
            
            SqlCommand command = new SqlCommand(queryEnvio, connection);

            command.Connection.Open();

            command.ExecuteNonQuery();

            command = new SqlCommand(queryContacto, connection);

            command.ExecuteNonQuery();
       
        
        if (connection.State == System.Data.ConnectionState.Open) 
            connection.Close();
        
        
        
        return queryContacto;


        
        }

        // POST Envios/5/entrega
         [Route("{envioId}/entrega")]
        [HttpPost]
        public async Task<string> Post()
        {
            return "ENTRA ACA";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
