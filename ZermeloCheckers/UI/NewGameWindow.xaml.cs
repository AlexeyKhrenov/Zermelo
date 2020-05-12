using System.Windows;

namespace ZermeloCheckers
{
    /// <summary>
    ///     Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        public NewGameWindow()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}