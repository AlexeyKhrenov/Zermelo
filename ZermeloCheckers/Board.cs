using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZermeloCheckers
{
    public class Board : Grid
    {
        public static DependencyProperty Player1FiguresProperty;
        public static DependencyProperty Player2FiguresProperty;

        private FigureViewModel selectedFigure;
        private BoardSquare[,] squares;

        public ObservableCollection<FigureViewModel> Player1Figures
        {
            get { return (ObservableCollection<FigureViewModel>) GetValue(Player1FiguresProperty); }
            set {
                SetValue(Player1FiguresProperty, value); 
            }
        }

        public ObservableCollection<FigureViewModel> Player2Figures
        {
            get { return (ObservableCollection<FigureViewModel>)GetValue(Player2FiguresProperty); }
            set
            {
                SetValue(Player2FiguresProperty, value);
            }
        }

        static Board()
        {
            Player1FiguresProperty = DependencyProperty.Register(
                "Player1Figures", typeof(ObservableCollection<FigureViewModel>), typeof(Board),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(WholeSetOfFiguresChanged))
            );

            Player2FiguresProperty = DependencyProperty.Register(
                "Player2Figures", typeof(ObservableCollection<FigureViewModel>), typeof(Board),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(WholeSetOfFiguresChanged))
            );
        }

        private static void WholeSetOfFiguresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var board = (Board)d;
            var newValue = (ObservableCollection<FigureViewModel>)e.NewValue;

            newValue.CollectionChanged += board.Figures_CollectionChanged;

            foreach (var item in newValue)
            {
                var figure = item.ToUIElement();

                //remove this check or inverse control flow
                board.squares?[item.X, item.Y].AddFigure(figure);
            }
        }

        public Board()
        {
        }

        private void Figures_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var oldItems = e.OldItems;
            var newItems = e.NewItems;

            if (oldItems != null)
            {
                foreach (var item in oldItems)
                {
                    RemoveFigure(item as FigureViewModel);
                }
            }

            if (newItems != null)
            {
                foreach (var item in newItems)
                {
                    AddFigure(item as FigureViewModel);
                }
            }
        }

        private void RemoveFigure(FigureViewModel figure)
        {
            var square = squares[figure.X, figure.Y];
            square.RemoveFigure();
        }

        private void AddFigure(FigureViewModel figure)
        {
            var square = squares[figure.X, figure.Y];
            square.AddFigure(figure.ToUIElement());
        }

        // todo - consider changing to the game model
        public void Initialize(int gameSize)
        {
            squares = new BoardSquare[gameSize,gameSize];

            var squareSize = Math.Min(Width, Height) / gameSize;
            var gridLength = new GridLength(squareSize);

            for (var i = 0; i < gameSize; i++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = gridLength });
            }

            for (var i = 0; i < gameSize; i++)
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = gridLength });
            }

            for (var y = 0; y < gameSize; y++)
            {
                for (var x = 0; x < gameSize; x++)
                {
                    var square = new BoardSquare();
                    square.X = x;
                    square.Y = y;
                    square.IsBlack = (y + x) % 2 != 0;
                    square.OnMouseClick += OnSquareMouseClick;

                    SetColumn(square, x);
                    SetRow(square, y);
                    Children.Add(square);

                    squares[x, y] = square;
                }
            }
        }

        private bool TryMoveFigure(FigureViewModel figure, int targetX, int targetY)
        {
            return figure.TryMoveFigure(targetX, targetY);
        }

        private void SelectFigure(BoardSquare square)
        {
            var figure = Player1Figures.Union(Player2Figures).FirstOrDefault(x => x.HasCoordinates(square.X, square.Y));

            if (figure != null)
            {
                var selectedSquare = squares[figure.X, figure.Y];
                selectedSquare.Select();

                if (figure.AllowedMoves != null)
                {
                    foreach (var allowedMove in figure.AllowedMoves)
                    {
                        squares[allowedMove.X, allowedMove.Y].SelectAllowedMove();
                    }
                }

                selectedFigure = figure;
            }
        }

        private void DeselectFigure(FigureViewModel figure)
        {
            var selectedSquare = squares[figure.X, figure.Y];
            selectedSquare.Deselect();

            if (figure.AllowedMoves != null)
            {
                foreach (var allowedMove in figure.AllowedMoves)
                {
                    squares[allowedMove.X, allowedMove.Y].DeselectAllowedMove();
                }
            }

            selectedFigure = null;
        }

        private void OnSquareMouseClick(object sender, RoutedEventArgs e)
        {
            var square = (BoardSquare)sender;
            if (selectedFigure != null)
            {
                var figure = selectedFigure;
                DeselectFigure(figure);
                TryMoveFigure(figure, square.X, square.Y);
            }
            else
            {
                SelectFigure(square);
            }
        }
    }
}
