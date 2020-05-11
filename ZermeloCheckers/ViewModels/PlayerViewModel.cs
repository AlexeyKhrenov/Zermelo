using CheckersAI;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using ZermeloCheckers.Misc;
using ZermeloCheckers.Models;

namespace ZermeloCheckers.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public string Name
        {
            get { return _model?.Name ?? _defaultName;}
            set { _defaultName = value; RaisePropertyChanged(); }
        }

        public bool IsHumanPlayer => _model != null ? !_model.IsComputerPlayer : false;

        public bool IsComputerPlayer => _model?.IsComputerPlayer ?? false;

        public int Ply => _model?.Ply ?? 0;

        public bool IsActive => _model?.IsActive ?? false;

        public int TimeToThink
        {
            get { return _model?.TimeToThinkMs ?? -1; }
            set { _model.UpdateTimeToThink(value); }
        }

        public ICommand StopThinkingCommand { get; set; }

        public ICommand UndoCommand { get; set; }

        public bool IsButtonsEnabled
        {
            get { return _isButtonsEnabled; }
            set { _isButtonsEnabled = value; RaisePropertyChanged(); }
        }

        public bool IsSliderEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; RaisePropertyChanged(); }
        }

        private bool _isEnabled;
        private bool _isButtonsEnabled;
        private string _defaultName;
        private PlayerModel _model;

        public PlayerViewModel(string name)
        {
            Name = name;

            StopThinkingCommand = new RelayCommand(obj => OnStopThinking());
            UndoCommand = new RelayCommand(obj => OnUndo());
        }

        public void FromModel(PlayerModel model)
        {
            _model = model;
            _model.PropertyChanged += ModelPropertyChanged;

            RaisePropertyChanged("Name");
            RaisePropertyChanged("IsActive");
            RaisePropertyChanged("IsComputerPlayer");
            RaisePropertyChanged("IsHumanPlayer"); // todo -remove this code duplication
            RaisePropertyChanged("TimeToThink");
        }

        public override void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsComputerPlayer")
            {
                RaisePropertyChanged("IsHumanPlayer");
            }

            if (e.PropertyName == "IsActive" || e.PropertyName == "IsUndoEnabled")
            {
                if (_model.IsActive)
                {
                    IsSliderEnabled = false;
                    
                    if (_model.IsComputerPlayer)
                    {
                        IsButtonsEnabled = true;
                    }
                    else
                    {
                        IsButtonsEnabled = _model.IsUndoEnabled;
                    }
                }
                else
                {
                    IsSliderEnabled = true;
                    IsButtonsEnabled = false;
                }
            }

            base.ModelPropertyChanged(sender, e);
        }

        public void OnStopThinking()
        {
            _model.StopThinking();
        }

        public void OnUndo()
        {
            _model.Undo();
        }
    }
}
