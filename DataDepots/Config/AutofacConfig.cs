using Autofac;
using DataDepots.Define;
using DataDepots.IFace;
using System.Reflection;
using IContainer = Autofac.IContainer;

namespace DataDepots
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
                    typeof(AbsServerDefine).IsAssignableFrom(type)
                    || (type.BaseType != null && typeof(IDataBaseDefine).IsAssignableFrom(type.BaseType))
                    || (type.BaseType != null && typeof(ITableDefine).IsAssignableFrom(type.BaseType))
                    || typeof(IDBProvider).IsAssignableFrom(type)
                 )
                {
                    builder.RegisterType(type).SingleInstance();
                }
            }

            //创建一个Autofac的容器
            return builder.Build();
        }
    }
}
