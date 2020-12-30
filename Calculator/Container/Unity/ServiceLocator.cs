using Calculator.Modules.Calculator.Handlers;
using Calculator.Modules.Calculator.Interfaces;
using Calculator.ViewModels.Views;
using Calculator.ViewModels.Views.Interfaces;
using Calculator.ViewModels.Windows;
using Unity;

namespace Calculator.Container.Unity
{
    public class ServiceLocator
    {
        private readonly UnityContainer _container;

        public ServiceLocator()
        {
            _container = GetContainerWithRegistrations() as UnityContainer;
        }

        private static IUnityContainer GetContainerWithRegistrations()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IBasicCalculatorHandler, BasicCalculatorHandler>()
                     .RegisterType<IBasicArithmeticOperatorHandler, BasicArithmeticOperatorHandler>()
                     .RegisterType<IRightParenthesisHandler, RightParenthesisHandler>()
                     .RegisterType<ICalculateHandler, CalculateHandler>();

            container.RegisterType<ICalculatorViewModel, CalculatorViewModel>();

            return container;
        }

        public MainWindowViewModel MainWindowViewViewModel =>
            _container.Resolve<MainWindowViewModel>();

    }
}
