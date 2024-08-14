using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyStore.Core.Models
{
    public class Menu:BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool Status { get; set; }
        public ICollection<MenuAccess> menuAccesses { get; set; }
    }
}
