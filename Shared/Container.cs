using Autofac;
using Autofac.Core;

using JetBrains.Annotations;

namespace BitcoinApi.Shared
{
    public static class Container
    {
        public static IContainer Current { get; private set; }

        public static void Init([NotNull] ContainerBuilder builder, [NotNull] params IModule[] modules)
        {
            foreach(var module in modules)
            {
                builder.RegisterModule(module);
            }

            Current = builder.Build();
        }
    }
}