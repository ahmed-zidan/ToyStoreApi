

using ToyStore.Core.Models;

namespace ToyStore.Core.IRepository
{
    public interface IColorRepo
    {
        Task<IEnumerable<Color>> GetColorsAsync();
        Task<Color> Get(int id);
        Task AddColorAsync(Color color);
        Task<bool> IsExist(string name);
        void RemoveColorAsync(Color color);
        Task<IEnumerable<Color>> GetColorsAsync(List<int> Ids);
    }
}
