using System.Windows;
using System.Windows.Controls;

namespace Calculator.Behaviors
{
    public static class CloseMenuItemOnClickBehavior
    {
        #region DEPENDENCY PROPERTIES

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(CloseMenuItemOnClickBehavior),
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
            if (!(dpo is MenuItem menuItem))
                return;

            bool oldValue = (bool)args.OldValue;
            bool newValue = (bool)args.NewValue;

            if (!oldValue && newValue)
            {
                menuItem.Click += OnClick;
            }
            else if (oldValue && !newValue)
            {
                menuItem.PreviewMouseLeftButtonDown -= OnClick;
            }
        }

        static void OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is MenuItem menuItem))
                return;

            Window window = Window.GetWindow(menuItem);

            window?.Close();
        }

        #endregion
    }
}