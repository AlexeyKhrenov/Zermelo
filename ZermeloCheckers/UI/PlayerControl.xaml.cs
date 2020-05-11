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
    public partial class PlayerControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty TimeToThinkProperty;
        public static readonly DependencyProperty PlyProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty IsButtonsEnabledProperty;
        public static readonly DependencyProperty IsSliderEnabledProperty;
        public static readonly DependencyProperty IsComputerPlayerProperty;
        public static readonly DependencyProperty IsHumanPlayerProperty;

        public static readonly DependencyProperty StopThinkingCommandProperty;
        public static readonly DependencyProperty UndoCommandProperty;

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

        public bool IsButtonsEnabled
        {
            get { return (bool)GetValue(IsButtonsEnabledProperty); }
            set { SetValue(IsButtonsEnabledProperty, value); }
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

        public ICommand StopThinkingCommand
        {
            get { return (ICommand)GetValue(StopThinkingCommandProperty); }
            set { SetValue(StopThinkingCommandProperty, value); }
        }

        public ICommand UndoCommand
        {
            get { return (ICommand)GetValue(UndoCommandProperty); }
            set { SetValue(UndoCommandProperty, value); }
        }

        static PlayerControl()
        {
            TimeToThinkProperty = DependencyProperty.Register(
                "TimeToThink", typeof(int), typeof(PlayerControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTimeToThinkChanged))
            );

            PlyProperty = DependencyProperty.Register(
                "Ply", typeof(int), typeof(PlayerControl)
            );

            TitleProperty = DependencyProperty.Register(
                "Title", typeof(string), typeof(PlayerControl)
            );

            IsActiveProperty = DependencyProperty.Register(
                "IsActive", typeof(bool), typeof(PlayerControl)
            );

            IsButtonsEnabledProperty = DependencyProperty.Register(
                "IsButtonsEnabled", typeof(bool), typeof(PlayerControl)
            );

            IsSliderEnabledProperty = DependencyProperty.Register(
                "IsSliderEnabled", typeof(bool), typeof(PlayerControl)
            );

            IsComputerPlayerProperty = DependencyProperty.Register(
                "IsComputerPlayer", typeof(bool), typeof(PlayerControl)
            );

            IsHumanPlayerProperty = DependencyProperty.Register(
                "IsHumanPlayer", typeof(bool), typeof(PlayerControl)
            );

            StopThinkingCommandProperty = DependencyProperty.Register(
                "StopThinkingCommand", typeof(ICommand), typeof(PlayerControl)
            );

            UndoCommandProperty = DependencyProperty.Register(
                "UndoCommand", typeof(ICommand), typeof(PlayerControl)
            );
        }

        public PlayerControl()
        {
            InitializeComponent();
            Slider.ValueChanged += Slider_ValueChanged;
        }

        public static void OnTimeToThinkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var playerControl = (PlayerControl)sender;
            var value = (int)e.NewValue;
            if (value == -1)
            {
                playerControl.Slider.Value = 101;
                playerControl.TimeToThink = value;
                playerControl.TimeToThinkLabel.Content = "\u221E";
            }
            else
            {
                playerControl.TimeToThinkLabel.Content = value;
                playerControl.TimeToThink = value;
                playerControl.Slider.Value = Math.Sqrt(value) / 2;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // todo - use default coeff as dependency property

            var value = (int)Math.Pow(e.NewValue * 2, 2);
            if (TimeToThink != value)
            {
                if (value > 40000)
                {
                    TimeToThink = (-1);
                }
                else
                {
                    TimeToThink = value;
                }
            }
        }

        private void StopThinkingButtonClick(object sender, RoutedEventArgs e)
        {
            StopThinkingCommand.Execute(null);
        }

        private void UntoButtonClick(object sender, RoutedEventArgs e)
        {
            UndoCommand.Execute(null);
        }
    }
}
