using CheckersAI;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ZermeloCheckers.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private bool _isEnabled;
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; RaisePropertyChanged(); } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged(); } }

        private int _maxPly;
        public int MaxPly
        {
            get { return _maxPly; }
            set { _maxPly = value; RaisePropertyChanged(); }
        }

        private int _timeToThink;
        public int TimeToThink
        {
            get { return _timeToThink; }
            set { _timeToThink = value; RaisePropertyChanged(); }
        }

        private int _sliderValue;
        public int SliderValue
        {
            get { return _sliderValue; }
            set { _sliderValue = value; RaisePropertyChanged(); }
        }

        public PlayerViewModel(string name)
        {
            Name = name;
            // todo - change to false after debug
            _isEnabled = false;
        }

        public void FromModel(IPlayer player)
        {
            if (player == null)
            {
                IsEnabled = false;
                Name = "Computer player";
            }

            switch (player)
            {
                case ComputerPlayer cp:
                    IsEnabled = true;
                    cp.MaxPlyChanged += OnMaxPlyChanged;
                    break;
                default:
                    IsEnabled = false;
                    break;
            }

            Name = player.Name;
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        private void OnMaxPlyChanged(int newValue)
        {
            MaxPly = newValue;
        }
    }
}
