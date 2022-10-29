using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBusketAsync(string basketId);
        Task<CustomerBasket> UpdateBusketAsync(CustomerBasket basket);
        Task<bool> DeleteBusketAsync(string basketId);
    }
}