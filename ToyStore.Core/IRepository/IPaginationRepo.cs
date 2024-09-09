using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.SharedModels;

namespace ToyStore.Core.IRepository
{
    public interface IPaginationRepo
    {
        Task<List<T>> GetDataByDynamicPropertyAsync<T>(GenericPagination pagination) where T : class;
        Task<List<T>> GetDataByDynamicPropertyAsync<T>(GenericPagination pagination, List<string> includes) where T : class;
    }
}
