using Checkers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZermeloCheckers.GameFactory;
using ZermeloCheckers.Models;
using ZermeloCheckers.ViewModels;

namespace ZermeloCheckers
{
    /// <summary>
    /// All viewmodels are assembled here
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _mainViewModel;
        private CheckersFactory _factory;
        private byte _gameSize;
        private int _defaultTimeToThink;

        public void AppStartup(object sender, StartupEventArgs e)
        {
            _gameSize = byte.Parse(FindResource("GameSize").ToString());
            _defaultTimeToThink = int.Parse(FindResource("DefaultTimeToThink").ToString());
            _factory = new CheckersFactory();
            _mainViewModel = new MainViewModel();

            MainWindow mainWindow = new MainWindow(_gameSize);
            mainWindow.DataContext = _mainViewModel;

            // it doesn't work from XAML
            mainWindow.ComputerPlayerControl1.DataContext = _mainViewModel.Player1;
            mainWindow.ComputerPlayerControl2.DataContext = _mainViewModel.Player2;

            mainWindow.NewGameButtonClickEvent += ShowNewGameDialog;
            mainWindow.Show();
        }

        public void ShowNewGameDialog()
        {
            var gameRequest = new GameRequest(_gameSize);
            var newGameViewModel = new NewGameViewModel();
            newGameViewModel.FromModel(gameRequest);

            NewGameWindow newGameWindow = new NewGameWindow();
            newGameWindow.DataContext = newGameViewModel;
            if (newGameWindow.ShowDialog() == true)
            {
                var game = _factory.CreateGame(gameRequest);
                var gameModel = new GameModel(game, _defaultTimeToThink);
                _mainViewModel.FromModel(gameModel);
            }
        }
    }
}
