using AutoMapper;
using Product.DTOs;
using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Profiles
{
    public class ProizvodProfile:Profile
    {

        public ProizvodProfile()
        {
            CreateMap<Proizvod, ProizvodReadDTO>()
               .ForMember(x => x.Dobavljaci, x => x.MapFrom(z => z.Dobavljaci.Select(y => new DobavljacDTO { Naziv = y.Dobavljac.Naziv, Id = y.DobavljacId })))
               .ForMember(x => x.TipProizvoda, x => x.MapFrom(z => new TipProizvodaDTO { Naziv = z.TipProizvoda.Naziv, Id = z.TipProizvodaId }))
               .ForMember(x => x.JedinicaMere, x => x.MapFrom(z => new JedinicaMereDTO { Naziv = z.JedinicaMere.Naziv, Id = z.JedinicaMereId }));
            CreateMap<ProizvodDTO, Proizvod>()
                .ForMember(x => x.Dobavljaci, x => x.MapFrom(y => y.Dobavljaci.Select(z => new ProizvodDobavljac { DobavljacId = z })));
        }
    }
}
