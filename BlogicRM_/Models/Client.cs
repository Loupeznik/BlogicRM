﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogicRM_.Models
{
    public class Client : GenericPerson
    { 
        [Key]
        public int ClientID { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}
