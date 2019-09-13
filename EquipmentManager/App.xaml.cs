using Repository;
using EquipmentManagerVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace EquipmentManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        EquipmentContainer _context;

        GenericRepositoryEF<Position> _positionRepos;
        GenericRepositoryEF<JournalEntry> _journalRepos;
        GenericRepositoryEF<EntryCategory> _evCategoryRepos;

        PositionsVM _positionsVM;
        JournalVM _journalVM;
        CreateJournalEntryVM _createJournalEntryVM;
        MainVM _mainVM;

        MainView _mainView;
        CreateJournalEntryView _createJournalEntryView;
               
        private void OnStartup(object sender, StartupEventArgs e)
        {
            //репозитории
            _context = new EquipmentContainer();

            _positionRepos = new GenericRepositoryEF<Position>(_context);
            _journalRepos = new GenericRepositoryEF<JournalEntry>(_context);
            _evCategoryRepos = new GenericRepositoryEF<EntryCategory>(_context);

            //GenericRepositoryEF<PositionNode> tempRepos = new GenericRepositoryEF<PositionNode>(_context);
            GenericRepositoryAccess<StockItem> st = new GenericRepositoryAccess<StockItem>(@"D:\Projects\Access\EquipmentManager\Склад.accdb");
            var dwdw = st.Get();

            //ViewModels
            _positionsVM = new PositionsVM(_positionRepos);
            _journalVM = new JournalVM(_journalRepos);
            _mainVM = new MainVM(_positionsVM, _journalVM);

            //Views
            //открытие окна создания записи журнала по событию
            _positionsVM.CreateJournalEntryReqEv += PositionsVM_CreateJournalEntryReqEv;

            //Main
            _mainView = new MainView();
            _mainView.DataContext = _mainVM;
            _mainView.Show();

        }

        private void PositionsVM_CreateJournalEntryReqEv(Position position)
        {
            _createJournalEntryVM = new CreateJournalEntryVM(position, _evCategoryRepos, _journalRepos);
            _createJournalEntryVM.JournalEntryCreatedEv += (je) =>_journalVM.AddJournalEntry(je);

            _createJournalEntryView = new CreateJournalEntryView();
            _createJournalEntryView.Owner = _mainView;
            _createJournalEntryView.DataContext = _createJournalEntryVM;
            _createJournalEntryView.ShowDialog();
        }

    }
}
