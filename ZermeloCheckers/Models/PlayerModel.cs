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
            private set { _isActive = value; RaisePropertyChanged(); }
        }

        public int TimeToThinkMs { get; private set; }

        public IPlayer Player;

        private int _ply;
        private string _name;
        private bool _isComputerPlayer;
        private bool _isActive;
        private DispatcherTimer _timer;

        public PlayerModel(IPlayer player, int defaultTimeToThink)
        {
            Player = player;
            Name = player.Name;
            IsComputerPlayer = player.IsComputerPlayer;
            IsActive = player.IsActive;
            TimeToThinkMs = defaultTimeToThink;

            if (Player.IsComputerPlayer)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(100);
                _timer.Tick += new EventHandler((sender, e) => UpdatePly());
            }
            else
            {
            }
        }

        public Task Act(IGame game)
        {
            IsActive = true;

            if (IsComputerPlayer)
            {
                // todo - do we need to recreate this object?
                var token = new CancellationTokenSource();
                token.CancelAfter(TimeToThinkMs);

                StartUpdatingPly();

                // todo - refactor this
                return Task.Run(
                    () =>
                    {
                        Player.MakeMove(game, token.Token).Wait();
                        StopUpdatingPly();
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

        public void UpdatePly()
        {
            var ply = ((ComputerPlayer)Player).Ply;
            if (ply != Ply)
            {
                Ply = ply;
            }
        }

        public void UpdateTimeToThink(int timeToThink)
        {
            if (!Player.IsComputerPlayer)
            {
                throw new InvalidOperationException("Can't update player's time to think.");
            }
            else
            {
                TimeToThinkMs = timeToThink;
            }
        }

        public void StartUpdatingPly()
        {
            _timer.Start();
        }

        public void StopUpdatingPly()
        {
            _timer.Stop();
        }
    }
}
