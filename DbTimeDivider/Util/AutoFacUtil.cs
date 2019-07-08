using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDepots.Util
{
    public static class AutoFacUtil
    {
        public static TService GetService<TService>(this IContainer iContainer)
        {
            return iContainer.Resolve<TService>();
        }

        public static IEnumerable<TService> GetServices<TService>(this IContainer iContainer)
        {
            var types = iContainer.ComponentRegistry.Registrations.Where(r => typeof(TService).IsAssignableFrom(r.Activator.LimitType)).Select(r => r.Activator.LimitType);
            return types.Select(t => (TService)iContainer.Resolve(t));
        }

    }
}
