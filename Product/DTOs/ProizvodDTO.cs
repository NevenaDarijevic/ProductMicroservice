using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.DTOs
{
    public class ProizvodDTO:BaseDTO
    {
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public double Pdv { get; set; }
        public long TipProizvodaId { get; set; }
        public long JedinicaMereId { get; set; }
        public ICollection<long> Dobavljaci { get; set; }
    }
}
