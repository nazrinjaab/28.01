using AutoMapper;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class TagService:ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
