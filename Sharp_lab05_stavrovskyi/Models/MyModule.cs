﻿using System;
using System.Diagnostics;


namespace Sharp_lab05_stavrovskyi.Models
{
    internal class MyModule
    {
        #region Fields

        private readonly ProcessModule _module;

        #endregion

        public string Name
        {
            get { return _module.ModuleName; }
        }

        public string Filepath
        {

            get
            {
                try
                {
                    return _module.FileName;
                }
                catch (Exception e) 
                {
                    return "Access denied";
                }
            }

        }

        internal MyModule(ProcessModule module)
        {
            _module = module;
        }
    }
}
