using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            
                
            //builder.HasNoKey();
            // builder.Navigation(x => x.CustomerBasket).AutoInclude();
            // builder.ToTable("CustomerBasket");
            // builder.Property(bi => bi.ProductName).IsRequired().HasMaxLength(100);
            // builder.Property(bi => bi.Price).IsRequired().HasColumnType("decimal(18,2)");
            // builder.Property(bi => bi.Quantity).IsRequired();
            // builder.Property(bi => bi.PictureUrl).IsRequired();
            
                                    
            
        }

    
    }
}