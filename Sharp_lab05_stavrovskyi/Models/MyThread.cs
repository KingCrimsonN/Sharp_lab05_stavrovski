using System;
using System.Diagnostics;


namespace Sharp_lab05_stavrovskyi.Models
{
    class MyThread
    {
        #region Fields

        private readonly ProcessThread _thread;

        #endregion

        public int Id
        {
            get { return _thread.Id; }
        }

        public ThreadState State
        {

            get
            {
                return _thread.ThreadState;

            }

        }

        public DateTime StartingTime
        {
            get { return _thread.StartTime; }
        }
        internal MyThread(ProcessThread thread)
        {
            _thread = thread;
        }
    }
}
