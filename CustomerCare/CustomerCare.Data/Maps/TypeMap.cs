using CustomerCare.Data.Maps.Common;
using Microsoft.EntityFrameworkCore;

namespace CustomerCare.Data.Maps
{
    public class TypeMap: IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Models.Type>()
                .ToTable("Types")
                .HasKey(x => x.Id);
        }
    }
}
