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
    public class StudentController : ApiController
    {
        string connectionString = "Data Source=DESKTOP-IGIOI52;Initial Catalog=College;Integrated Security=True";
        // GET: api/Student
        public IHttpActionResult Get()
        {
            try
            {
                List<Student> stuList = new List<Student>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $@"SELECT * FROM student";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            stuList.Add(new Student(dr.GetString(1), dr.GetString(2), dr.GetDateTime(3), dr.GetString(4), dr.GetInt32(5)));
                        }
                    }
                    connection.Close();
                    return Ok(new { MESSAGE = "NEW STUDENT HAS BEEN ADDED TO YOUR LIST", stuList });
                }
            }
            catch (Exception err)
            {
                return Ok(err.Message);
            }
            catch
            {
                return Ok(new { Message = "GENERAL ERROR" });
            }
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            //Get();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT * FROM student WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader datadb = command.ExecuteReader();
                    if (datadb.HasRows)
                    {
                        while (datadb.Read())
                        {
                            Student stu = new Student(datadb.GetString(1), datadb.GetString(2), datadb.GetDateTime(3), datadb.GetString(4), datadb.GetInt32(5));
                            return Ok(new { stu });
                        }
                    }
                    connection.Close();
                    return Ok(new { MESSAGE = "NOT EXISST" });
                }
            }
            catch (Exception err)
            {
                return Ok(err.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody] Student value)
        {
            Get();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string postQuery = $@"INSERT INTO student(Fname,Lname,Birth,Email,Year_study)
                    values('{value.firstName}','{value.lastName}','{value.Birth}','{value.Email}',{value.StudyYear}) ";
                    SqlCommand command = new SqlCommand(postQuery, connection);
                    int rows = command.ExecuteNonQuery();
                    connection.Close();
                }
                return Ok(new { value });
            }
            catch (SqlException err)
            {
                return Ok(err.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
        }
        // PUT: api/Student/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Student value)
        {
            Get();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string putQuery = $@"UPDATE student SET Fname='{value.firstName}',Lname='{value.lastName}',Birth='{value.Birth}',Email='{value.Email}',Year_study={value.StudyYear} 
                                       WHERE Id = {id}";
                    SqlCommand sqlCommand = new SqlCommand(putQuery, connection);
                    int updaterow = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception err)
            {
                return Ok(err.Message);
            }
            catch
            {
                return Ok(new { MESSAGE = "GENERAL ERROR" });
            }
            return Ok();
        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = $@"DELETE FROM student WHERE Id = {id}";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    int deletedaterow = deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (SqlException err)
            {
                return Ok(err.Message);
            }
            catch
            {

            }
            return Ok();
        }
    }
}
