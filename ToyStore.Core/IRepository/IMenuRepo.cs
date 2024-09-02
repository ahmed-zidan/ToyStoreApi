using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;
using ToyStore.Core.SharedModels;

namespace ToyStore.Core.IRepository
{
    public interface IMenuRepo
    {
        Task addMenu(Menu menu);
        void deleteMenu(Menu menu);
        Task updateMenuAccessMenus(List<MenuAccess> menus);
        Task<List<Menu>> getAllMenus(string roleName);
        Task<List<Menu>> getAllMenus();
        Task<MenuAccess> getMenuAccess(string roleName ,string menuName);
        Task<bool> isMenuExistAsync(string name);
        Task<Menu> getMenu(int Id);
       
    }
}
