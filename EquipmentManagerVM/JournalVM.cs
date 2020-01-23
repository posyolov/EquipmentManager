using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Repository;
using System.Windows.Data;
using System.ComponentModel;

namespace EquipmentManagerVM
{
    /// <summary>
    /// Journal entries ViewModel
    /// </summary>
    public class JournalVM : ViewModelBase
    {
        readonly IGenericRepository<JournalEntry> _journalRepos;

        ObservableCollection<JournalEntry> _totalJournalEntries;

        public ICollectionView FilteredJournalEntries { get; }

        public FilterCriteriaInterval<DateTime> FilterCriteriaDateTime { get; }
        public FilterCriteriaString FilterCriteriaPosition { get; }
        public FilterCriteriaEnumerable FilterCriteriaStatus { get; }
        public FilterCriteriaEnumerable FilterCriteriaCategory { get; }
        public FilterCriteriaString FilterCriteriaDescription { get; }

        public JournalVM(IGenericRepository<JournalEntry> journalRepository, IGenericRepository<JournalEntryCategory> journalEntryCategories, IGenericRepository<PositionStatusBitInfo> positionStatusBitInfo)
        {
            _journalRepos = journalRepository;
            _totalJournalEntries = new ObservableCollection<JournalEntry>(_journalRepos?.GetWithInclude(p => p.Position, c => c.JournalEntryCategory));

            FilterCriteriaDateTime = new FilterCriteriaInterval<DateTime>("Дата/время", DateTime.Now, DateTime.Now);
            FilterCriteriaDateTime.CriteriaChanged += () => FilteredJournalEntries.Refresh();

            FilterCriteriaPosition = new FilterCriteriaString("Позиция");
            FilterCriteriaPosition.CriteriaChanged += () => FilteredJournalEntries.Refresh();

            FilterCriteriaStatus = new FilterCriteriaEnumerable("Статус", positionStatusBitInfo.Get());
            FilterCriteriaStatus.CriteriaChanged += () => FilteredJournalEntries.Refresh();

            FilterCriteriaCategory = new FilterCriteriaEnumerable("Категория", journalEntryCategories.Get());
            FilterCriteriaCategory.CriteriaChanged += () => FilteredJournalEntries.Refresh();

            FilterCriteriaDescription = new FilterCriteriaString("Описание");
            FilterCriteriaDescription.CriteriaChanged += () => FilteredJournalEntries.Refresh();

            FilteredJournalEntries = CollectionViewSource.GetDefaultView(_totalJournalEntries);
            FilteredJournalEntries.Filter = JournalEntriesFilter;
        }

        private bool JournalEntriesFilter(object journalEntry)
        {
            if (journalEntry is JournalEntry entry)
            {
                return FilterCriteriaPosition.ContainsIn(entry.Position_Name) &&
                       FilterCriteriaStatus.EqualsTo(entry.PositionStatusBitInfo_BitNumber) &&
                       FilterCriteriaCategory.EqualsTo(entry.JournalEntryCategory_Id) &&
                       FilterCriteriaDescription.ContainsIn(entry.Description);
            }
            else
            {
                return false;
            }
        }

        public void AddJournalEntry(JournalEntry JournalEntry)
        {
            _totalJournalEntries.Add(JournalEntry);
        }
    }
}
