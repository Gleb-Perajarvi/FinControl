using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Absolut.Model
{
    public class PresenterModel
    {
        private static Model _model;

        public static Model Model
        {
            get => _model;
            set => _model = value;
        }
    }
}
