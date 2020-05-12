using System;
using System.Windows;

namespace ZermeloCheckers
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(int boardSize)
        {
            InitializeComponent();

            // todo - move to config or to common place
            gameBoard.Initialize(boardSize);
        }

        public event Action NewGameButtonClickEvent;

        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            NewGameButtonClickEvent();
        }
    }
}