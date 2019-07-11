using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EquipmentManagerVM
{
    public class PositionsVM : INotifyPropertyChanged
    {
        public ObservableCollection<Position> Positions { get; }
        public ObservableCollection<PositionNode> PositionsTree { get; }

        public DelegateCommand<object> AddRootPositionCommand { get; }
        public DelegateCommand<object> AddChildPositionCommand { get; }
        public DelegateCommand<object> DeletePositionCommand { get; }

        private PositionNode _selectedItem;
        public PositionNode SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();

                AddChildPositionCommand.RiseCanExecuteChanged();
                DeletePositionCommand.RiseCanExecuteChanged();
            }
        }

        public PositionsVM(ObservableCollection<Position> positions)
        {
            Positions = positions;

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (Position pos in positions)
            {
                if (!pos.ParentId.HasValue)
                {
                    //для каждого строим ветвь
                    PositionNode posNode = new PositionNode(pos);
                    BuildBranch(posNode, positions);
                    PositionsTree.Add(posNode);
                }
            }

            AddRootPositionCommand = new DelegateCommand<object>(
                execute: AddRootPositionExecute
                );

            AddChildPositionCommand = new DelegateCommand<object>(
                execute: AddChildPositionExecute,
                canExecute: (s) => { return _selectedItem != null; }
                );

            DeletePositionCommand = new DelegateCommand<object>(
                execute: DeletePositionExecute,
                canExecute: (s) => { return _selectedItem != null; }
                );
        }

        void BuildBranch(PositionNode positionNode, ObservableCollection<Position> positions)
        {
            var childrenPositions = positions.Where(c => c.ParentId == positionNode.Id);

            positionNode.Nodes = new ObservableCollection<PositionNode>();

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos);
                BuildBranch(posNode, positions);
                positionNode.Nodes.Add(posNode);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddRootPositionExecute(object parametr)
        {
            ;
        }

        private void AddChildPositionExecute(object parametr)
        {
            ;
        }

        private void DeletePositionExecute(object parametr)
        {
            ;
        }

    }
}
