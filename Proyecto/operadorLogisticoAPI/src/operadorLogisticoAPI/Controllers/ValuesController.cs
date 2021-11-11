using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;

namespace operadorLogisticoAPI.Controllers
{
    
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
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

        
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            SqlConnection connection = new SqlConnection(getConnectionString());
            
            SqlCommand command = new SqlCommand("select * from envio FOR JSON PATH", connection);
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
            return jsonResult.ToString();
 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
