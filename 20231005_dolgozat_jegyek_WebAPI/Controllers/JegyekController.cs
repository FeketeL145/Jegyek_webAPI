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
                MySqlCommand cmd = new MySqlCommand(sqlcommand, connect.connection);
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
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<JegyekDto>> GetId(Guid id)
        {
            try
            {
                connect.connection.Open();
                string sqlcommand = $"SELECT * FROM jegynaplo WHERE jegynaplo.id == '@id'";
                MySqlCommand cmd = new MySqlCommand(sqlcommand, connect.connection);
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
        [HttpPost]
        public ActionResult<IEnumerable<CreateJegyekDto>> Post(CreateJegyekDto createJegyek)
        {
            Guid id = Guid.NewGuid();
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                connect.connection.Open();
                string sqlcommand = $"INSERT INTO `jegynaplo`(`id`, `jegy`, `leiras`, `letrejottido`) VALUES (@id,@jegy,@leiras,@letrejottido)";
                MySqlCommand cmd = new MySqlCommand(sqlcommand, connect.connection);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("jegy", createJegyek.jegy);
                cmd.Parameters.AddWithValue("leiras", createJegyek.leiras);
                cmd.Parameters.AddWithValue("letrejottido", time);
                cmd.ExecuteNonQuery();
                connect.connection.Close();
                return StatusCode(200, jegyek);
            }
            catch (Exception ex1)
            {
                return BadRequest(ex1.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<UpdateJegyekDto>> Put(UpdateJegyekDto updateJegyek, Guid id)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                connect.connection.Open();
                string sqlcommand = $"UPDATE `jegynaplo` SET `jegy`=@jegy,`leiras`=@leiras,`letrejottido`=@letrejottido WHERE `id` =@id";
                MySqlCommand cmd = new MySqlCommand(sqlcommand, connect.connection);
                cmd.Parameters.AddWithValue("jegy", updateJegyek.jegy);
                cmd.Parameters.AddWithValue("leiras", updateJegyek.leiras);
                cmd.Parameters.AddWithValue("letrejottido", time);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                connect.connection.Close();
                return StatusCode(200, jegyek);
            }
            catch (Exception ex1)
            {
                return BadRequest(ex1.Message);
            }
        }
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            try
            {
                connect.connection.Open();
                string sqlcommand = $"DELETE FROM jegynaplo WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sqlcommand, connect.connection);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                connect.connection.Close();
                return StatusCode(200, "Successfully deleted record");
            }
            catch (Exception ex1)
            {
                return BadRequest(ex1.Message);
            }
        }
    }
}
