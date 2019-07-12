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
        readonly IEntities<Position> positionsRepos;

        public ObservableCollection<Position> Positions { get; }
        public ObservableCollection<PositionNode> PositionsTree { get; }

        public DelegateCommand<object> AddRootPositionCommand { get; }
        public DelegateCommand<object> AddChildPositionCommand { get; }
        public DelegateCommand<object> DeletePositionCommand { get; }

        private PositionNode _selectedItem;
        private PositionNode _selectedItemEdit;

        public PositionNode SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
                SelectedItemEdit = value; //надо копировать по значению

                AddChildPositionCommand.RiseCanExecuteChanged();
                DeletePositionCommand.RiseCanExecuteChanged();
            }
        }

        public PositionNode SelectedItemEdit
        {
            get { return _selectedItemEdit; }
            set
            {
                _selectedItemEdit = value;
                NotifyPropertyChanged();
            }
        }

        public PositionsVM(IEntities<Position> positionsRepository)
        {
            positionsRepos = positionsRepository;

            Positions = positionsRepository.Entities;

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (Position pos in Positions)
            {
                if (!pos.ParentId.HasValue)
                {
                    //для каждого строим ветвь
                    PositionNode posNode = new PositionNode(pos);
                    BuildBranch(posNode, Positions);
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
            Position pos = new Position()
            {
                Id = _selectedItem.Id,
                Name = _selectedItem.Name,
                ParentId = _selectedItem.ParentId,
                Title = _selectedItem.Title
            };

            positionsRepos.Update(pos);
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
