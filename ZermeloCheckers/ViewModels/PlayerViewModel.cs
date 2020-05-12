using System.ComponentModel;
using System.Windows.Input;
using ZermeloCheckers.Misc;
using ZermeloCheckers.Models;

namespace ZermeloCheckers.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private string _defaultName;
        private bool _isButtonsEnabled;

        private bool _isEnabled;
        private PlayerModel _model;

        public PlayerViewModel(string name)
        {
            Name = name;

            StopThinkingCommand = new RelayCommand(obj => OnStopThinking());
            UndoCommand = new RelayCommand(obj => OnUndo());
        }

        public string Name
        {
            get => _model?.Name ?? _defaultName;
            set
            {
                _defaultName = value;
                RaisePropertyChanged();
            }
        }

        public bool IsHumanPlayer => !_model?.IsComputerPlayer ?? false;

        public bool IsComputerPlayer => _model?.IsComputerPlayer ?? false;

        public int Ply => _model?.Ply ?? 0;

        public bool IsActive => _model?.IsActive ?? false;

        public int TimeToThink
        {
            get => _model?.TimeToThinkMs ?? -1;
            set => _model.UpdateTimeToThink(value);
        }

        public ICommand StopThinkingCommand { get; set; }

        public ICommand UndoCommand { get; set; }

        public bool IsButtonsEnabled
        {
            get => _isButtonsEnabled;
            set
            {
                _isButtonsEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSliderEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }

        public void FromModel(PlayerModel model)
        {
            _model = model;
            _model.PropertyChanged += ModelPropertyChanged;

            ModelPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            ModelPropertyChanged(this, new PropertyChangedEventArgs("IsActive"));
            ModelPropertyChanged(this, new PropertyChangedEventArgs("IsComputerPlayer"));
            ModelPropertyChanged(this, new PropertyChangedEventArgs("IsHumanPlayer"));
            ModelPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            ModelPropertyChanged(this, new PropertyChangedEventArgs("TimeToThink"));
            ModelPropertyChanged(this, new PropertyChangedEventArgs("Ply"));
        }

        public override void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsComputerPlayer") RaisePropertyChanged("IsHumanPlayer");

            if (e.PropertyName == "IsActive" || e.PropertyName == "IsUndoEnabled")
            {
                if (_model.IsActive)
                {
                    IsSliderEnabled = false;

                    if (_model.IsComputerPlayer)
                        IsButtonsEnabled = true;
                    else
                        IsButtonsEnabled = _model.IsUndoEnabled;
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