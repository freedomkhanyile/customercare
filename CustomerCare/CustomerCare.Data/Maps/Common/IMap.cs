using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCare.Data.Maps.Common
{
    public interface IMap
    {
        void Visit(ModelBuilder builder);
    }
}
