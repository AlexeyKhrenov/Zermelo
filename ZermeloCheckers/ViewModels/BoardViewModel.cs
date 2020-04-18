using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;

namespace ZermeloCheckers.ViewModels
{
    class BoardViewModel : INotifyPropertyChanged
    {
        // TODO - remove hard-coded value
        public int Size = 8;

        private bool _isUndoEnabled = false;
        public bool IsUndoEnabled
        {
            get { return _isUndoEnabled; }
            set
            {
                _isUndoEnabled = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<FigureViewModel> _player1Figures;
        private ObservableCollection<FigureViewModel> _player2Figures;

        public string ActivePlayer => Game?.ActivePlayer.Name;

        public ICommand UndoMoveCommand { get; set; }

        private IGameFactory GameFactory;

        private IGame Game;

        public BoardViewModel()
        {
             
        }

        public ObservableCollection<FigureViewModel> Player1Figures
        {
            get
            {
                return _player1Figures;
            }
            set
            {
                _player1Figures = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<FigureViewModel> Player2Figures
        {
            get
            {
                return _player2Figures;
            }
            set
            {
                _player2Figures = value;
                RaisePropertyChanged();
            }
        }

        public void OnFigureMoved(object sender, int x0, int y0, int x1, int y1)
        {
            Game.Move(x0, y0, x1, y1);
            UpdateFigures(Game.Player1.Figures, Player1Figures);
            UpdateFigures(Game.Player2.Figures, Player2Figures);
            RaisePropertyChanged(nameof(ActivePlayer));
        }

        public void OnUndoMoveCommand()
        {
            Game.Undo();
            UpdateFigures(Game.Player1.Figures, Player1Figures);
            UpdateFigures(Game.Player2.Figures, Player2Figures);
            RaisePropertyChanged(nameof(ActivePlayer));
        }

        public void UpdateFigures(IList<IFigure> modelFigures, ObservableCollection<FigureViewModel> uiFigures)
        {
            var toBeRemoved = uiFigures.Except(modelFigures).ToList();
            var toBeInserted = modelFigures.Except(uiFigures).ToList();

            foreach (var r in toBeRemoved)
            {
                uiFigures.Remove(r as FigureViewModel);
            }

            foreach (var i in toBeInserted)
            {
                var newUiFigure = i.ToViewModel();
                newUiFigure.FigureMoved += OnFigureMoved;
                uiFigures.Add(newUiFigure);
            }

            IsUndoEnabled = Game.HistoryLength != 0;
        }

        public BoardViewModel(IGameFactory gameFactory)
        {
            GameFactory = gameFactory;

            // move size of the game to the config
            Game = GameFactory.CreateGame(6, false);
            _player1Figures = new ObservableCollection<FigureViewModel>(Game.Player1.Figures.Select(x => x.ToViewModel()).ToList());
            _player2Figures = new ObservableCollection<FigureViewModel>(Game.Player2.Figures.Select(x => x.ToViewModel()).ToList());

            foreach (var figure in _player1Figures)
            {
                figure.FigureMoved += OnFigureMoved;
            }

            foreach (var figure in _player2Figures)
            {
                figure.FigureMoved += OnFigureMoved;
            }

            UndoMoveCommand = new RelayCommand(obj => OnUndoMoveCommand());
            IsUndoEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void RaisePropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
