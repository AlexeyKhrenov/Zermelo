using System;
using System.Collections.Generic;
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
    public partial class ComputerPlayerControl : UserControl
    {
        public static DependencyProperty TitleProperty;

        public static DependencyProperty TimeToThinkProperty;

        public static DependencyProperty PlyProperty;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public int TimeToThink
        {
            get { return (int)GetValue(TimeToThinkProperty); }
            set { SetValue(TimeToThinkProperty, value); }
        }

        public int Ply
        {
            get { return (int)GetValue(PlyProperty); }
            set { SetValue(PlyProperty, value); }
        }

        static ComputerPlayerControl()
        {
            TimeToThinkProperty = DependencyProperty.Register(
                "TimeToThink", typeof(int), typeof(ComputerPlayerControl)
            );

            PlyProperty = DependencyProperty.Register(
                "Ply", typeof(int), typeof(ComputerPlayerControl)
            );

            TitleProperty = DependencyProperty.Register(
                "Title", typeof(string), typeof(ComputerPlayerControl)
            );
        }

        public ComputerPlayerControl()
        {
            InitializeComponent();
            Slider.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeToThink = (int)Math.Pow(e.NewValue * 2, 2);
        }
    }
}
