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

        public static DependencyProperty IsActiveProperty;

        public static DependencyProperty IsStopThinkingButtonEnabledProperty;

        public static DependencyProperty IsSliderEnabledProperty;

        public static DependencyProperty IsUndoButtonEnabledProperty;

        public static DependencyProperty IsComputerPlayerProperty;

        public static DependencyProperty IsHumanPlayerProperty;

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

        public bool IsStopThinkingButtonEnabled
        {
            get { return (bool)GetValue(IsStopThinkingButtonEnabledProperty); }
            set { SetValue(IsStopThinkingButtonEnabledProperty, value); }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public bool IsSliderEnabled
        {
            get { return (bool)GetValue(IsSliderEnabledProperty); }
            set { SetValue(IsSliderEnabledProperty, value); }
        }

        public bool IsUndoButtonEnabled
        {
            get { return (bool)GetValue(IsUndoButtonEnabledProperty); }
            set { SetValue(IsUndoButtonEnabledProperty, value); }
        }

        public bool IsComputerPlayer
        {
            get { return (bool)GetValue(IsComputerPlayerProperty); }
            set { SetValue(IsComputerPlayerProperty, value); }
        }

        public bool IsHumanPlayer
        {
            get { return (bool)GetValue(IsHumanPlayerProperty); }
            set { SetValue(IsHumanPlayerProperty, value); }
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

            IsActiveProperty = DependencyProperty.Register(
                "IsActive", typeof(bool), typeof(ComputerPlayerControl)
            );

            IsStopThinkingButtonEnabledProperty = DependencyProperty.Register(
                "IsStopThinkingButtonEnabled", typeof(bool), typeof(ComputerPlayerControl)
            );

            IsSliderEnabledProperty = DependencyProperty.Register(
                "IsSliderEnabled", typeof(bool), typeof(ComputerPlayerControl)
            );

            IsUndoButtonEnabledProperty = DependencyProperty.Register(
                "IsUndoButtonEnabled", typeof(bool), typeof(ComputerPlayerControl)
            );

            IsComputerPlayerProperty = DependencyProperty.Register(
                "IsComputerPlayer", typeof(bool), typeof(ComputerPlayerControl)
            );

            IsHumanPlayerProperty = DependencyProperty.Register(
                "IsHumanPlayer", typeof(bool), typeof(ComputerPlayerControl)
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
