using Unity;
using Unity.Lifetime;

namespace Calculator.Container.Unity
{
    public static class DependencyInjector
    {
        private static readonly UnityContainer UnityContainer = new UnityContainer();

        public static void RegisterSingleton<TInterface, TClass>() where TClass : TInterface
        {
            UnityContainer.RegisterType<TInterface, TClass>(new ContainerControlledLifetimeManager());
        }

        public static void RegisterTransient<TInterface, TClass>() where TClass : TInterface
        {
            UnityContainer.RegisterType<TInterface, TClass>(new ContainerControlledTransientManager());
        }

        public static TClass Retrieve<TClass>()
        {
            return UnityContainer.Resolve<TClass>();
        }
    }
}
