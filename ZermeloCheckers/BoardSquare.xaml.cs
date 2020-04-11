using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for BoardSquare.xaml
    /// </summary>
    public partial class BoardSquare : UserControl, INotifyPropertyChanged
    {
        public event RoutedEventHandler OnMouseClick;
        public event PropertyChangedEventHandler PropertyChanged;

        public int X;

        public int Y;

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
        }

        public void RemoveFigure()
        {
            SquareButton.Content = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // do we need to check for null?
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
