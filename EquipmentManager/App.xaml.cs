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
        GenericRepositoryEF<JournalEvent> _journalRepos;
        GenericRepositoryEF<EventCategory> _evCategoryRepos;

        PositionsVM _positionsVM;
        JournalVM _journalVM;
        CreateJournalEventVM _createJournalEventVM;
        MainVM _mainVM;

        MainView _mainView;
        CreateJournalEventView _createJournalEventView;
               
        private void OnStartup(object sender, StartupEventArgs e)
        {
            //репозитории
            _context = new EquipmentContainer();
            _positionRepos = new GenericRepositoryEF<Position>(_context);
            _journalRepos = new GenericRepositoryEF<JournalEvent>(_context);
            _evCategoryRepos = new GenericRepositoryEF<EventCategory>(_context);

            //ViewModels
            _positionsVM = new PositionsVM(_positionRepos);
            _journalVM = new JournalVM(_journalRepos);
            _mainVM = new MainVM(_positionsVM, _journalVM);

            //Views
            _positionsVM.CreateJournalEventReqEv += PositionsVM_CreateJournalEventReqEv;

            //Main
            _mainView = new MainView();
            _mainView.DataContext = _mainVM;
            _mainView.Show();

        }

        private void PositionsVM_CreateJournalEventReqEv(Position position)
        {
            _createJournalEventVM = new CreateJournalEventVM(position, _evCategoryRepos, _journalRepos);
            _createJournalEventVM.JournalEventCreatedEv += (je) =>_journalVM.AddJournalEvent(je);

            _createJournalEventView = new CreateJournalEventView();
            _createJournalEventView.Owner = _mainView;
            _createJournalEventView.DataContext = _createJournalEventVM;
            _createJournalEventView.ShowDialog();
        }

    }
}
