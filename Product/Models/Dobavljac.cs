﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Models
{
    public class Dobavljac : Base
    {
        [Required]
        public string Naziv { get; set; }

        [Required]
        public string PIB { get; set; }
        
        public string Napomena { get; set; }
        public virtual List<ProizvodDobavljac> Proizvodi { get; set; }
    }
}
