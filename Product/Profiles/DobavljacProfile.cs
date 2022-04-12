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
    public class DobavljacProfile : Profile
    {
        public DobavljacProfile()
        {
            CreateMap<Dobavljac, DobavljacDTO>();
            CreateMap<DobavljacDTO, Dobavljac>();
        }
    }
}
