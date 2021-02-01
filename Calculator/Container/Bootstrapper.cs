using Calculator.Container.Unity;
using Calculator.Core.Services;
using Calculator.Core.Services.Interfaces;
using Calculator.Views.Calculator.Services;
using Calculator.Views.Calculator.Services.Interfaces;
using Calculator.Views.Calculator.ViewModels;
using Calculator.Views.Calculator.ViewModels.Interfaces;
using Calculator.Windows.Main.ViewModels;
using Calculator.Windows.Main.ViewModels.Interfaces;

namespace Calculator.Container
{
    public static class Bootstrapper
    {
        public static void Init()
        {
            DependencyInjector.RegisterSingleton<IEnvironmentService, EnvironmentService>();

            DependencyInjector.RegisterTransient<ICalculatorStorageService, CalculatorStorageService>();
            DependencyInjector.RegisterTransient<ICalculateService, CalculateService>();

            DependencyInjector.RegisterTransient<IMainWindowViewModel, MainWindowViewModel>();
            DependencyInjector.RegisterTransient<ICalculatorViewModel, CalculatorViewModel>();
        }
    }
}
