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
        private void OnStartup(object sender, StartupEventArgs e)
        {
            GenericRepositoryEF<Position> positionRepos = new GenericRepositoryEF<Position>();
            GenericRepositoryEF<JournalEvent> journalRepos = new GenericRepositoryEF<JournalEvent>();

            PositionsVM positionsTreeVM = new PositionsVM(positionRepos);
            //JournalVM journalVM = new JournalVM(new ObservableCollection<string> { "111111111111111111111", "222222222222222222222222", "3333333333333333333333333", "4444444444444444", "555555555555555555555555555" });
            JournalVM journalVM = new JournalVM(journalRepos);

            MainVM mainVM = new MainVM(positionsTreeVM, journalVM);

            MainView mainView = new MainView();
            mainView.DataContext = mainVM;
            mainView.Show();

        }
    }
}
