using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using MovieBox.Models;
using MovieBox.Dtos;

namespace MovieBox.Areas.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap()
                                        .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Movie, MovieDto>().ReverseMap()
                                   .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}
