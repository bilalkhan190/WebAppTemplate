using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Application.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<RegisterUserRequestDTO, User>();
        }
    }
}
