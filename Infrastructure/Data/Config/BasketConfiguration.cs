using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            
            // builder.ToTable("BasketItem");
            // builder.
            // builder.Property(b => b.Id).IsRequired();                        
            // builder.HasOne(b => b.Id).WithMany(b => b[]);    
            // builder.HasKey<BasketItem>(x => x.Id);       
            
        }

    
    }
}