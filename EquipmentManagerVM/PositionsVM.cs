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

        private IGenericRepository<Position> _positionsRepository;
        private IEnumerable<Position> _positions;
        private IEnumerable<PositionStatusBitInfo> _positionStatusBitsInfo;

        public ObservableCollection<PositionNode> PositionsTree { get; set; }

        public DelegateCommand<object> JournalEntryCreateReqCommand { get; }
        public DelegateCommand<object> AddRootPositionCommand { get; }
        public DelegateCommand<object> AddChildPositionCommand { get; }
        public DelegateCommand<object> DeletePositionCommand { get; }
        public DelegateCommand<object> SavePosDataCommand { get; }

        private PositionNode _selectedNode;
        public PositionNode SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                _selectedNode = value;
                NotifyPropertyChanged();

                AddChildPositionCommand.RiseCanExecuteChanged();
                DeletePositionCommand.RiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public PositionsVM(IGenericRepository<Position> repository, IEnumerable<PositionStatusBitInfo> positionStatusBitsInfo)
        {
            _positionsRepository = repository;
            _positions = _positionsRepository.Get();
            _positionStatusBitsInfo = positionStatusBitsInfo;

            PositionsTree = new ObservableCollection<PositionNode>();

            //добавляем узлы верхнего уровня
            foreach (Position pos in _positions)
            {
                if (pos.ParentName == null)
                {
                    PositionNode posNode = new PositionNode(pos, _positions, _positionStatusBitsInfo);
                    posNode.PositionStatusChanged += OnPositionNodeStatusChanged;
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
                canExecute: (s) => { return _selectedNode != null; }
                );

            DeletePositionCommand = new DelegateCommand<object>(
                execute: DeletePositionExecute,
                canExecute: (s) => { return _selectedNode != null; }
                );

            SavePosDataCommand = new DelegateCommand<object>(
                execute: SavePosDataExecute
                );

        }

        /// <summary>
        /// Remove node by ID in nodes collection
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nodes"></param>
        void RemoveNodeFromTree(string name, ObservableCollection<PositionNode> nodes)
        {
            foreach (PositionNode node in nodes)
            {
                if (node.PositionData.Name == name)
                {
                    nodes.Remove(node);
                    return;
                }

                RemoveNodeFromTree(name, node.Nodes);
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
                Name = "ROOT_POS"
            };

            var posNode = new PositionNode(pos, _positionStatusBitsInfo)
            {
                IsSelected = true
            };
            posNode.PositionStatusChanged += OnPositionNodeStatusChanged;

            PositionsTree.Add(posNode);
        }

        /// <summary>
        /// Execute method for AddChildPositionCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void AddChildPositionExecute(object parametr)
        {
            Position pos = new Position()
            {
                Name = SelectedNode.PositionData.Name + "_XX",
                ParentName = SelectedNode.PositionData.Name
            };

            var posNode = new PositionNode(pos, _positionStatusBitsInfo)
            {
                IsSelected = true
            };

            SelectedNode.AddChild(posNode);
        }

        /// <summary>
        /// Execute method for DeletePositionCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void DeletePositionExecute(object parametr)
        {
            List<Position> branchPosData = SelectedNode.GetAllPositionsList();

            Exception res = _positionsRepository.RemoveRange(branchPosData);

            if (res == null)
                RemoveNodeFromTree(SelectedNode.PositionData.Name, PositionsTree);
            else
                System.Windows.MessageBox.Show(res.ToString() + "\nЗаплатка! Добавить окно по шаблону MVVM!");
        }

        /// <summary>
        /// Execute method for SavePosDataCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void SavePosDataExecute(object parametr)
        {
            _positionsRepository.Update(SelectedNode.PositionData);
            //SelectedItem.SetPosData(SelectedItemPosData);
        }

        /// <summary>
        /// Execute method for JournalEntryCreateReqCommand
        /// </summary>
        /// <param name="parametr"></param>
        private void RiseJournalEntryCreateReqEv(object parametr)
        {
            foreach (Position pos in _positions)
            {
                if (pos.Name == SelectedNode.PositionData.Name)
                {
                    JournalEntryCreateReqEv?.Invoke(pos);
                    break;
                }
            }
        }

        /// <summary>
        /// Update position data in Position table and add journal entry at changed status bit
        /// </summary>
        /// <param name="positionNode"></param>
        /// <param name="statusBit"></param>
        private void OnPositionNodeStatusChanged(PositionNode positionNode, PositionStatusBit statusBit)
        {
            _positionsRepository.Update(positionNode.PositionData);

            JournalEntry jEntry = new JournalEntry()
            {
                DateTime = DateTime.Now,
                Description = "",
                Position = positionNode.PositionData,
                PositionStatusBitInfo = statusBit.StatusBitInfo,
                IsIncoming = statusBit.Value
            };
            
            JournalEntryCreatedEv?.Invoke(jEntry);
        }
    }
}
