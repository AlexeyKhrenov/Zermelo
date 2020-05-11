using CheckersAI;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ZermeloCheckers.Models
{
    public class PlayerModel : BaseModel
    {
        public bool IsComputerPlayer
        {
            get { return _isComputerPlayer; }
            set { _isComputerPlayer = value; RaisePropertyChanged(); }
        }
        
        public string Name
        {
            get { return _name; }
            private set { _name = value; RaisePropertyChanged(); }
        }

        public int Ply
        {
            get { return _ply; }
            private set { _ply = value; RaisePropertyChanged(); }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; RaisePropertyChanged(); }
        }

        public bool IsUndoEnabled
        {
            get { return _isUndoEnabled; }
            set { _isUndoEnabled = value; RaisePropertyChanged(); }
        }

        public int TimeToThinkMs
        {
            get { return _timeToThinkMs; }
            private set { _timeToThinkMs = value; RaisePropertyChanged(); }
        }

        public IPlayer Player;

        public Action<IPlayer> UndoMoveCallback { get; set; }
        
        private int _ply;
        private int _timeToThinkMs;
        private string _name;
        private bool _isUndoEnabled;
        private bool _isComputerPlayer;
        private bool _isActive;
        private CancellationTokenSource _cts;

        public PlayerModel(IPlayer player, int defaultTimeToThink)
        {
            Player = player;
            Name = player.Name;
            IsComputerPlayer = player.IsComputerPlayer;
            IsActive = player.IsActive;

            if (Player.IsComputerPlayer)
            {
                UpdateTimeToThink(defaultTimeToThink);
            }
        }

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
                        Ply = ((ComputerPlayer)Player).Ply;
                    });
            }
            else
            {
                return Task.CompletedTask;
            }
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
            if (_cts != null && !_cts.Token.IsCancellationRequested)
            {
                _cts.Cancel();
            }
        }

        public void UpdateTimeToThink(int timeToThink)
        {
            if (timeToThink < -1)
            {
                throw new ArgumentException("Time to think can't be less than -1");
            }
            if (!Player.IsComputerPlayer)
            {
                throw new InvalidOperationException("Can't update player's time to think.");
            }
            else
            {
                TimeToThinkMs = timeToThink;
            }
        }
    }
}
