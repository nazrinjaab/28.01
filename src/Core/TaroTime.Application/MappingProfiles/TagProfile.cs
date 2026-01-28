using AutoMapper;
using TaroTime.Application.DTOs;
using TaroTime.Domain.Entities;

namespace TaroTime.Application.MappingProfiles
{
    internal class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag,GetTagInProductDto>();
        }
    }
}
