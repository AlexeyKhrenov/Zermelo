using CheckersAI;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ZermeloCheckers.Models
{
    public class PlayerModel
    {
        public int TimeToThinkMs { get; private set; }

        public int Ply { get; private set; }

        public string Name => Player.Name;

        public bool PlayerControlAvailable { get; private set; }

        public bool IsComputerPlayer { get; private set; }

        public IPlayer Player;

        private DispatcherTimer _timer;

        public PlayerModel(IPlayer player)
        {
            Player = player;
            if (Player.IsComputerPlayer)
            {
                PlayerControlAvailable = true;
                IsComputerPlayer = true;

                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(100);
                _timer.Tick += new EventHandler((sender, e) => UpdatePly());
            }
            else
            {
                PlayerControlAvailable = false;
            }
        }

        public Task Act(IGame game)
        {
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
