using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {

        
        private readonly StoreContext _context;

        public BasketRepository(StoreContext context)
        {
            _context = context;
            

        }

        public async Task<bool> DeleteBusketAsync(string basketId)
        {
            var basket = await _context.CustomerBaskets.Include(bi => bi.Items).FirstOrDefaultAsync(b => b.Id == basketId);

            if(basket == null) return false;

            _context.CustomerBaskets.Remove(basket);

            var result = await _context.SaveChangesAsync();

            if(result > 0) return true;

            return false;
        }

        public async Task<CustomerBasket> GetBusketAsync(string basketId)
        {



            var data = await _context.CustomerBaskets.Include(bi => bi.Items).FirstOrDefaultAsync(b => b.Id == basketId);


            if(data == null)
            {
                return null;
            } 
            else
            {
                return data;
            }            

        }

        public async Task<CustomerBasket> UpdateBusketAsync(CustomerBasket basket)
        {


            var existsBasket  = await _context.CustomerBaskets.Include(bi => bi.Items).FirstOrDefaultAsync(b => b.Id == basket.Id);

            if(existsBasket != null) 
            {
                // for (int i = 0; i < existsBasket.Items.Count; i++)
                // {
                    
                //         existsBasket.Items[i].ItemId = basket.Items[i].ItemId;
                //         existsBasket.Items[i].ProductName = basket.Items[i].ProductName;
                //         existsBasket.Items[i].Price = basket.Items[i].Price;
                //         existsBasket.Items[i].Quantity = basket.Items[i].Quantity;
                //         existsBasket.Items[i].PictureUrl = basket.Items[i].PictureUrl;
                //         existsBasket.Items[i].Brand = basket.Items[i].Brand;
                //         existsBasket.Items[i].Type = basket.Items[i].Type;
                    

                // }
                for (int i = 0; i < existsBasket.Items.Count; i++)
                {
                    _context.BasketItems.Remove(existsBasket.Items[i]);
                }

                for (int i = 0; i < basket.Items.Count; i++)
                {
                    existsBasket.Items.Add(basket.Items[i]);                    
                }

                var result = await _context.SaveChangesAsync();
                if(result > 0) return basket;

                return null;
            }
            else{
                await _context.CustomerBaskets.AddAsync(basket);

                var result = await _context.SaveChangesAsync();

                if(result > 0) return basket;

                return null;
            }
            
            
            
            


        }



        
    }
}