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
 
namespace ZermeloCheckers
{
    class BoardViewModel : INotifyPropertyChanged
    {
        // TODO - remove hard-coded value
        public int Size = 8;

        private ObservableCollection<FigureViewModel> _figures;

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

        public void OnFigureMoved(object sender, int x0, int y0, int x1, int y1)
        {
            Game.Move(x0, y0, x1, y1);

            var toBeRemoved = new List<FigureViewModel>();
            var toBeInserted = new List<FigureViewModel>();

            foreach (var uiFigure in Figures)
            {
                var source = Game.Figures.FirstOrDefault(f => f.X == uiFigure.X && f.Y == uiFigure.Y && f.Type == uiFigure.Type);
                if (source == null)
                {
                    toBeRemoved.Add(uiFigure);
                }
                else
                {
                    uiFigure.AllowedMoves = source.AvailableMoves;
                }
            }

            foreach (var r in toBeRemoved){
                Figures.Remove(r);
            }

            foreach (var figure in Game.Figures)
            {
                var source = Figures.FirstOrDefault(f => f.X == figure.X && f.Y == figure.Y && f.Type == figure.Type);
                if (source == null)
                {
                    var newUiFigure = figure.ToViewModel();
                    newUiFigure.FigureMoved += OnFigureMoved;
                    toBeInserted.Add(newUiFigure);
                }
            }

            foreach (var i in toBeInserted)
            {
                Figures.Add(i);
            }
        }

        public BoardViewModel(IGameFactory gameFactory)
        {
            GameFactory = gameFactory;

            // move size of the game to the config
            Game = GameFactory.CreateGame(6, false);
            _figures = new ObservableCollection<FigureViewModel>(Game.Figures.Select(x => x.ToViewModel()).ToList());

            foreach (var figure in _figures)
            {
                figure.FigureMoved += OnFigureMoved;
            }
        }

        // WARNING - business logic
        

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void RaisePropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
