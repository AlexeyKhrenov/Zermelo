using Checkers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ZermeloCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void AppStartup(object sender, StartupEventArgs e)
        {
            var factory = new CheckersFactory();
            var viewModel = new BoardViewModel(factory);

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
