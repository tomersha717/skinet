using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class BasketItem
    {  
        //[Key]      
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        //public CustomerBasket CustomerBasket { get; set; }
        
        //[Required]
        //public string CustomerBasketId { get; set; }
        //public CustomerBasket CustomerBasket { get; set; }
        
    }
}