using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static _20231005_dolgozat_jegyek_WebAPI.Controllers.Dtos;

namespace _20231005_dolgozat_jegyek_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JegyekController : ControllerBase
    {
        Connect connect = new();
        private readonly List<JegyekDto> jegyek = new();

        [HttpGet]
        public ActionResult<IEnumerable<JegyekDto>> Get() 
        {
            try
            {
                connect.connection.Open();
                string sqlcommand = "SELECT * FROM jegynaplo";
                MySqlCommand cmd = new MySqlCommand(sqlcommand);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    var result = new JegyekDto(
                        reader.GetGuid("id"),
                        reader.GetInt32("jegy"),
                        reader.GetString("leiras"),
                        reader.GetDateTime("letrejottido")
                        );
                    jegyek.Add(result);
                }
                connect.connection.Close();
                return StatusCode(200, jegyek);
            }
            catch (Exception ex1)
            {
                return BadRequest(ex1.Message);
            }
        }
    }
}
