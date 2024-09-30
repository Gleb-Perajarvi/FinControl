using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Absolut.Model;

namespace Absolut.ViewModel
{
    internal class VMActivities : INotifyPropertyChanged
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
        public event EventHandler<bool> OpenMenuActivity;

        private Model.Model? _model;
        private string? _selectedItem;
        private string _inputText;
        private RelayCommand? _addActivity;
        private RelayCommand? _removeActivity;
        private RelayCommand? _setActivity;
        private IDialogService _dialogService;

        public RelayCommand AddActivity
        {
            get
            {
                return _addActivity ??
                    (_addActivity = new RelayCommand(obj =>
                    {
                        Currentmodel.Activities.Insert(0, InputText);
                        _selectedItem = InputText;
                        Message?.Invoke(this, Currentmodel.AddValue($"INSERT INTO Company_Activity (ID_Company,Name_of_Activity) VALUES ((SELECT ID FROM Companies WHERE Name = '{Currentmodel.NameCompany}'),'{InputText}')"));

                    },
                    (obj) => !string.IsNullOrWhiteSpace(InputText)));
            }
        }

        public RelayCommand RemoveActivity
        {
            get
            {
                return _removeActivity ??
                    (_removeActivity = new RelayCommand(obj =>
                    {
                        if (_dialogService.YesOrNo($"Вы точно желаете удалить {SelectedItem}", "Предупреждение"))
                        {
                            string selected = obj as string;
                            if (selected != null)
                            {
                                Currentmodel.Activities.Remove(selected);
                                Message?.Invoke(this, Currentmodel.DeleteValue($"DELETE FROM Company_Activity WHERE Name_of_Activity = '{selected}'"));
                            }
                        }
                        else
                        {
                            return;
                        }
                    },
                    (obj) => SelectedItem != null && Currentmodel.Activities.Count > 0 && Currentmodel.Status == "admin"));
            }
        }

        public RelayCommand SetActivity
        {
            get
            {
                return _setActivity ??
                    (_setActivity = new RelayCommand(obj =>
                    {
                        string setact = obj as string;

                        if (setact != null)
                        {
                            Currentmodel.CompanyActivity = setact;

                            CheckIsWinOpen.IsWinActOpen = false;

                            Currentmodel.GetSpecValue("Account_Activity", "Activity_Money", $"AS AM JOIN Companies AS C ON AM.ID_Company = C.ID JOIN Company_Activity AS CA ON AM.ID_Activity = CA.ID WHERE CA.Name_of_Activity = '{Currentmodel.CompanyActivity}' AND C.Name = '{Currentmodel.NameCompany}' AND AM.ID = (SELECT MAX(ID) FROM Activity_Money WHERE ID_Activity = AM.ID_Activity AND ID_Company = AM.ID_Company)");
                            Currentmodel.GetValues("AI.ID,AI.Activity_Income,AI.Date_Income,AI.Comments", "Activity_Income", $"AS AI JOIN Companies AS C ON AI.ID_Company = C.ID JOIN Company_Activity AS CA ON AI.ID_Activity = CA.ID WHERE CA.Name_of_Activity = '{Currentmodel.CompanyActivity}' AND C.Name = '{Currentmodel.NameCompany}' ORDER BY AI.ID DESC LIMIT 5");
                            Currentmodel.GetValues("AE.ID,AE.Activity_Expense,AE.Date_Expense,AE.Comments", "Activity_Expense", $"AS AE JOIN Companies AS C ON AE.ID_Company = C.ID JOIN Company_Activity AS CA ON AE.ID_Activity = CA.ID WHERE CA.Name_of_Activity = '{Currentmodel.CompanyActivity}' AND C.Name = '{Currentmodel.NameCompany}' ORDER BY AE.ID DESC LIMIT 5");

                            PresenterModel.Model = Currentmodel;
                            OpenMenuActivity?.Invoke(this, true);
                        }
                    },
                    (obj) => SelectedItem != null && Currentmodel.Activities.Count > 0));
            }
        }

        public string? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged("InputText");
            }
        }

        public Model.Model? Currentmodel
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Currentmodel");
            }
        }

        public VMActivities(Model.Model model, IDialogService dialogService)
        {
            if (model != null)
            {
                _model = model;
            }
            else
            {
                _model = new Model.Model();
                PresenterModel.Model = _model;
                
            }

            _model.GetValues("Name_of_Activity", "Company_Activity", $"AS CA JOIN Companies AS C ON CA.ID_Company = C.ID WHERE CA.ID_Company = (SELECT ID FROM Companies WHERE Name = '{_model.NameCompany}')");
            this._dialogService = dialogService;
        }
    }
}
