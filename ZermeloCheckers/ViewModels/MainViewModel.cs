using Game.Implementations;
using Game.Primitives;
using Game.PublicInterfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ZermeloCheckers.Misc;
using ZermeloCheckers.Models;

namespace ZermeloCheckers.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public PlayerViewModel Player1;
        public PlayerViewModel Player2;

        public bool IsBlocked => _model?.IsBlocked ?? false;

        private GameModel _model;
        private ObservableCollection<FigureViewModel> _figures;

        public MainViewModel()
        {
            Figures = new ObservableCollection<FigureViewModel>();

            Player1 = new PlayerViewModel("Computer player 1");
            Player2 = new PlayerViewModel("Computer player 2");
        }

        public ObservableCollection<FigureViewModel> Figures
        {
            get
            {
                return _figures;
            }
            set
            {
                _figures = value;
                RaisePropertyChanged();
            }
        }

        public void OnFigureMoved(object sender, byte x0, byte y0, byte x1, byte y1)
        {
            _model.Move(new Move(x0, y0, x1, y1));
        }

        public void OnFiguresUpdated()
        {
            var modelFigures = _model.Figures.Where(x => !x.IsCaptured);
            var uiFigures = Figures;

            var toBeRemoved = uiFigures.Except(modelFigures).ToList();

            // todo - refactor this
            var toBeUpdated = modelFigures.Join(uiFigures, x => x.GetHashCode(), x => x.GetHashCode(), (x,y) => new KeyValuePair<IFigure, FigureViewModel>(x,y)).ToList();
            var toBeInserted = modelFigures.Except(uiFigures).ToList();

            foreach (var r in toBeRemoved)
            {
                uiFigures.Remove(r as FigureViewModel);
            }

            foreach (var i in toBeInserted)
            {
                var newUiFigure = i.ToViewModel();
                newUiFigure.FigureMoved += OnFigureMoved;
                uiFigures.Add(newUiFigure);
            }

            foreach (var u in toBeUpdated)
            {
                u.Value.AvailableMoves = u.Key.AvailableMoves;
            }
        }

        public void FromModel(GameModel model)
        {
            _model = model;

            // still better then iterating through whole board
            if (Figures != null)
            {
                for (var i = Figures.Count - 1; i >= 0; i--)
                {
                    Figures.Remove(Figures[i]);
                }
            }

            _model.PropertyChanged += ModelPropertyChanged;

            Player1.FromModel(model.Player1Model);
            Player2.FromModel(model.Player2Model);

            OnFiguresUpdated();
            RaisePropertyChanged("ActivePlayer");
        }

        public override void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Figures")
            {
                OnFiguresUpdated();
            }

            base.ModelPropertyChanged(sender, e);
        }
    }
}
