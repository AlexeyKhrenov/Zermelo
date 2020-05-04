using CheckersAI;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ZermeloCheckers.Models;

namespace ZermeloCheckers.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private bool _isEnabled;
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; RaisePropertyChanged(); } }

        private string _defaultName;
        public string Name
        {
            get { return _model?.Name ?? _defaultName;}
            set { _defaultName = value; RaisePropertyChanged(); }
        }

        public int Ply => _model?.Ply ?? 0;

        public int TimeToThink
        {
            get { return _model?.TimeToThinkMs ?? 0; }
            set { _model.UpdateTimeToThink(value); }
        }

        public PlayerViewModel(string name)
        {
            Name = name;
        }

        private PlayerModel _model;
        public void FromModel(PlayerModel model)
        {
            _model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string memberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
