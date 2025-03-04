namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel, Entity> where  ViewModel : class where SaveViewModel : class where Entity : class
    {
        Task<SaveViewModel> CreateAsync(SaveViewModel vm);
        Task<SaveViewModel> UpdateAsync(SaveViewModel vm, int id);

        Task DeleteAsync(int id);

        Task<SaveViewModel> GetByIdSaveViewModel(int id);

        Task<List<ViewModel>> GetAllViewModel();
    }
}
