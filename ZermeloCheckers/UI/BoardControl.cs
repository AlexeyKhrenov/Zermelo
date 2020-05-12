using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZermeloCheckers.Misc;
using ZermeloCheckers.ViewModels;

namespace ZermeloCheckers
{
    public class BoardControl : Grid
    {
        public static DependencyProperty FiguresProperty;

        public static DependencyProperty IsBlockedProperty;

        private FigureViewModel selectedFigure;
        private BoardSquare[,] squares;

        static BoardControl()
        {
            FiguresProperty = DependencyProperty.Register(
                "Figures", typeof(ObservableCollection<FigureViewModel>), typeof(BoardControl),
                new FrameworkPropertyMetadata(WholeSetOfFiguresChanged)
            );

            IsBlockedProperty = DependencyProperty.Register(
                "IsBlocked", typeof(bool), typeof(BoardControl),
                new FrameworkPropertyMetadata(IsBlockedPropertyChanged)
            );
        }

        public ObservableCollection<FigureViewModel> Figures
        {
            get => (ObservableCollection<FigureViewModel>) GetValue(FiguresProperty);
            set => SetValue(FiguresProperty, value);
        }

        public bool IsBlocked
        {
            get => (bool) GetValue(IsBlockedProperty);
            set => SetValue(IsBlockedProperty, value);
        }

        private static void WholeSetOfFiguresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var board = (BoardControl) d;
            var newValue = (ObservableCollection<FigureViewModel>) e.NewValue;

            newValue.CollectionChanged += (sender, e) =>
                board.Dispatcher.Invoke(() => board.Figures_CollectionChanged(sender, e));

            foreach (var item in newValue)
            {
                var figure = item.ToUIElement();

                //remove this check or inverse control flow
                board.squares?[item.X, item.Y].AddFigure(figure);
            }
        }

        private static void IsBlockedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var board = (BoardControl) d;

            var newValue = (bool) e.NewValue;

            if (newValue)
                if (board.selectedFigure != null)
                    board.DeselectFigure(board.selectedFigure);
        }

        private void Figures_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var oldItems = e.OldItems;
            var newItems = e.NewItems;

            if (oldItems != null)
                foreach (var item in oldItems)
                    RemoveFigure(item as FigureViewModel);

            if (newItems != null)
                foreach (var item in newItems)
                    AddFigure(item as FigureViewModel);

            if (selectedFigure != null) DeselectFigure(selectedFigure);
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
            squares = new BoardSquare[gameSize, gameSize];

            var squareSize = Math.Min(Width, Height) / gameSize;
            var gridLength = new GridLength(squareSize);

            for (var i = 0; i < gameSize; i++) ColumnDefinitions.Add(new ColumnDefinition {Width = gridLength});

            for (var i = 0; i < gameSize; i++) RowDefinitions.Add(new RowDefinition {Height = gridLength});

            for (byte y = 0; y < gameSize; y++)
            for (byte x = 0; x < gameSize; x++)
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

        private bool TryMoveFigure(FigureViewModel figure, byte targetX, byte targetY)
        {
            return figure.TryMoveFigure(targetX, targetY);
        }

        private void SelectFigure(BoardSquare square)
        {
            if (Figures == null) return;

            var figure = Figures.FirstOrDefault(x => x.HasCoordinates(square.X, square.Y));

            if (figure != null)
            {
                var selectedSquare = squares[figure.X, figure.Y];
                selectedSquare.Select();

                if (figure.AvailableMoves != null)
                    foreach (var allowedMove in figure.AvailableMoves)
                        squares[allowedMove.X, allowedMove.Y].SelectAllowedMove();

                selectedFigure = figure;
            }
        }

        private void DeselectFigure(FigureViewModel figure)
        {
            var selectedSquare = squares[figure.X, figure.Y];
            selectedSquare.Deselect();

            if (figure.AvailableMoves != null)
                foreach (var allowedMove in figure.AvailableMoves)
                    squares[allowedMove.X, allowedMove.Y].DeselectAllowedMove();

            selectedFigure = null;
        }

        private void OnSquareMouseClick(object sender, RoutedEventArgs e)
        {
            if (IsBlocked) return;
            var square = (BoardSquare) sender;
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