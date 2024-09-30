using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Absolut.Model;

namespace Absolut.ViewModel
{
    class VMMainMenu : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event EventHandler<string> Message;
        public event EventHandler<byte> OpenWindow;

        private Model.Model? _model;
        private string _title;
        private RelayCommand? _addCompany;
        private RelayCommand? _addActive;
        private RelayCommand? _addValues;
        private RelayCommand? _exit;
        private RelayCommand? _openHelpUser;
        private RelayCommand? _openHelpAdm;
        private RelayCommand? _openCreateRep;

        public RelayCommand CreateRep
        {
            get
            {
                return _openCreateRep ??
                    (_openCreateRep = new RelayCommand(obj =>
                    {
                        OpenWindow?.Invoke(this, 6);
                    }));
            }
        }

        public RelayCommand OpenHelpUser
        {
            get
            {
                return _openHelpUser ??
                    (_openHelpUser = new RelayCommand(obj =>
                    {
                        OpenWindow?.Invoke(this, 4);
                    },
                    (obj) => Model.Status == "user" || Model.Status == "admin"));
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
                    (obj) => Model.Status == "admin"));
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
                        OpenWindow?.Invoke(this, 0);
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
                        OpenWindow?.Invoke(this, 1);
                    }));
            }
        }

        public RelayCommand Exit
        {
            get
            {
                return _exit ??
                    (_exit = new RelayCommand(obj =>
                    {
                        OpenWindow?.Invoke(this, 2);
                    }));
            }
        }

        public RelayCommand AddCompany
        {
            get
            {
                return _addCompany ??
                    (_addCompany = new RelayCommand(obj =>
                    {
                        PresenterModel.Model.NameCompany = null;
                        OpenWindow?.Invoke(this, 3);
                    }));
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

        private string? _formatAccount;

        public string? FormatAccount
        {
            get
            {
                return _formatAccount;
            }
            set
            {
                _formatAccount = value;
                OnPropertyChanged("FormatAccount");
            }
        }

        public Model.Model? Model 
        { 
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnPropertyChanged("Model");
            }
        }

        private void OnMoneyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MoneyCompany")
            {
                GetFormatAccount();
            }
        }

        public VMMainMenu(Model.Model model)
        {
            if (model != null)
            {
                Model = model;
            }
            else
            {
                Model = new Model.Model();
                PresenterModel.Model = Model;
            }
            StartSettings();
        }

        private void StartSettings()
        {
            GetValues();
            GetFormatAccount();
            GetTitle(Model.Status);
        }

        private void GetValues()
        {
            Model.GetSpecValue("Account", "Money_Company", $"AS MC JOIN Companies AS C ON MC.ID_Company = C.ID WHERE C.Name = '{Model.NameCompany}' AND MC.ID = (SELECT MAX(ID) FROM Money_Company WHERE ID_Company = MC.ID_Company)");
            Model.GetValues("CI.ID,CI.Income,CI.Date_Income,CI.Comments", "Company_Income", $"AS CI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID WHERE Comp.Name = '{Model.NameCompany}' ORDER BY CI.ID DESC LIMIT 5");
            Model.GetValues("CE.ID,CE.Expense,CE.Date_Expense,CE.Comments", "Company_Expense", $"AS CE JOIN Companies AS Comp ON CE.ID_Company = Comp.ID WHERE CE.ID_Company = (SELECT ID FROM Companies WHERE Name = '{Model.NameCompany}') ORDER BY CE.ID DESC LIMIT 5");
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

        private void GetFormatAccount()
        {
            var f = new NumberFormatInfo { NumberGroupSeparator = " " };
            FormatAccount = Model.MoneyCompany.ToString("n0", f) + "$";
        }
    }
}
