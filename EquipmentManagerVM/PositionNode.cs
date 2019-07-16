using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace EquipmentManagerVM
{
    public class PositionNode : INotifyPropertyChanged
    {
        private Position _posData;

        public ObservableCollection<PositionNode> Nodes { get; set; }

        public Position PosData
        {
            get => _posData;
            set
            {
                _posData = value;
                NotifyPropertyChanged();
            }
        }

        public PositionNode(Position position)
        {
            PosData = position;
            //Id = position.Id;
            //ParentId = position.ParentId;
            //Name = position.Name;
            //Title = position.Title;
        }

        public Position CopyPosData()
        {
            return new Position() { Id = PosData.Id, Name = PosData.Name, ParentId = PosData.ParentId, Title = PosData.Title };
        }

        public void SetPosData(Position posData)
        {
            PosData = new Position() { Id = posData.Id, Name = posData.Name, ParentId = posData.ParentId, Title = posData.Title }; ;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
