using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

namespace RichardAI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void WindowDragMove(object? sender, PointerPressedEventArgs e)
        {
            if (sender is Control control && control.GetVisualRoot() is Window window)
            {
                window.BeginMoveDrag(e);
            }
        }

        private void SubmitButton_Click(object? sender, RoutedEventArgs e)
        {
            // Обработка нажатия кнопки
            if (!string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                Console.WriteLine("Input: " + InputTextBox.Text);
            }
        }
    }
}