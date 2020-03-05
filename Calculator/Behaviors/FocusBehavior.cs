using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Calculator.Behaviors
{
    public static class FocusBehavior
    {
        #region DEPENDENCY PROPERTIES
        
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool?), typeof(FocusBehavior),
                new FrameworkPropertyMetadata(IsFocusedChanged));

        public static bool? GetIsFocused(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($@"Property \\element\\ is not exists this method: {nameof(GetIsFocused)} in {nameof(FocusBehavior)} Class!");
            }

            return (bool?)element.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject element, bool? value)
        {
            if (element == null)
            {
                throw new ArgumentNullException($@"Property \\element\\ is not exists this method: {nameof(GetIsFocused)} in {nameof(FocusBehavior)} Class!");
            }

            element.SetValue(IsFocusedProperty, value);
        }

        #endregion 

        #region EVENT HANDLERS

        private static void IsFocusedChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
        {
            if (dpo is FrameworkElement frameworkElement && args.OldValue == null && args.NewValue != null && (bool)args.NewValue)
            {
                frameworkElement.Loaded += FrameworkElementLoaded;
            }
        }

        private static void FrameworkElementLoaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is FrameworkElement frameworkElement))
                return;

            frameworkElement.Loaded -= FrameworkElementLoaded;

            frameworkElement.Focus();

            if (frameworkElement is TextBoxBase textBoxBase)
            {
                textBoxBase.SelectAll();
            }
        }

        #endregion
    }
}
