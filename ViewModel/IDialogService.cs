using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Absolut.ViewModel
{
    internal interface IDialogService
    {
        bool YesOrNo(string message, string caption);
    }
}
