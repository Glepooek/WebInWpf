﻿using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebInWpf.Cefsharp.NET452.Behaviours
{
    public class TextBoxBindingUpdateOnEnterBehaviour : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += OnTextBoxKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= OnTextBoxKeyDown;
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var txtBox = sender as TextBox;
                txtBox?.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }
    }
}
