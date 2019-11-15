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
        MainVM _mainVM;
        MainView _mainView;

        // Create main ViewModel and View. Create other Views by events from MainVM.
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Main ViewModel.
            _mainVM = new MainVM();
            // Subscribe to create jornal entry view
            _mainVM.JournalEntryCreateViewEv += (vm) =>
            {
                JournalEntryCreateView _journalEntryCreateView = new JournalEntryCreateView
                {
                    Owner = _mainView,
                    DataContext = vm
                };
                _journalEntryCreateView.ShowDialog();
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
