using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College_Mvc_Web_Api.Models
{
    public class Proffesor
    {
        public string firstName;
        public string lastName;
        public string proffetion;
        public string email;
        public int salary;

        public Proffesor(string firstName, string lastName, string proffetion, string email, int salary)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.proffetion = proffetion;
            this.email = email;
            this.salary = salary;
        }
    }
}