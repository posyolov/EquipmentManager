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
            PositionsVM positionsTreeVM = new PositionsVM(RepositoryData.GetPositions());
            WorkAreaVM workAreaVM = new WorkAreaVM(new ObservableCollection<string> { "111111111111111111111", "222222222222222222222222", "3333333333333333333333333", "4444444444444444", "555555555555555555555555555" });

            MainVM mainVM = new MainVM(positionsTreeVM, workAreaVM);

            MainView mainView = new MainView();
            mainView.DataContext = mainVM;
            mainView.Show();

        }
    }
}
