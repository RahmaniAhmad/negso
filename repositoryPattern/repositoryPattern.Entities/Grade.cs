using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace repositoryPattern.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
