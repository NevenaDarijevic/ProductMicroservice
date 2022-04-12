using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Models
{
    public class TipProizvoda : Base
    {
        [Required]
        public string Naziv { get; set; }
        
    }
}
