using AutoMapper;
using Product.DTOs;
using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Profiles
{
    public class TipProizvodaProfile : Profile
    {
        public TipProizvodaProfile()
        {
            CreateMap<TipProizvoda, TipProizvodaDTO>();
            CreateMap<TipProizvodaDTO, TipProizvoda>();
        }
    }
}
