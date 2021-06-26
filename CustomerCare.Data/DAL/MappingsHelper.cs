using CustomerCare.Data.Maps;
using CustomerCare.Data.Maps.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace CustomerCare.Data.DAL
{
    public static class MappingsHelper
    {
        public static IEnumerable<IMap> GetMappings()
        {
            // get namespaces under Data.Maps directory.
            var assemblyTypes = typeof(CustomerMap).GetTypeInfo().Assembly.DefinedTypes;

            var mappings = assemblyTypes
                .Where(x => x.Namespace != null &&
                    x.Namespace.Contains(typeof(CustomerMap).Namespace))
                .Where(x => typeof(IMap).GetTypeInfo().IsAssignableFrom(x) && 
                    !x.IsAbstract);

            return mappings.Select(m => (IMap)Activator.CreateInstance(m.AsType())).ToArray();
        }
    }
}
