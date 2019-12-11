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
    /// ViewModel positions tree
    /// </summary>
    public class PositionsVM : ViewModelBase
    {
        public event Action<Position> JournalEntryCreateReqEv;
        public event Action<JournalEntry> JournalEntryCreatedEv;

        IGenericRepository<Position> _repository;
        ObservableCollection<Position> _positions;
        IEnumerable<PositionStatusBitInfo> _positionStatusBitsInfo;

        public ObservableCollection<PositionNode> PositionsTree { get; set; }

        public DelegateCommand<object> JournalEntryCreateReqCommand { get; }
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
                SelectedItemPosData = value?.GetPositionData();

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

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public PositionsVM(IGenericRepository<Position> repository, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
        {
            _repository = repository;
            _positions = new ObservableCollection<Position>(_repository.Get());
            _positionStatusBitsInfo = positionStatusBitsInfo;

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (Position pos in _positions)
            {
                if (!pos.ParentId.HasValue)
                {
                    //для каждого строим ветвь
                    PositionNode posNode = new PositionNode(pos, _positionStatusBitsInfo);
                    posNode.PositionStatusChanged += OnPositionNodeChanged;
                    BuildBranch(posNode, _positions);
                    PositionsTree.Add(posNode);
                }
            }

            JournalEntryCreateReqCommand = new DelegateCommand<object>(
                execute: RiseJournalEntryCreateReqEv
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

        /// <summary>
        /// Building branch the node.
        /// </summary>
        /// <param name="positionNode"></param>
        /// <param name="positions"></param>
        void BuildBranch(PositionNode positionNode, IEnumerable<Position> positions)
        {
            var childrenPositions = positions.Where(c => c.ParentId == positionNode.PosData.Id);

            positionNode.Nodes = new ObservableCollection<PositionNode>();

            foreach (var pos in childrenPositions)
            {
                PositionNode posNode = new PositionNode(pos, _positionStatusBitsInfo);
                posNode.PositionStatusChanged += OnPositionNodeChanged;
                BuildBranch(posNode, positions);
                positionNode.Nodes.Add(posNode);
            }

        }

        /// <summary>
        /// Extract the node`s children position data to list recursively
        /// </summary>
        /// <param name="childrenPosData"></param>
        /// <param name="childNode"></param>
        void GetChildrenPosDataList(PositionNode childNode, List<Position> childrenPosData)
        {
            childrenPosData.Add(childNode.PosData);

            if (childNode.Nodes != null)
                foreach (PositionNode node in childNode.Nodes)
                    GetChildrenPosDataList(node, childrenPosData);
        }

        /// <summary>
        /// Remove node by ID in nodes collection
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nodes"></param>
        void RemoveNodeById(int id, ObservableCollection<PositionNode> nodes)
        {
            foreach (PositionNode node in nodes)
            {
                if (node.PosData.Id == id)
                {
                    nodes.Remove(node);
                    return;
                }

                RemoveNodeById(id, node.Nodes);
            }
        }

        /// <summary>
        /// Execute method for AddRootPositionCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void AddRootPositionExecute(object parametr)
        {
            Position pos = new Position()
            {
                //Id = присваивает метод AddOrUpdate EF
                Name = "New position",
            };

            SetComplexName(pos);
            _repository.Update(pos);

            PositionsTree.Add(new PositionNode(pos, _positionStatusBitsInfo));
        }

        /// <summary>
        /// Execute method for AddChildPositionCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void AddChildPositionExecute(object parametr)
        {
            Position pos = new Position()
            {
                //Id = присваивает метод AddOrUpdate EF
                Name = "New position",
                ParentId = SelectedItem.PosData.Id,
            };

            SetComplexName(pos);
            _repository.Update(pos);

            SelectedItem.Nodes.Add(new PositionNode(pos, _positionStatusBitsInfo));
        }

        /// <summary>
        /// Execute method for DeletePositionCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void DeletePositionExecute(object parametr)
        {
            List<Position> branchPosData = new List<Position>();
            GetChildrenPosDataList(SelectedItem, branchPosData);

            if (_repository.RemoveRange(branchPosData) == null)
                RemoveNodeById(SelectedItem.PosData.Id, PositionsTree);
            else
                System.Windows.MessageBox.Show("Заплатка! Добавить окно по шаблону MVVM!");
        }

        /// <summary>
        /// Execute method for SavePosDataCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void SavePosDataExecute(object parametr)
        {
            SetComplexName(SelectedItemPosData);

            _repository.Update(SelectedItemPosData);
            SelectedItem.SetPosData(SelectedItemPosData);
        }

        /// <summary>
        /// Execute method for JournalEntryCreateReqCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void RiseJournalEntryCreateReqEv(object parametr)
        {
            foreach (Position pos in _positions)
            {
                if (pos.Id == SelectedItemPosData.Id)
                {
                    JournalEntryCreateReqEv?.Invoke(pos);
                    break;
                }
            }
        }

        /// <summary>
        /// Set ComplexName property for the position
        /// </summary>
        /// <param name="pos"></param>
        private void SetComplexName(Position pos)
        {
            if (pos.ParentId != null)
            {
                Position parentPos = _repository.FindById((int)pos.ParentId);
                if (parentPos != null)
                {
                    pos.ComplexName = parentPos.ComplexName + ";" + pos.Name;
                }
            }
            else
                pos.ComplexName = pos.Name;

        }

        /// <summary>
        /// Update position data in Position table and add journal entry at changed status bit
        /// </summary>
        /// <param name="positionNode"></param>
        /// <param name="statusBit"></param>
        private void OnPositionNodeChanged(PositionNode positionNode, PositionStatusBit statusBit)
        {
            _repository.Update(positionNode.PosData);

            JournalEntry jEntry = new JournalEntry()
            {
                DateTime = DateTime.Now,
                Description = "",
                Position = positionNode.PosData,
                PositionStatusBitInfo = statusBit.StatusBitInfo,
                IsIncoming = statusBit.Value
            };
            
            JournalEntryCreatedEv?.Invoke(jEntry);
        }
    }
}
