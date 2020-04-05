using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Sharp_lab05_stavrovskyi.Models;
using Sharp_lab05_stavrovskyi.Tools;
using Sharp_lab05_stavrovskyi.Tools.Managers;

namespace Sharp_lab05_stavrovskyi.ViewModels
{
    class ProcessViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<MyProcess> _processes;
        private Thread _workThread;
        private MyProcess _selectedProcess;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;

        private RelayCommand<object> _endTask;
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

        #endregion

        internal ProcessViewModel()
        {
            _processes = new ObservableCollection<MyProcess>(StationManager.ProcessList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
                StartWork();
            StationManager.StopThreads += StopWorkingThread;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            //_workThread.Abort();
            _workThread = null;
        }

        private void EndCommand(object o)
        {
            _selectedProcess?.Process?.Kill();
            StationManager.RemoveProcess(_selectedProcess);
            //StationManager.Update();
            SelectedProcess = null;

        }

        private bool CanExecuteCommand()
        {
            return SelectedProcess != null;
        }
    }
}
