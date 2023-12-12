using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ExamenAPI.Models;

namespace ExamenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MecanicoController : ControllerBase
    {
        private readonly String cadenaSQL;

        public MecanicoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Mecanico> lista = new List<Mecanico>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_Mecanicos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Mecanico()
                            {
                                IdMecanico = Convert.ToInt32(rd["IdMecanico"]),
                                Nombre = rd["Nombre"].ToString(),
                                Edad = Convert.ToInt32(rd["Edad"]),
                                Domicilio = rd["Domicilio"].ToString(),
                                Titulo = rd["Titulo"].ToString(),
                                Especialidad = rd["Especialidad"].ToString(),
                                SueldoBase = Convert.ToInt32(rd["SueldoBase"]),
                                GratTitulo = Convert.ToInt32(rd["GratTitulo"]),
                                SueldoTotal = Convert.ToInt32(rd["SueldoTotal"])
                            });

                        }
                    }
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }
        }
        [HttpGet]
        [Route("Obtener/{idMecanico:int}")]
        public IActionResult Obtener(int idMecanico)
        {
            List<Mecanico> lista = new List<Mecanico>();
            Mecanico mecanico = new Mecanico();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_Mecanicos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Mecanico()
                            {
                                IdMecanico = Convert.ToInt32(rd["IdMecanico"]),
                                Nombre = rd["Nombre"].ToString(),
                                Edad = Convert.ToInt32(rd["Edad"]),
                                Domicilio = rd["Domicilio"].ToString(),
                                Titulo = rd["Titulo"].ToString(),
                                Especialidad = rd["Especialidad"].ToString(),
                                SueldoBase = Convert.ToInt32(rd["SueldoBase"]),
                                GratTitulo = Convert.ToInt32(rd["GratTitulo"]),
                                SueldoTotal = Convert.ToInt32(rd["SueldoTotal"])
                            });

                        }
                    }
                    mecanico = lista.Where(item => item.IdMecanico == idMecanico).FirstOrDefault();
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = mecanico });
                }
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = mecanico });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Mecanico objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_Mecanico", conexion);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("Edad", objeto.Edad);
                    cmd.Parameters.AddWithValue("Domicilio", objeto.Domicilio);
                    cmd.Parameters.AddWithValue("Titulo", objeto.Titulo);
                    cmd.Parameters.AddWithValue("Especialidad", objeto.Especialidad);
                    cmd.Parameters.AddWithValue("SueldoBase", objeto.SueldoBase);
                    cmd.Parameters.AddWithValue("GratTitulo", objeto.GratTitulo);
                    cmd.Parameters.AddWithValue("SueldoTotal", objeto.SueldoTotal);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Mecanico objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_Mecanico", conexion);
                    cmd.Parameters.AddWithValue("IdMecanico", objeto.IdMecanico == 0 ? DBNull.Value : objeto.IdMecanico);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                    cmd.Parameters.AddWithValue("Edad", objeto.Edad == 0 ? DBNull.Value : objeto.Edad);
                    cmd.Parameters.AddWithValue("Domicilio", objeto.Domicilio is null ? DBNull.Value : objeto.Domicilio);
                    cmd.Parameters.AddWithValue("Titulo", objeto.Titulo is null ? DBNull.Value : objeto.Titulo);
                    cmd.Parameters.AddWithValue("Especialidad", objeto.Especialidad is null ? DBNull.Value : objeto.Especialidad);
                    cmd.Parameters.AddWithValue("SueldoBase", objeto.SueldoBase == 0 ? DBNull.Value : objeto.SueldoBase);
                    cmd.Parameters.AddWithValue("GratTitulo", objeto.GratTitulo == 0 ? DBNull.Value : objeto.GratTitulo);
                    cmd.Parameters.AddWithValue("SueldoTotal", objeto.SueldoTotal == 0 ? DBNull.Value : objeto.SueldoTotal);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado", });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, });
            }
        }
        [HttpDelete]
        [Route("Eliminar/{idMecanico:int}")]
        public IActionResult Eliminar(int idMecanico)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_Mecanico", conexion);
                    cmd.Parameters.AddWithValue("idMecanico", idMecanico);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado", });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, });
            }
        }
    }
}