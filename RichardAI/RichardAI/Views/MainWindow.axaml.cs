using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace RichardAI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PointerEntered += MainWindow_PointerEntered;
            PointerExited += MainWindow_PointerExited;
            PointerPressed += MainWindow_PointerPressed;

            Deactivated += (s, e) => Close();
        }

        private void MainWindow_PointerEntered(object sender, PointerEventArgs e)
        {
            this.Opacity = 1;
        }

        private void MainWindow_PointerExited(object sender, PointerEventArgs e)
        {
            this.Opacity = 0.7;
        }

        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            new ChatWindow().Show();
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            this.PointerEntered -= MainWindow_PointerEntered;
            this.PointerExited -= MainWindow_PointerExited;
            this.PointerPressed -= MainWindow_PointerPressed;
            base.OnClosed(e);
        }
    }
}