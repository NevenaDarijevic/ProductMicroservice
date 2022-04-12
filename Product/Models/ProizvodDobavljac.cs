using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Models
{
    public class ProizvodDobavljac
    {
        public long ProizvodId { get; set; }
        public long DobavljacId { get; set; }
        public Proizvod Proizvod { get; set; }
        public Dobavljac Dobavljac { get; set; }
    }
}
