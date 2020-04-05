using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using Sharp_lab05_stavrovskyi.Models;
using Sharp_lab05_stavrovskyi.Tools;
using Sharp_lab05_stavrovskyi.Tools.Managers;
using Sharp_lab05_stavrovskyi.Views;

namespace Sharp_lab05_stavrovskyi.ViewModels
{
    class ProcessViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<MyProcess> _processes;
        private Thread _workThread;
        private MyProcess _selectedProcess;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;

        private RelayCommand<object> _endTask;
        private RelayCommand<object> _showModulesCommand;
        private RelayCommand<object> _showThreadsCommand;
        #endregion

        #region Properties

        public ObservableCollection<MyProcess> Processes
        {
            get => _processes;
            private set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }

        public MyProcess SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {

                _selectedProcess = value;
                StationManager.SelectedProcess = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> EndTask
        {
            get
            {
                return _endTask ?? (_endTask = new RelayCommand<object>(EndCommand, o => CanExecuteCommand()));

            }
        }

        public RelayCommand<object> ShowModules
        {
            get
            {
                return _showModulesCommand ?? (_showModulesCommand =
                    new RelayCommand<object>(ShowModulesCommand, o => CanExecuteCommand()));
            }
        }

        public RelayCommand<object> ShowThreads
        {
            get
            {
                return _showThreadsCommand ?? (_showThreadsCommand =
                    new RelayCommand<object>(ShowThreadsCommand, o => CanExecuteCommand()));
            }
        }

        #endregion

        internal ProcessViewModel()
        {
            _processes = new ObservableCollection<MyProcess>(StationManager.ProcessList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWork();
            StationManager.StopThreads += StopWorkingThread;
        }

        private void StartWork()
        {
            _workThread = new Thread(WorkProcess);
            _workThread.Start();
        }

        private void WorkProcess()
        {
            while (!_token.IsCancellationRequested)
            {
                int temp = -1;
                if (SelectedProcess != null)
                {
                    temp = SelectedProcess.ID;
                }

                StationManager.Update();

                Processes = new ObservableCollection<MyProcess>(StationManager.ProcessList);

                foreach (var p in Processes)
                {
                    if (p.ID == temp)
                    {
                        SelectedProcess = p;
                        break;
                    }
                }
                Thread.Sleep(2000);
            }
        }
        internal void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workThread.Join(1000);
            _workThread = null;
        }

        private void EndCommand(object o)
        {
            _selectedProcess?.Process?.Kill();
            StationManager.RemoveProcess(_selectedProcess);
            //StationManager.Update();
            SelectedProcess = null;

        }

        private void ShowModulesCommand(object o)
        {
            try
            {
                ModuleView smw = new ModuleView();
                smw.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: Modules can't be observed!");
            }
        }

        private void ShowThreadsCommand(object o)
        {
            try
            {
                ThreadView smw = new ThreadView();
                smw.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: Threads can't be observed!");
            }
        }

        private bool CanExecuteCommand()
        {
            return SelectedProcess != null;
        }
    }
}
