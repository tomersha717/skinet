using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CustomerBasketDto
    {
        public CustomerBasketDto()
        {
        }

        public CustomerBasketDto(string id)
        {
            Id = id;            
        }
        public string Id { get; set; }        
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}