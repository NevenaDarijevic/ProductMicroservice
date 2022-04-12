using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.DTOs
{
    public class ProizvodReadDTO
    {
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public double Pdv { get; set; }
        public TipProizvodaDTO TipProizvoda { get; set; }
        public JedinicaMereDTO JedinicaMere { get; set; }
        public List<DobavljacDTO> Dobavljaci { get; set; }
       
    }
}
