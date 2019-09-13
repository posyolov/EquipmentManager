using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;
using System.ComponentModel;

namespace EquipmentManagerVM
{
    /// <summary>
    /// ViewModel для дерева позиций
    /// </summary>
    public class PositionsVM : ViewModelBase
    {
        public event Action<Position> CreateJournalEntryReqEv;

        readonly IGenericRepository<Position> positionsRepos;

        IEnumerable<Position> _positions;
        public ObservableCollection<PositionNode> PositionsTree { get; set; }

        public DelegateCommand<object> CreateJournalEntryReqCommand { get; }
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

            _positions = positionsRepository.Get();

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (Position pos in _positions)
            {
                if (!pos.ParentId.HasValue)
                {
                    //для каждого строим ветвь
                    PositionNode posNode = new PositionNode(pos);
                    BuildBranch(posNode, _positions);
                    PositionsTree.Add(posNode);
                }
            }

            CreateJournalEntryReqCommand = new DelegateCommand<object>(
                execute: RiseCreateJournalEntryReqEv
                );

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

        void BuildBranch(PositionNode positionNode, IEnumerable<Position> positions)
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

            SetComplexName(pos);
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

            SetComplexName(pos);
            positionsRepos.Update(pos);

            SelectedItem.Nodes.Add(new PositionNode(pos));
        }

        private void DeletePositionExecute(object parametr)
        {
            List<Position> branchPosData = new List<Position>();
            GetChildrenPosData(branchPosData, SelectedItem);

            if (positionsRepos.RemoveRange(branchPosData) == null)
                RemoveNode(SelectedItem.PosData.Id, PositionsTree);
            else
                System.Windows.MessageBox.Show("Заплатка! Добавить окно по шаблону MVVM!");
        }

        private void SavePosDataExecute(object parametr)
        {
            SetComplexName(SelectedItemPosData);

            positionsRepos.Update(SelectedItemPosData);
            SelectedItem.SetPosData(SelectedItemPosData);
        }


        private void RiseCreateJournalEntryReqEv(object parametr)
        {
            foreach (Position pos in _positions)
            {
                if (pos.Id == SelectedItemPosData.Id)
                {
                    CreateJournalEntryReqEv?.Invoke(pos);
                    break;
                }
            }
        }

        //задание составного имени
        private void SetComplexName(Position pos)
        {
            if (pos.ParentId != null)
            {
                Position parentPos = positionsRepos.FindById((int)pos.ParentId);
                if (parentPos != null)
                {
                    pos.ComplexName = parentPos.ComplexName + ";" + pos.Name;
                }
            }
            else
                pos.ComplexName = pos.Name;

        }
    }
}
