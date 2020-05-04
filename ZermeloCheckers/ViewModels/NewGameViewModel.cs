using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ZermeloCheckers.GameFactory;

namespace ZermeloCheckers.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        private bool _isPlayer1Computer;
        public bool IsPlayer1Computer
        {
            get { return _isPlayer1Computer; }
            set
            {
                _isPlayer1Computer = value;
                _model.IsPlayer1ComputerPlayer = value;
                RaisePropertyChanged();
            }
        }

        private bool _isPlayer2Computer;
        public bool IsPlayer2Computer
        {
            get {return _isPlayer2Computer; }
            set
            {
                _isPlayer2Computer = value;
                _model.IsPlayer2ComputerPlayer = value;
                RaisePropertyChanged();
            }
        }

        private string _player1Name;
        public string Player1Name
        {
            get { return _player1Name; }
            set
            {
                _player1Name = value;
                _model.Player1Name = value;
                CheckOkButtonEnabled();
                RaisePropertyChanged();
            }
        }

        private string _player2Name;
        public string Player2Name
        {
            get { return _player2Name; }
            set
            {
                _player2Name = value;
                _model.Player2Name = value;
                CheckOkButtonEnabled();
                RaisePropertyChanged();
            }
        }

        private bool _isOkButtonEnabled;
        public bool IsOkButtonEnabled
        {
            get { return _isOkButtonEnabled; }
            set { _isOkButtonEnabled = value; RaisePropertyChanged(); }
        }

        private GameRequest _model;

        public NewGameViewModel()
        {
        }

        public void FromModel(GameRequest model)
        {
            _model = model;

            // default values
            Player1Name = "Player 1";
            Player2Name = "Player 2";
            IsPlayer2Computer = true;
        }

        private void CheckOkButtonEnabled()
        {
            var isEnabled = !string.IsNullOrWhiteSpace(Player1Name) && !string.IsNullOrWhiteSpace(Player2Name);
            if (IsOkButtonEnabled != isEnabled)
            {
                IsOkButtonEnabled = isEnabled;
            }
        }
    }
}
