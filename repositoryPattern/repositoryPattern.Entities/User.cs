using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace repositoryPattern.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }

        public bool IsAdmin { set; get; }

    }
}
