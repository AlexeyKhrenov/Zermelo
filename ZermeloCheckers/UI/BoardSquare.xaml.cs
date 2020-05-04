using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ZermeloCheckers
{
    public partial class BoardSquare : UserControl, INotifyPropertyChanged
    {
        public event RoutedEventHandler OnMouseClick;

        public event PropertyChangedEventHandler PropertyChanged;

        public byte X;

        public byte Y;

        public bool IsBlack { get; set; }

        public bool IsSelected { get; set; }

        public bool IsAllowedMove { get; set; }

        public BoardSquare()
        {
            InitializeComponent();
            IsBlack = false;
        }

        public void AddFigure(UIElement figure)
        {
            SquareButton.Content = figure;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
        }

        public void RemoveFigure()
        {
            SquareButton.Content = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // todo - do we need to check for null?
            OnMouseClick?.Invoke(this, e);
        }

        public void Select()
        {
            IsSelected = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
        }

        public void Deselect()
        {
            IsSelected = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
        }

        public void SelectAllowedMove()
        {
            IsAllowedMove = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAllowedMove"));
        }

        public void DeselectAllowedMove()
        {
            IsAllowedMove = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsAllowedMove"));
        }
    }
}
