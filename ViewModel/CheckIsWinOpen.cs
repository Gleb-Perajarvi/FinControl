using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absolut.ViewModel
{
    public static class CheckIsWinOpen
    {
        private static bool _isWinActOpen = false;
        private static bool _isWinCompOpen = false;

        public static bool IsWinActOpen
        {
            get => _isWinActOpen;
            set => _isWinActOpen = value;
        }

        public static bool IsWinCompOpen
        {
            get => _isWinCompOpen;
            set => _isWinCompOpen = value;
        }
    }
}
