using Autofac;
using DbTimeDivider.IFace;
using DbTimeDivider.Schema;
using System.Reflection;
using IContainer = Autofac.IContainer;

namespace DataDepots.Config
{
    public class AutofacConfig
    {
        public static IContainer Register(string assemblyString)
        {
            //实例化一个autofac的创建容器
            var builder = new ContainerBuilder();
            Assembly ass = Assembly.Load(assemblyString);

            foreach (var type in ass.GetTypes())
            {
                if (
                    typeof(AbsDbHostSchema).IsAssignableFrom(type)
                    || (type.BaseType != null && typeof(IDbSchema).IsAssignableFrom(type.BaseType))
                    || (type.BaseType != null && typeof(ITableSchema).IsAssignableFrom(type.BaseType))
                 )
                {
                    builder.RegisterType(type).SingleInstance();
                }
                else if (typeof(AbsDBProvider).IsAssignableFrom(type))
                {
                    builder.RegisterType(type);
                }
            }

            //创建一个Autofac的容器
            return builder.Build();
        }
    }
}
