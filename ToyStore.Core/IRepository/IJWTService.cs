using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.Models;

namespace ToyStore.Core.Repository
{
    public interface IJWTService
    {
        string GetToken(AppUser user);
    }
}
