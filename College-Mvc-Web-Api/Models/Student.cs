using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College_Mvc_Web_Api.Models
{
    public class Student
    {
       public string firstName;
       public string lastName;
       public DateTime Birth;
       public string Email;
       public int StudyYear;

        public Student(string firstName, string lastName, DateTime birth, string email, int studyYear)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            Birth = birth;
            Email = email;
            StudyYear = studyYear;
        }
    }
}