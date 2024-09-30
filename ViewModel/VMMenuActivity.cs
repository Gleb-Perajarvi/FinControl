using Absolut.Model;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Absolut.ViewModel
{
    internal class VMMenuActivity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) 
            { 
                PropertyChanged(this, new PropertyChangedEventArgs(prop)); 
            }
        }

        public event EventHandler<byte> OpenWindow;

        private Model.Model _currentModel;
        private RelayCommand? _addCompany;
        private RelayCommand? _addActive;
        private RelayCommand? _addValues;
        private RelayCommand? _openHelpUser;
        private RelayCommand? _openHelpAdm;

        public RelayCommand OpenHelpUser
        {
            get
            {
                return _openHelpUser ??
                    (_openHelpUser = new RelayCommand(obj =>
                    {
                        OpenWindow?.Invoke(this, 4);
                    },
                    (obj) => CurrentModel.Status == "user" || CurrentModel.Status == "admin"));
            }
        }

        public RelayCommand OpenHelpAdm
        {
            get
            {
                return _openHelpAdm ??
                    (_openHelpAdm = new RelayCommand(obj =>
                    {
                        OpenWindow?.Invoke(this, 5);
                    },
                    (obj) => CurrentModel.Status == "admin"));
            }
        }

        private string _title;
        private string _formatAcc;

        public string FormatAccount
        {
            get => _formatAcc;
            set
            {
                _formatAcc = value;
                OnPropertyChanged("FormatAccount");
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        public RelayCommand AddCompany
        {
            get
            {
                return _addCompany ??
                    (_addCompany = new RelayCommand(obj =>
                    {
                        PresenterModel.Model.CompanyActivity = null;
                        OpenWindow?.Invoke(this, 0);
                    }));
            }
        }

        public RelayCommand AddActive
        {
            get
            {
                return _addActive ??
                    (_addActive = new RelayCommand(obj =>
                    {
                        PresenterModel.Model.CompanyActivity = null;
                        OpenWindow?.Invoke(this, 1);
                    }));
            }
        }

        public RelayCommand AddValues
        {
            get
            {
                return _addValues ??
                    (_addValues = new RelayCommand(obj =>
                    {
                        OpenWindow?.Invoke(this, 2);
                    }));
            }
        }

        public Model.Model CurrentModel
        {
            get => _currentModel;
            set
            {
                _currentModel = value;
                OnPropertyChanged("CurrentModel");
            }
        }

        public VMMenuActivity(Model.Model model)
        {
            if (model == null)
            {
                _currentModel= new Model.Model();
                PresenterModel.Model = _currentModel;
            }
            else
            {
                _currentModel = model;
            }

            StartSettings();
        }

        private void StartSettings()
        {
            GetFormatAccount(_currentModel.ActivityMoney);
            GetTitle(_currentModel.Status);
        }

        private void GetTitle(string? status)
        {
            switch (status)
            {
                case "user":
                    {
                        Title = "Пользователь";
                    }
                    break;
                case "admin":
                    {
                        Title = "Администратор";
                    }
                    break;
                default:
                    {
                        Title = "Гость";
                    }
                    break;
            }
        }

        private void GetFormatAccount(int money)
        {
            var f = new NumberFormatInfo { NumberGroupSeparator = " " };
            FormatAccount = money.ToString("n0", f) + "$";
        }
    }
}
