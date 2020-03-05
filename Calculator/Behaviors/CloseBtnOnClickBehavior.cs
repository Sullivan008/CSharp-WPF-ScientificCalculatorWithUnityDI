using System.Windows;
using System.Windows.Controls;

namespace Calculator.Behaviors
{
    public static class CloseBtnOnClickBehaviour
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(CloseBtnOnClickBehaviour),
                new PropertyMetadata(false, OnIsEnabledPropertyChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            object val = obj.GetValue(IsEnabledProperty);

            return (bool)val;
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        #endregion

        #region EVENT HANDLERS

        static void OnIsEnabledPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
        {
            if (!(dpo is Button button))
                return;

            bool oldValue = (bool)args.OldValue;
            bool newValue = (bool)args.NewValue;

            if (!oldValue && newValue)
            {
                button.Click += OnClick;
            }
            else if (oldValue && !newValue)
            {
                button.PreviewMouseLeftButtonDown -= OnClick;
            }
        }

        static void OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button))
                return;

            Window window = Window.GetWindow(button);

            window?.Close();
        }

        #endregion
    }
}
