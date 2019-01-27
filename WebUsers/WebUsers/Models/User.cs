using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUsers.Models
{
    public class User
    {
        public string IdValue { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        [Key]
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public Location Location { get; set; }

    }
}
