using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogicRM_.Models
{
    public class Client : GenericPerson
    { 
        [Key]
        public Guid ClientID { get; set; }
    }
}
