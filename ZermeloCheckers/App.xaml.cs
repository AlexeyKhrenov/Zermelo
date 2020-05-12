using System.Windows;
using ZermeloCheckers.GameFactory;
using ZermeloCheckers.Models;
using ZermeloCheckers.ViewModels;

namespace ZermeloCheckers
{
    /// <summary>
    ///     All viewmodels are assembled here
    /// </summary>
    public partial class App : Application
    {
        private int _defaultTimeToThink;
        private CheckersFactory _factory;
        private byte _gameSize;
        private MainViewModel _mainViewModel;

        public void AppStartup(object sender, StartupEventArgs e)
        {
            _gameSize = byte.Parse(FindResource("GameSize").ToString());
            _defaultTimeToThink = int.Parse(FindResource("DefaultTimeToThink").ToString());
            _factory = new CheckersFactory();
            _mainViewModel = new MainViewModel();

            var mainWindow = new MainWindow(_gameSize);
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

            var newGameWindow = new NewGameWindow();
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