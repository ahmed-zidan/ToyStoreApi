using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;
using ToyStore.Core.SharedModels;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class MenuRepo : IMenuRepo
    {
        private readonly AppDbContext _context;
        public MenuRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task addMenu(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
        }

        public void deleteMenu(Menu menu)
        {
            _context.Menus.Remove(menu);
        }

        public async Task<List<Menu>> getAllMenus(string roleName)
        {
            if(roleName.ToUpper() == "ADMIN")
            {
                return await _context.Menus.Where(x=>x.Status==true).ToListAsync();
            }
            else
            {
                return await _context.Menus.Where(x =>x.Status == true && x.menuAccesses.Any(c => c.HaveView == true && c.Role.Name == roleName)).ToListAsync();
            }
            //return await _context.MenuAccesses.Include(x => x.Menu).Where(x => x.Role.NormalizedName == roleName.ToUpper()).ToListAsync();
        }

        public async Task<List<Menu>> getAllMenus()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> getMenu(int Id)
        {
            return await _context.Menus.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<MenuAccess> getMenuAccess(string roleName, string menuName)
        {
            return await _context.MenuAccesses.FirstOrDefaultAsync(x => x.Role.Name == roleName && x.Menu.Name == menuName);
        }


       
        public async Task<bool> isMenuExistAsync(string name)
        {
            return await _context.Menus.AnyAsync(x=>x.Name.ToLower() == name.ToLower());
        }

       

        public async Task updateMenuAccessMenus(List<MenuAccess> menus)
        {
            foreach (var menu in menus) { 
                //update
                if(menu.Id > 0)
                {
                    _context.MenuAccesses.Update(menu);
                }
                else
                {
                    await _context.MenuAccesses.AddAsync(menu);
                }
            }
        }

       
    }
}
