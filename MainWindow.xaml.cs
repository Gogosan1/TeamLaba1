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
using WpfApp1.Controller;
using WpfApp1;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            presenter = new GameWindowPresenter();
            InitializeComponent();
        }

        private void LetsGoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(PlayerNickTextBox.Text))
            {
                if (presenter.IsPlayerByNickExist(PlayerNickTextBox.Text))
                    MessageBox.Show("С возвращением");
                else
                    MessageBox.Show("Добро пожаловать! Рады новым пользователям.");
                var form = new MenuWindow(presenter);
                form.ShowDialog();
            }
            else
                MessageBox.Show("Значение имени не может быть пустым.");
        }
        private GameWindowPresenter presenter;
    }
}
