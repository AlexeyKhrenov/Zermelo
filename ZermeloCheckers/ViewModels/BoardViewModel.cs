using Game.Implementations;
using Game.Primitives;
using Game.PublicInterfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private ObservableCollection<FigureViewModel> _figures;

        public string ActivePlayer => Game?.Board.ActivePlayer.Name;

        public ICommand UndoMoveCommand { get; set; }

        private IGameFactory GameFactory;

        private IGame Game;

        public BoardViewModel()
        {
             
        }

        public ObservableCollection<FigureViewModel> Figures
        {
            get
            {
                return _figures;
            }
            set
            {
                _figures = value;
                RaisePropertyChanged();
            }
        }

        public void OnFigureMoved(object sender, byte x0, byte y0, byte x1, byte y1)
        {
            Game.Move(new Move(x0, y0, x1, y1));
            UpdateFigures(Game.Board.GetFigures(), Figures);
            RaisePropertyChanged(nameof(ActivePlayer));
        }

        public void OnUndoMoveCommand()
        {
            Game.Undo();
            UpdateFigures(Game.Board.GetFigures(), Figures);
            RaisePropertyChanged(nameof(ActivePlayer));
        }

        public void UpdateFigures(IEnumerable<IFigure> modelFigures, ObservableCollection<FigureViewModel> uiFigures)
        {
            var toBeRemoved = uiFigures.Except(modelFigures).ToList();

            // todo - refactor this
            var toBeUpdated = modelFigures.Join(uiFigures, x => x.GetHashCode(), x => x.GetHashCode(), (x,y) => new KeyValuePair<IFigure, FigureViewModel>(x,y)).ToList();
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

            foreach (var u in toBeUpdated)
            {
                u.Value.AvailableMoves = u.Key.AvailableMoves;
            }

            IsUndoEnabled = Game.HistoryLength != 0;
        }

        public BoardViewModel(IGameFactory gameFactory)
        {
            GameFactory = gameFactory;

            var player1 = new HumanPlayer("Player 1");
            var player2 = new HumanPlayer("Player 2");

            // move size of the game to the config
            Game = GameFactory.CreateGame(6, false, player1, player2);
            _figures = new ObservableCollection<FigureViewModel>(Game.Board.GetFigures().Select(x => x.ToViewModel()).ToList());

            foreach (var figure in _figures)
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
