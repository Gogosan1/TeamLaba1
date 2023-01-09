using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using WpfApp1.Controller;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow(GameWindowPresenter presenter)
        {
            this.presenter = presenter;
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new GameWindow(presenter);
            form.ShowDialog();
        }

        private void RulesLoadButton_Click(object sender, RoutedEventArgs e)
        {
           var sr = new StreamReader("Files/Rules.txt", Encoding.Default);
           
           MessageBox.Show(sr.ReadToEnd());
            
            
        }

        private void RatingLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new RatingWindow(presenter);
            form.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
           Environment.Exit(0);
        }
        private GameWindowPresenter presenter;
    }
}
