using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Documents;
 
namespace ZermeloCheckers
{
    class BoardViewModel : INotifyPropertyChanged
    {
        // TODO - remove hard-coded value
        public int Size = 8;

        ObservableCollection<FigureViewModel> _figures;

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

        public BoardViewModel()
        {
            _figures = new ObservableCollection<FigureViewModel>()
            {
                new FigureViewModel()
                {
                    Y = 0,
                    X = 0,
                    Type = FigureType.Pawn
                },
                new FigureViewModel()
                {
                    Y = 1,
                    X = 1,
                    Type = FigureType.Pawn
                },
            };

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
