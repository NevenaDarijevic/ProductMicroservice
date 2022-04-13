using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.DTOs
{
    public class ProizvodCUDTO
    {
      //FOR CREATE, UPDATE, PATCH
        [Required]
        public string Naziv { get; set; }
        [Required]
        public double Cena { get; set; }
        [Required]
        public double Pdv { get; set; }
        public long TipProizvodaId { get; set; }
        public long JedinicaMereId { get; set; }
        public ICollection<long> Dobavljaci { get; set; }
    }
}
