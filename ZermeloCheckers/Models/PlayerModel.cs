using System;
using System.Threading;
using System.Threading.Tasks;
using CheckersAI;
using Game.PublicInterfaces;

namespace ZermeloCheckers.Models
{
    public class PlayerModel : BaseModel
    {
        private CancellationTokenSource _cts;
        private bool _isActive;
        private bool _isComputerPlayer;
        private bool _isUndoEnabled;
        private string _name;

        private int _ply;
        private int _timeToThinkMs;

        public IPlayer Player;

        public PlayerModel(IPlayer player, int defaultTimeToThink)
        {
            Player = player;
            Name = player.Name;
            IsComputerPlayer = player.IsComputerPlayer;
            IsActive = player.IsActive;

            if (Player.IsComputerPlayer) UpdateTimeToThink(defaultTimeToThink);
        }

        public bool IsComputerPlayer
        {
            get => _isComputerPlayer;
            set
            {
                _isComputerPlayer = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public int Ply
        {
            get => _ply;
            private set
            {
                _ply = value;
                RaisePropertyChanged();
            }
        }

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                RaisePropertyChanged();
            }
        }

        public bool IsUndoEnabled
        {
            get => _isUndoEnabled;
            set
            {
                _isUndoEnabled = value;
                RaisePropertyChanged();
            }
        }

        public int TimeToThinkMs
        {
            get => _timeToThinkMs;
            private set
            {
                _timeToThinkMs = value;
                RaisePropertyChanged();
            }
        }

        public Action<IPlayer> UndoMoveCallback { get; set; }

        public Task Act(IGame game)
        {
            IsActive = true;

            if (IsComputerPlayer)
            {
                // todo - do we need to recreate this object?
                _cts = new CancellationTokenSource();
                _cts.CancelAfter(TimeToThinkMs);

                // todo - refactor this
                return Task.Run(
                    () =>
                    {
                        Player.MakeMove(game, _cts.Token).Wait();
                        Ply = ((ComputerPlayer) Player).Ply;
                    });
            }

            return Task.CompletedTask;
        }

        public void Wait()
        {
            IsActive = false;
        }

        public void Undo()
        {
            UndoMoveCallback?.Invoke(Player);
        }

        public void StopThinking()
        {
            if (_cts != null && !_cts.Token.IsCancellationRequested) _cts.Cancel();
        }

        public void UpdateTimeToThink(int timeToThink)
        {
            if (timeToThink < -1) throw new ArgumentException("Time to think can't be less than -1");
            if (!Player.IsComputerPlayer)
                throw new InvalidOperationException("Can't update player's time to think.");
            TimeToThinkMs = timeToThink;
        }
    }
}