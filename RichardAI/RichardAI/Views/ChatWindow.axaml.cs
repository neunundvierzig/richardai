using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace RichardAI
{
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => InputTextBox.Focus();
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitButton_Click(null, null);
                e.Handled = true;
            }
        }

        private void SubmitButton_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                string userInput = InputTextBox.Text;
                Console.WriteLine("Input: " + userInput);
            }
        }
    }
}