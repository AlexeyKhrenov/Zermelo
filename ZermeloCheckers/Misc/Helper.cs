using Game.PublicInterfaces;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ZermeloCheckers.ViewModels;

namespace ZermeloCheckers.Misc
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
                AvailableMoves = figure.AvailableMoves
            };
        }

        // todo - move this component to XAML
        public static UIElement ToUIElement(this FigureViewModel figure)
        {
            if (figure.Type == "WhiteQueen" || figure.Type == "BlackQueen")
            {
                return new Rectangle()
                {
                    Height = 26,
                    Width = 26,
                    Fill = figure.Type != "WhiteQueen" ? Brushes.Black : Brushes.White,
                    Stroke = Brushes.Tan,
                    StrokeThickness = 5,
                };
            }
            else
            {
                return new Rectangle()
                {
                    Height = 26,
                    Width = 26,
                    Fill = figure.Type != "White" ? Brushes.Black : Brushes.White,
                };
            }
        }
    }
}
