
using ToyStore.Core.Models;

namespace ToyStore.Core.IRepository
{
    public interface ISizeRepo
    {
        Task<IEnumerable<Size>> GetSizesAsync();
        Task<Size> Get(int id);
        Task AddSizeAsync(Size size);
        Task<bool> IsExist(string name);
        void RemoveSizeAsync(Size size);
        Task<IEnumerable<Size>> GetSizesAsync(List<int> Ids);
    }
}
