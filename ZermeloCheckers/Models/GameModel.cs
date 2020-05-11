using Game.Primitives;
using Game.PublicInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZermeloCheckers.Models
{
    // todo - remove INotifyPropertyChanged if not needed
    public class GameModel : BaseModel
    {
        public PlayerModel Player1Model;

        public PlayerModel Player2Model;

        public IEnumerable<IFigure> Figures => _game.Board.Figures;

        public bool IsUndoEnabled;

        public bool IsBlocked
        {
            get { return _isBlocked; }
            set { _isBlocked = value; RaisePropertyChanged(); }
        }

        private bool _isBlocked;
        private IGame _game;

        public GameModel(IGame game, int defaultTimeToThink)
        {
            _game = game;
            Player1Model = new PlayerModel(game.Board.Player1, defaultTimeToThink);
            Player2Model = new PlayerModel(game.Board.Player2, defaultTimeToThink);

            Player1Model.UndoMoveCallback += OnUndo;
            Player2Model.UndoMoveCallback += OnUndo;

            InvokeUiUpdate();
            NextMove();
        }

        public void Move(Move move)
        {
            _game.Move(move);
            InvokeUiUpdate();
            NextMove();
        }

        public void OnUndo(IPlayer player)
        {
            _game.Undo(player);
            NextMove();
            InvokeUiUpdate();
        }

        public void OnBoardBlockedUnblocked(bool isBlocked)
        {
            IsBlocked = isBlocked;
        }

        public void NextMove()
        {
            var activePlayerModel = _game.Board.ActivePlayer == Player1Model.Player ? Player1Model : Player2Model;
            var awaitingPlayerModel = _game.Board.ActivePlayer == Player1Model.Player ? Player2Model : Player1Model;

            awaitingPlayerModel.Wait();

            if (activePlayerModel.IsComputerPlayer)
            {
                IsBlocked = true;
                activePlayerModel.Act(_game).ContinueWith(task => NextMove());
            }
            else
            {
                IsBlocked = false;
                activePlayerModel.Act(_game).Wait();
            }

            InvokeUiUpdate();
        }

        private void InvokeUiUpdate()
        {
            // update board UI after move
            RaisePropertyChanged("Figures");
            Player1Model.IsActive = _game.Board.ActivePlayer == Player1Model.Player;
            Player2Model.IsActive = _game.Board.ActivePlayer == Player2Model.Player;
            Player1Model.IsUndoEnabled = _game.CanUndo;
            Player1Model.IsUndoEnabled = _game.CanUndo;
        }
    }
}
