using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace repositoryPattern.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
