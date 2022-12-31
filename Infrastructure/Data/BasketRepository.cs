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

            var data = new CustomerBasket();

            data = await _context.CustomerBaskets.Include(bi => bi.Items).FirstOrDefaultAsync(b => b.Id == basketId);


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

            var existsBasket = new CustomerBasket();
            existsBasket  = await _context.CustomerBaskets.Include(bi => bi.Items).FirstOrDefaultAsync(b => b.Id == basket.Id);
            
            var existsBasketItem = new BasketItem();
            
            



            if(existsBasket != null) 
            {
                
                    
                existsBasket.ClientSecret = basket.ClientSecret;
                existsBasket.PaymentIntentId = basket.PaymentIntentId;
                existsBasket.DeliveryMethodId = basket.DeliveryMethodId;
                existsBasket.ShippingPrice = basket.ShippingPrice;

                // Update or remove items from Basket
                for (int i = 0; i < existsBasket.Items.Count; i++)
                {
                    BasketItem clientItemBasket = basket.Items.Find(item => item.Id == existsBasket.Items[i].Id);
                    
                    if (clientItemBasket != null)
                    {
                        existsBasket.Items[i].Quantity = clientItemBasket.Quantity;
                    }
                    else
                    {
                        existsBasket.Items.Remove(existsBasket.Items[i]);
                    }
                }

                // Add items to Basket
                for (int i = 0; i < basket.Items.Count; i++)
                {
                    existsBasketItem = await _context.BasketItems.FirstOrDefaultAsync(item => item.Id == basket.Items[i].Id);
                    if (existsBasketItem == null)
                    {
                        existsBasket.Items.Add(basket.Items[i]);
                    }

                }




                /*
                foreach (var item in basket.Items)
                {
                    existsBasketItem = await _context.BasketItems.FirstOrDefaultAsync(i => i.Id == );
                    if (existsBasketItem != null)
                    {

                        existsBasketItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        existsBasket.Items.Add(item);
                    }
                }
                
                var result = await _context.SaveChangesAsync();

                // CustomerBasket existsBasketCheckToRemove = new CustomerBasket();
                // existsBasketCheckToRemove = existsBasket;

                existsBasket  = await _context.CustomerBaskets.Include(bi => bi.Items).FirstOrDefaultAsync(b => b.Id == basket.Id);
                foreach (var item in existsBasket.Items)
                {

                    //existsBasketItem = await _context.BasketItems.FirstOrDefaultAsync(i => i.Id == item.Id);
                    BasketItem clientItemBasket = basket.Items.Find(i => i.Id == item.Id);

                    if (clientItemBasket == null)
                    {                        
                        existsBasket.Items.Remove(item);
                    }
                    
                }

                /*
                for (int i = 0; i < existsBasket.Items.Count; i++)
                {
                    _context.BasketItems.Remove(existsBasket.Items[i]);
                }

                int totalItems = basket.Items.Count;
                for (int i = 0; i < totalItems; i++)
                {
                    existsBasket.Items.Add(basket.Items[i]);
                }
                */
                var result = await _context.SaveChangesAsync();
                
                if(result > 0) return existsBasket;

                return null;
            }
            else
            {
                await _context.CustomerBaskets.AddAsync(basket);

                var result = await _context.SaveChangesAsync();

                if(result > 0) return basket;

                return null;
            }
            
            
            
            


        }



        
    }
}