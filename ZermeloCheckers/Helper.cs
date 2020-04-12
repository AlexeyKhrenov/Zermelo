using Game.PublicInterfaces;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZermeloCheckers
{
    public static class Helper
    {
        public static FigureViewModel ToViewModel(this IFigure figure)
        {
            return new FigureViewModel()
            {
                X = figure.X,
                Y = figure.Y,
                Type = figure.Type,
                AllowedMoves = figure.AvailableMoves
            };
        }

        // todo - move this component to XAML
        public static UIElement ToUIElement(this FigureViewModel figureViewModel)
        {
            return new Rectangle()
            {
                Height = 26,
                Width = 26,
                Fill = figureViewModel.Type != "White" ? Brushes.Black : Brushes.White,
                //Stroke = Brushes.Non
                StrokeThickness = 2,
            };
        }
    }
}
