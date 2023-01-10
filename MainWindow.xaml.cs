using System;
using System.Windows;
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
            InitializeComponent();
        }

        private void LetsGoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NickTextBox.Text))
            {
                presenter = new GameWindowPresenter(NickTextBox.Text);
                
                    MessageBox.Show("Добро пожаловать!");

                var form = new MenuWindow(presenter);
                this.Close();
                form.ShowDialog();
            }
            else
                MessageBox.Show("Значение имени не может быть пустым.");
        }
        private GameWindowPresenter presenter;

    }
}
