using System;
using System.Windows.Controls;

namespace Sharp_lab05_stavrovskyi.Tools
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
