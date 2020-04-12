using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZermeloCheckers"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZermeloCheckers;assembly=ZermeloCheckers"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Board/>
    ///
    /// </summary>
    public class Board : Grid
    {
        public static DependencyProperty FiguresProperty;

        private FigureViewModel selectedFigure;
        private BoardSquare[,] squares;

        public ObservableCollection<FigureViewModel> Figures
        {
            get { return (ObservableCollection<FigureViewModel>) GetValue(FiguresProperty); }
            set {
                SetValue(FiguresProperty, value); 
            }
        }

        static Board()
        {
            FiguresProperty = DependencyProperty.Register(
                "Figures", typeof(ObservableCollection<FigureViewModel>), typeof(Board),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnFiguresChanged))
            );
        }

        private static void OnFiguresChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var board = (Board)d;
            var newValue = (ObservableCollection<FigureViewModel>)e.NewValue;
            var oldValue = (ObservableCollection<FigureViewModel>)e.OldValue;

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
            var selectedSquare = squares[figure.X, figure.Y];

            if (figure.TryMoveFigure(targetX, targetY))
            {
                selectedSquare.RemoveFigure();
                squares[targetX, targetY].AddFigure(figure.ToUIElement());
                return true;
            }

            return false;
        }

        private void TrySelectFigure(BoardSquare square)
        {
            var figure = Figures.FirstOrDefault(x => x.HasCoordinates(square.X, square.Y));

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
                TrySelectFigure(square);
            }
        }
    }
}
