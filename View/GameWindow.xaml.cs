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
using System.Windows.Shapes;
using WpfApp1.Controller;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        private void MakeAMoveButton_Click(object sender, RoutedEventArgs e)
        {
            // подписаться на событие
            controller.MakeAMove();
            
            bool AddBoost = true;

            if (AddBoost)
        }

        private GameWindowController controller;
    }
}
