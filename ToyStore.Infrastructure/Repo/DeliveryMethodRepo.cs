using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models.Orders;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Infrastructure.Repo
{
    public class DeliveryMethodRepo : IDeliveryMethodRepo
    {
        private readonly AppDbContext _context;
        public DeliveryMethodRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DeliveryMethod> getDeliveryMethodAsync(int deliveryMethodId)
        {
            return await _context.DeliveryMethods.FirstOrDefaultAsync(x => x.Id == deliveryMethodId);
        }

        public async Task<IEnumerable<DeliveryMethod>> getDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods.ToListAsync();
        }
    }
}
