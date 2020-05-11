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

        public event Action FigureUpdatedEvent;

        public bool IsUndoEnabled;

        private IGame _game;

        public GameModel(IGame game, int defaultTimeToThink)
        {
            _game = game;
            Player1Model = new PlayerModel(game.Board.Player1, defaultTimeToThink);
            Player2Model = new PlayerModel(game.Board.Player2, defaultTimeToThink);

            Player1Model.UndoMoveCallback += OnUndo;
            Player2Model.UndoMoveCallback += OnUndo;

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

        public void NextMove()
        {
            var activePlayerModel = _game.Board.ActivePlayer == Player1Model.Player ? Player1Model : Player2Model;
            var awaitingPlayerModel = _game.Board.ActivePlayer == Player1Model.Player ? Player2Model : Player1Model;

            awaitingPlayerModel.Wait();

            if (activePlayerModel.IsComputerPlayer)
            {
                activePlayerModel.Act(_game).ContinueWith(task => NextMove());
            }
            else
            {
                activePlayerModel.Act(_game).Wait();
            }

            InvokeUiUpdate();
        }

        private void InvokeUiUpdate()
        {
            // update board UI after move
            FigureUpdatedEvent?.Invoke();
            Player1Model.IsActive = _game.Board.ActivePlayer == Player1Model.Player;
            Player2Model.IsActive = _game.Board.ActivePlayer == Player2Model.Player;
            Player1Model.IsUndoEnabled = _game.CanUndo;
            Player1Model.IsUndoEnabled = _game.CanUndo;
        }
    }
}
