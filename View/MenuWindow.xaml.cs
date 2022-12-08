using Model.InternalLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new GameWindow();
            form.ShowDialog();
        }

        private void RulesLoadButton_Click(object sender, RoutedEventArgs e)
        {
            // из файла
            MessageBox.Show("ТУТ ТЕКСТ ПРАВИЛ НАШЕЙ ИГРЫ");
        }

        private void RatingLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new RatingWindow();
            form.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
           Environment.Exit(0);
        }
        private Game game;
    }
}
