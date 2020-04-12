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

        public void OnFigureMoved(object sender, FigureViewModel obj)
        {
            // TODO - consider implementing another pattern here
            if (sender as BoardViewModel == null)
            {
                foreach (var figure in Figures)
                {
                    EvaluateAllowedMoves(figure);
                }
            }
        }

        public BoardViewModel(IGameFactory gameFactory)
        {
            GameFactory = gameFactory;

            // move size of the game to the config
            Game = GameFactory.CreateGame(8, false);
            _figures = new ObservableCollection<FigureViewModel>(Game.Figures.Select(x => x.ToViewModel()).ToList());

            foreach (var figure in _figures)
            {
                figure.FigureMoved += OnFigureMoved;
                EvaluateAllowedMoves(figure);
            }
        }

        // WARNING - business logic
        public void EvaluateAllowedMoves(FigureViewModel figure)
        {
            var allowedMoves = new List<Point>();
            allowedMoves.Add(new Point(figure.X + 1, figure.Y + 1));
            allowedMoves.Add(new Point(figure.X - 1, figure.Y - 1));
            allowedMoves.Add(new Point(figure.X + 1, figure.Y - 1));
            allowedMoves.Add(new Point(figure.X - 1, figure.Y + 1));

            allowedMoves.Add(new Point(figure.X + 1, figure.Y));
            allowedMoves.Add(new Point(figure.X - 1, figure.Y));
            allowedMoves.Add(new Point(figure.X, figure.Y + 1));
            allowedMoves.Add(new Point(figure.X, figure.Y - 1));

            allowedMoves.RemoveAll(x => x.X < 0 || x.Y < 0 || x.X >= Size || x.Y >= Size);

            foreach (var neigh in Figures)
            {
                allowedMoves.RemoveAll(x => x.X == neigh.X && x.Y == neigh.Y);
            }

            figure.AllowedMoves = allowedMoves.ToArray();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void RaisePropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
