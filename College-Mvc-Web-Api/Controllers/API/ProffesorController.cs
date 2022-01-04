using College_Mvc_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace College_Mvc_Web_Api.Controllers.API
{
    public class ProffesorController : ApiController
    {
        string TheConnection = "Data Source=DESKTOP-IGIOI52;Initial Catalog=College;Integrated Security=True";
        // GET: api/Proffesor
        public IHttpActionResult Get()
        {
            try
            {
                List<Proffesor> proffesorList = new List<Proffesor>();
                using (SqlConnection connection = new SqlConnection(TheConnection))
                {
                    connection.Open();
                    string sql = $@"SELECT * FROM Proffesors";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            proffesorList.Add(new Proffesor(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5)));
                        }
                    }
                    connection.Close();
                    return Ok(new { Message = "GOOD", proffesorList });
                }
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
        }

        // GET: api/Proffesor/5

        public IHttpActionResult Get(int id)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(TheConnection))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM Proffesors WHERE Id = {id}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Proffesor proffesor = new Proffesor(dataReader.GetString(1), dataReader.GetString(2), dataReader.GetString(3), dataReader.GetString(4), dataReader.GetInt32(5));
                            return Ok(new { proffesor });

                        }
                    }
                    connection.Close();
                    return Ok(new { MESSAGE = "NOT EXISST" });
                }
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
        }

        // POST: api/Proffesor

        public IHttpActionResult IHttpActionResult([FromBody] Proffesor value)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(TheConnection))
                {
                    connection.Open();
                    string postQuery = $@"INSERT INTO Proffesors (Fname,Lname,Proffetion,Email,Salary)
                    values ('{value.firstName}','{value.lastName}','{value.proffetion}','{value.email}',{value.salary})";
                    SqlCommand command = new SqlCommand(postQuery, connection);
                    int rowsEffected = command.ExecuteNonQuery();
                    connection.Close();
                    return Ok(rowsEffected);
                }
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }

        }

        // PUT: api/Proffesor/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Proffesor value)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(TheConnection))
                {
                    connection.Open();
                    string putQuery = $@"UPDATE Proffesors SET Fname = '{value.firstName}',Lname = '{value.lastName}',Proffetion = '{value.proffetion}',Email = '{value.email}',Salary = {value.salary}";
                    SqlCommand command = new SqlCommand(putQuery, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return Ok(command);
                }
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
        }

        // DELETE: api/Proffesor/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(TheConnection))
                {
                    connection.Open();
                    string deleteQuery = $@"DELETE FROM Proffesors WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    int delete = command.ExecuteNonQuery();
                    connection.Close();
                    return Ok(delete);
                }
            }
            catch (SqlException ex)
            {
                return Ok(ex.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
        }
    }
}
