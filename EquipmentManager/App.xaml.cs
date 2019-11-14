using EquipmentManagerVM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using EquipmentManagerM;

namespace EquipmentManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        Manager _manager;
        MainVM _mainVM;
        MainView _mainView;

        // Application startup.
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Main Model.
            _manager = new Manager();

            // Main ViewModel.
            _mainVM = new MainVM(_manager);
            // Subscribe to 
            _mainVM.CreateJournalEntryViewEv += (vm) =>
                {
                    CreateJournalEntryView _createJournalEntryView = new CreateJournalEntryView();
                    _createJournalEntryView.Owner = _mainView;
                    _createJournalEntryView.DataContext = vm;
                    _createJournalEntryView.ShowDialog();
                };

            // Main View.
            _mainView = new MainView
            {
                DataContext = _mainVM
            };
            _mainView.Show();
        }
    }
}
