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
        static string connectionString = "Data Source=DESKTOP-IGIOI52;Initial Catalog=College;Integrated Security=True";
        CollegeDataContext collegeDB = new CollegeDataContext(connectionString);
        // GET: api/Student
        public IHttpActionResult Get()
        {
            try
            {
                //List<Student> stuList= new List<Student>();
                return Ok(collegeDB.students.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {

            try
            {
                return Ok(collegeDB.students.First(studentItem => studentItem.Id == id));
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
        public IHttpActionResult Post([FromBody] student value)
        {
            try
            {
                collegeDB.students.InsertOnSubmit(value);
                collegeDB.SubmitChanges();
                return Ok("sucsses");
            }
            catch (SqlException err)
            {
                return BadRequest(err.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        // PUT: api/Student/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] student value)
        {
            try
            {
                student updateStudent = collegeDB.students.First(studentItem => studentItem.Id == id);
                if (updateStudent != null)
                {
                    updateStudent.Fname = value.Fname;
                    updateStudent.Lname = value.Lname;
                    updateStudent.Birth=value.Birth;
                    updateStudent.Email = value.Email;
                    updateStudent.Year_study = value.Year_study;
                }
                collegeDB.SubmitChanges();
                return Ok("change Saved");
            }
            catch(SqlException err)
            {
                return BadRequest(err.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            collegeDB.students.DeleteOnSubmit(collegeDB.students.First(studentItem => studentItem.Id == id));
            collegeDB.SubmitChanges();
            return Ok("OBG WAS DELETED");

        }
    }
}
