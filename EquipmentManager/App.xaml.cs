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
        GenericRepositoryEF<Position> positionRepos;
        GenericRepositoryEF<JournalEvent> journalRepos;
        GenericRepositoryEF<EventCategory> evCategoryRepository;

        PositionsVM positionsVM;
        JournalVM journalVM;
        MainVM mainVM;

        MainView mainView;
        CreateJournalEventView createJournalEventView;
               
        private void OnStartup(object sender, StartupEventArgs e)
        {
            //репозитории
            positionRepos = new GenericRepositoryEF<Position>();
            journalRepos = new GenericRepositoryEF<JournalEvent>();
            evCategoryRepository = new GenericRepositoryEF<EventCategory>();

            //ViewModels
            positionsVM = new PositionsVM(positionRepos);
            journalVM = new JournalVM(journalRepos);
            mainVM = new MainVM(positionsVM, journalVM);

            //Views
            positionsVM.CreateJournalEventEv += (p) =>
            {
                createJournalEventView = new CreateJournalEventView();
                createJournalEventView.Owner = mainView;
                createJournalEventView.DataContext = new CreateJournalEventVM(p, evCategoryRepository);

                //createJournalEventView.Closed += CreateJournalEventView_Closed;

                var res = createJournalEventView.ShowDialog();
            };

            //Main
            mainView = new MainView();
            mainView.DataContext = mainVM;
            mainView.Show();

        }

    }
}
