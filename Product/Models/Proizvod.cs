using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public class Proizvod : Base
    {
        [Required]
        public string Naziv { get; set; }

        [Required]
        public double Cena { get; set; }
        [Required]
        public double Pdv { get; set; }
        public long TipProizvodaId { get; set; }
        public long JedinicaMereId { get; set; }
        public virtual TipProizvoda TipProizvoda { get; set; }
        public virtual JedinicaMere JedinicaMere { get; set; }
        public virtual List<ProizvodDobavljac> Dobavljaci { get; set; }  
    }
}
