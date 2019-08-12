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
        public event Action<JournalEvent> JournalEventCreatedEv;

        public JournalEvent JEvent { get; set; }

        public IEnumerable<EventCategory> EvCategories { get; }
        IGenericRepository<JournalEvent> _journalRepos;
        IGenericRepository<EventCategory> _evCategoryRepository;

        private bool closeTrigger;
        public bool CloseTrigger
        {
            get => closeTrigger;
            set
            {
                closeTrigger = value;
                NotifyPropertyChanged();
            }
        }

        public DelegateCommand<object> CreateJournalEventCommand { get; }

        public CreateJournalEventVM(Position position, IGenericRepository<EventCategory> evCategoryRepository, IGenericRepository<JournalEvent> journalRepository)
        {
            //запрос списка категорий
            _evCategoryRepository = evCategoryRepository;
            EvCategories = _evCategoryRepository.Get();

            JEvent = new JournalEvent()
            {
                DateTime = DateTime.Now,
                Position = position,
                //EventCategory = new EventCategory()
            };

            CreateJournalEventCommand = new DelegateCommand<object>(
                execute: CreateJournalEventExecute,
                canExecute: CreateJournalEventCanExecute
                );

            _journalRepos = journalRepository;
        }

        private bool CreateJournalEventCanExecute(object obj)
        {
            return true; // JEvent.Position != null && JEvent.EventCategory != null;
        }

        private void CreateJournalEventExecute(object obj)
        {
            if (JEvent != null && JEvent.Position != null && JEvent.EventCategory != null)
            {
                CloseTrigger = true;

                _journalRepos.Add(JEvent);

                JournalEventCreatedEv?.Invoke(JEvent);
            }
        }

    }
}
