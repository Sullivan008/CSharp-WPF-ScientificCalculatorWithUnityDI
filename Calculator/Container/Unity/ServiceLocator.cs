using Calculator.Modules.Calculator.Handlers;
using Calculator.Modules.Calculator.Interfaces;
using Calculator.ViewModels.MainWindow;
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

        private static IUnityContainer GetContainerWithRegistrations() => 
            new UnityContainer()
                .RegisterType<IBasicCalculatorHandler, BasicCalculatorHandler>()
                .RegisterType<IBasicArithmeticOperatorHandler, BasicArithmeticOperatorHandler>()
                .RegisterType<IRightParenthesisHandler, RightParenthesisHandler>()
                .RegisterType<ICalculateHandler, CalculateHandler>();

        public MainWindowViewModel MainWindowViewViewModel =>
            _container.Resolve<MainWindowViewModel>();

    }
}
