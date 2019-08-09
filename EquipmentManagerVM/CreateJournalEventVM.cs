using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagerVM
{
    public class CreateJournalEventVM : ViewModelBase
    {
        public JournalEvent JEvent { get; }
        public IEnumerable<EventCategory> EvCategories { get; }

        private bool closeTrigger;
        public bool CloseTrigger
        {
            get { return this.closeTrigger; }
            set
            {
                closeTrigger = value;
                NotifyPropertyChanged();
            }
        }

        public DelegateCommand<object> CreateJournalEventCommand { get; }

        public CreateJournalEventVM(Position position, IGenericRepository<EventCategory> evCategoryRepository)
        {
            //запрос списка категорий
            EvCategories = evCategoryRepository.Get();

            JEvent = new JournalEvent()
            {
                DateTime = DateTime.Now,
                Position = position,
            };

            CreateJournalEventCommand = new DelegateCommand<object>(
                execute: CreateJournalEventExecute,
                canExecute: CreateJournalEventCanExecute
                );
        }

        private bool CreateJournalEventCanExecute(object obj)
        {
            return JEvent.Position != null && JEvent.EventCategory != null;
        }

        private void CreateJournalEventExecute(object obj)
        {
            if(JEvent != null)
                ;

            CloseTrigger = true;
        }

    }
}
