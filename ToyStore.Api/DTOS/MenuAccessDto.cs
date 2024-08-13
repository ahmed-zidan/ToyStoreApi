using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using ToyStore.Core.Models;

namespace ToyStore.Api.DTOS
{
    public class MenuAccessDto
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public IdentityRoleDto Role { get; set; }
        public int MenuId { get; set; }
        public MenuDto Menu { get; set; }
        public bool HaveView { get; set; }
        public bool HaveAdd { get; set; }
        public bool HaveEdit { get; set; }
        public bool HaveDelete { get; set; }
    }
}
