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
                SelectedItemPosData = value.CopyPosData();

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

        private void AddRootPositionExecute(object parametr)
        {
            Position pos = new Position()
            {
                //Id = _selectedItem.PosData.Id,
                Name = "New position",
                //ParentId = _selectedItem.PosData.ParentId,
                //Title = _selectedItem.PosData.Title
            };

            positionsRepos.Update(pos);
            PositionsTree.Add(new PositionNode(pos));
        }

        private void AddChildPositionExecute(object parametr)
        {
            ;
        }

        private void DeletePositionExecute(object parametr)
        {
            ;
        }

        private void SavePosDataExecute(object parametr)
        {
            //var node = PositionsTree.Where(p => p.Id == SelectedItemPosData.Id).FirstOrDefault();
            //if(node != null)
            //    node.SetPosData(SelectedItemPosData);
            //NotifyPropertyChanged("PositionsTree");

            //PositionsTree[0].Name = "kuku";
            //NotifyPropertyChanged("PositionsTree");

            //работает
            //PositionsTree.Add(new PositionNode(new Position() { Id = 222, Name = "KUKU" }));

            //работает
            //PositionsTree[0] = new PositionNode(new Position() { Id = 222, Name = "KUKU" });

            //заработало
            //PositionsTree[0].PosData = new Position() { Id = 222, Name = "KUKU" };

            //SelectedItem.PosData = new Position() { Name = SelectedItemPosData.Name, Title = SelectedItemPosData.Title };

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
