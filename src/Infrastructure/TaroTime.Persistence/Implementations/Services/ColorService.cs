using AutoMapper;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.Persistence.Implementations.Services
{
    internal class ColorService:IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
