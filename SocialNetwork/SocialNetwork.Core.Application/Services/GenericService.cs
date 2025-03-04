using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Core.Application.Services
{
    public class GenericService<ViewModel, SaveViewModel, Entity> : IGenericService<ViewModel, SaveViewModel, Entity>
        where ViewModel : class
        where SaveViewModel : class
        where Entity : class
    {

        private readonly IGenericRepository<Entity> _repository;

        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<SaveViewModel> CreateAsync(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repository.AddAsync(entity);
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllViewModel()
        {
            List<Entity> entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }

        public virtual async Task<SaveViewModel> GetByIdSaveViewModel(int id)
        {
            Entity entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task<SaveViewModel> UpdateAsync(SaveViewModel vm, int id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            entity = await _repository.UpdateAsync(entity, id);
            return _mapper.Map<SaveViewModel>(entity);
        }
    }
}
