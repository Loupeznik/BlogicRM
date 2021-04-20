﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlogicRM_.Models
{
    public class Institution
    {
        [Key]
        public int InstitutionID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
