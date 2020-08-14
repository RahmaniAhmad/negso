using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace repositoryPattern.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
