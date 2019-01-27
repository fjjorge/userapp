using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUsers.Models
{
    public class Location
    {
        [Key]
        public string Uuid { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

    }
}
