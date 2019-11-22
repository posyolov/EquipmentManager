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

        /// <summary>
        /// Create main ViewModel and View. Create other Views by events from MainVM.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _mainVM = new MainVM();
            _mainVM.JournalEntryCreateViewEv += OnJournalEntryCreateViewEv;

            _mainView = new MainView
            {
                DataContext = _mainVM
            };
            _mainView.Show();
        }

        /// <summary>
        /// Show JournalEntryCreateView on request from MainView.
        /// </summary>
        /// <param name="vm"></param>
        private void OnJournalEntryCreateViewEv(JournalEntryCreateVM vm)
        {
            JournalEntryCreateView _journalEntryCreateView = new JournalEntryCreateView
            {
                Owner = _mainView,
                DataContext = vm
            };
            _journalEntryCreateView.ShowDialog();
        }
    }
}
