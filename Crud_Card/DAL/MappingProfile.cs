using AutoMapper;
using Crud_Card.DAL.Entities;
using Crud_Card.Models;

namespace Crud_Card.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
           // CreateMap<Card, CardDto>();
            CreateMap<CardDto, Card>().ReverseMap();
        }
    }
}
