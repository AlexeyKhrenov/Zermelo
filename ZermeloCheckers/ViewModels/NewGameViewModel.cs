using ZermeloCheckers.GameFactory;

namespace ZermeloCheckers.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        private bool _isOkButtonEnabled;
        private bool _isPlayer1Computer;

        private bool _isPlayer2Computer;

        private GameRequest _model;

        private string _player1Name;

        private string _player2Name;

        public bool IsPlayer1Computer
        {
            get => _isPlayer1Computer;
            set
            {
                _isPlayer1Computer = value;
                _model.IsPlayer1ComputerPlayer = value;
                RaisePropertyChanged();
            }
        }

        public bool IsPlayer2Computer
        {
            get => _isPlayer2Computer;
            set
            {
                _isPlayer2Computer = value;
                _model.IsPlayer2ComputerPlayer = value;
                RaisePropertyChanged();
            }
        }

        public string Player1Name
        {
            get => _player1Name;
            set
            {
                _player1Name = value;
                _model.Player1Name = value;
                CheckOkButtonEnabled();
                RaisePropertyChanged();
            }
        }

        public string Player2Name
        {
            get => _player2Name;
            set
            {
                _player2Name = value;
                _model.Player2Name = value;
                CheckOkButtonEnabled();
                RaisePropertyChanged();
            }
        }

        public bool IsOkButtonEnabled
        {
            get => _isOkButtonEnabled;
            set
            {
                _isOkButtonEnabled = value;
                RaisePropertyChanged();
            }
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
            if (IsOkButtonEnabled != isEnabled) IsOkButtonEnabled = isEnabled;
        }
    }
}