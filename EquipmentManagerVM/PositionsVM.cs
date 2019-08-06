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
        readonly IGenericRepository<Position> positionsRepos;

        public ObservableCollection<Position> Positions { get; }
        public ObservableCollection<PositionNode> PositionsTree { get; set; }

        public DelegateCommand<object> AddRootPositionCommand { get; }
        public DelegateCommand<object> AddChildPositionCommand { get; }
        public DelegateCommand<object> DeletePositionCommand { get; }
        public DelegateCommand<object> SavePosDataCommand { get; }

        private PositionNode _selectedItem;
        private Position _selectedItemPosData;

        public PositionNode SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();

                //приходится каждый раз создавать экземпляр
                SelectedItemPosData = value?.CopyPosData();

                AddChildPositionCommand.RiseCanExecuteChanged();
                DeletePositionCommand.RiseCanExecuteChanged();
            }
        }

        public Position SelectedItemPosData
        {
            get { return _selectedItemPosData; }
            set
            {
                _selectedItemPosData = value;
                NotifyPropertyChanged();
            }
        }

        public PositionsVM(IGenericRepository<Position> positionsRepository)
        {
            positionsRepos = positionsRepository;

            Positions = positionsRepository.Get();

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

            SavePosDataCommand = new DelegateCommand<object>(
                execute: SavePosDataExecute
                );

        }

        void BuildBranch(PositionNode positionNode, ObservableCollection<Position> positions)
        {
            var childrenPositions = positions.Where(c => c.ParentId == positionNode.PosData.Id);

            positionNode.Nodes = new ObservableCollection<PositionNode>();

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos);
                BuildBranch(posNode, positions);
                positionNode.Nodes.Add(posNode);
            }

        }

        void GetChildrenPosData(List<Position> childrenPosData, PositionNode childNode)
        {
            childrenPosData.Add(childNode.PosData);

            if (childNode.Nodes != null)
                foreach (PositionNode node in childNode.Nodes)
                    GetChildrenPosData(childrenPosData, node);
        }

        void RemoveNode(int id, ObservableCollection<PositionNode> nodes)
        {
            foreach (PositionNode node in nodes)
            {
                if (node.PosData.Id == id)
                {
                    nodes.Remove(node);
                    return;
                }

                RemoveNode(id, node.Nodes);
            }
        }

        private void AddRootPositionExecute(object parametr)
        {
            Position pos = new Position()
            {
                //Id = присваивает метод AddOrUpdate EF
                Name = "New position",
            };

            positionsRepos.Update(pos);
            PositionsTree.Add(new PositionNode(pos));
        }

        private void AddChildPositionExecute(object parametr)
        {
            Position pos = new Position()
            {
                //Id = присваивает метод AddOrUpdate EF
                Name = "New position",
                ParentId = SelectedItem.PosData.Id,
            };

            positionsRepos.Update(pos);

            SelectedItem.Nodes.Add(new PositionNode(pos));
        }

        private void DeletePositionExecute(object parametr)
        {
            List<Position> branchPosData = new List<Position>();
            GetChildrenPosData(branchPosData, SelectedItem);
            positionsRepos.RemoveRange(branchPosData);

            RemoveNode(SelectedItem.PosData.Id, PositionsTree);
        }

        private void SavePosDataExecute(object parametr)
        {
            positionsRepos.Update(SelectedItemPosData);
            SelectedItem.SetPosData(SelectedItemPosData);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
