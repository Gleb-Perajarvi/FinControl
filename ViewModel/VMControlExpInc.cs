using Absolut.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Absolut.ViewModel
{
    internal class VMControlExpInc : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private IDialogService _dialogService;

        private byte _addVal = 3;
        private bool _modelNull = false;
        private string _inputAcc;
        private string _inputInc;
        private string _inputIncCom;
        private string _inputExp;
        private string _inputExpCom;

        private string _textAddComp;
        private string _textAddAct;

        private int _inc = 0;
        private int _exp = 0;
        private string _dateinc;
        private string _dateexp;

        private DateTime? _dateIncome;
        private DateTime? _dateExpense;

        public DateTime? DateIncome
        {
            get => _dateIncome;
            set
            {
                _dateIncome = value;
                OnPropertyChanged("DateIncome");
            }
        }

        public DateTime? DateExpense
        {
            get => _dateExpense;
            set
            {
                _dateExpense = value;
                OnPropertyChanged("DateExpense");
            }
        }

        public string TextAddComp
        {
            get => _textAddComp;
        }

        public string TextAddAct
        {
            get => _textAddAct;
        }

        public string InputAccount
        {
            get => _inputAcc;
            set
            {
                _inputAcc = value;
                OnPropertyChanged("InputAccount");
            }
        }

        public string InputIncome
        {
            get => _inputInc;
            set
            {
                _inputInc = value;
                OnPropertyChanged("InputIncome");
            }
        }

        public string InputExpense
        {
            get => _inputExp;
            set
            {
                _inputExp = value;
                OnPropertyChanged("InputExpense");
            }
        }

        public string InputIncCom
        {
            get => _inputIncCom;
            set
            {
                _inputIncCom = value;
                OnPropertyChanged("InputIncCom");
            }
        }

        public string InputExpCom
        {
            get => _inputExpCom;
            set
            {
                _inputExpCom = value;
                OnPropertyChanged("InputExpCom");
            }
        }

        public event EventHandler<string> Message;
        public event EventHandler<byte> OpenBorders;

        private Model.Model _currentModel;
        private RelayCommand? _addValueComp;
        private RelayCommand? _addValueAct;
        private RelayCommand? _addAccount;
        private RelayCommand? _addInfoIncome;
        private RelayCommand? _addInfoExpense;

        public RelayCommand AddInfoExpense
        {
            get
            {
                return _addInfoExpense ??
                    (_addInfoExpense = new RelayCommand(obj =>
                    {
                        AddExpense();
                    },
                    (obj) => !string.IsNullOrWhiteSpace(InputExpense) && DateExpense != null && !_modelNull && _addVal != 3));
            }
        }

        public RelayCommand AddInfoIncome
        {
            get
            {
                return _addInfoIncome ??
                    (_addInfoIncome = new RelayCommand(obj =>
                    {
                        AddIncome();
                    },
                    (obj) => !string.IsNullOrWhiteSpace(InputIncome) && DateIncome != null && !_modelNull && _addVal != 3));
            }
        }

        private void AddExpense()
        {
            if (int.TryParse(InputExpense, out _exp) && _exp > 0)
            {
                _dateexp = DateExpense.Value.ToString("dd.MM.yyyy HH:mm");
                string comment = GetComment(InputExpCom);

                switch (_addVal)
                {
                    case 0:
                        {
                            AddSpecValue($"INSERT INTO Company_Expense (ID_Company, Expense, Date_Expense, Comments) VALUES ({GetIDCompany()}, '{_exp}', '{_dateexp}', '{comment}')");
                            CurrentModel.MoneyCompany -= _exp;
                            CurrentModel.GetValues("CE.ID,CE.Expense,CE.Date_Expense,CE.Comments", "Company_Expense", $"AS CE JOIN Companies AS Comp ON CE.ID_Company = Comp.ID WHERE CE.ID_Company = (SELECT ID FROM Companies WHERE Name = '{PresenterModel.Model.NameCompany}') ORDER BY CE.ID DESC LIMIT 5");
                            CurrentModel.AddValue($"INSERT INTO Money_Company (ID_Company,Account) VALUES ({GetIDCompany()}, '{CurrentModel.MoneyCompany}')");
                        }
                        break;
                    case 1:
                        {
                            AddSpecValue($"INSERT INTO Activity_Expense (ID_Company, ID_Activity, Activity_Expense, Date_Expense, Comments) VALUES ({GetIDCompany()}, {GetIDActive()}, '{_exp}', '{_dateexp}', '{comment}')");
                            CurrentModel.ActivityMoney -= _exp;
                            CurrentModel.GetValues("AE.ID,AE.Activity_Expense,AE.Date_Expense,AE.Comments", "Activity_Expense", $"AS AE JOIN Companies AS C ON AE.ID_Company = C.ID JOIN Company_Activity AS CA ON AE.ID_Activity = CA.ID WHERE CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' AND C.Name = '{CurrentModel.NameCompany}' ORDER BY AE.ID DESC LIMIT 5");
                            CurrentModel.AddValue($"INSERT INTO Activity_Money (ID_Company, ID_Activity, Account_Activity) VALUES ({GetIDCompany()}, {GetIDActive()}, '{CurrentModel.ActivityMoney}')");
                        }
                        break;
                }
            }
        }

        private void AddIncome()
        {
            if (int.TryParse(InputIncome, out _inc) && _inc > 0)
            {
                _dateinc = DateIncome.Value.ToString("dd.MM.yyyy HH:mm");
                string comments = GetComment(InputIncCom);

                switch (_addVal)
                {
                    case 0:
                        {
                            AddSpecValue($"INSERT INTO Company_Income (ID_Company, Income, Date_Income, Comments) VALUES ({GetIDCompany()}, '{_inc}', '{_dateinc}', '{comments}')");
                            CurrentModel.MoneyCompany += _inc;
                            CurrentModel.GetValues("CI.ID,CI.Income,CI.Date_Income,CI.Comments", "Company_Income", $"AS CI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID WHERE Comp.Name = '{PresenterModel.Model.NameCompany}' ORDER BY CI.ID DESC LIMIT 5");
                            CurrentModel.AddValue($"INSERT INTO Money_Company (ID_Company,Account) VALUES ({GetIDCompany()}, '{CurrentModel.MoneyCompany}')");
                        }
                        break;
                    case 1:
                        {
                            AddSpecValue($"INSERT INTO Activity_Income (ID_Company, ID_Activity, Activity_Income, Date_Income, Comments) VALUES ({GetIDCompany()}, {GetIDActive()}, '{_inc}', '{_dateinc}', '{comments}')");
                            CurrentModel.ActivityMoney += _inc;
                            CurrentModel.GetValues("AI.ID,AI.Activity_Income,AI.Date_Income,AI.Comments", "Activity_Income", $"AS AI JOIN Companies AS C ON AI.ID_Company = C.ID JOIN Company_Activity AS CA ON AI.ID_Activity = CA.ID WHERE CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' AND C.Name = '{CurrentModel.NameCompany}' ORDER BY AI.ID DESC LIMIT 5");
                            CurrentModel.AddValue($"INSERT INTO Activity_Money (ID_Company,ID_Activity,Account_Activity) VALUES ({GetIDCompany()}, {GetIDActive()}, '{CurrentModel.ActivityMoney}')");
                        }
                        break;
                }
            }
            else
            {
                Message?.Invoke(this, "Неверно введен доход");
            }
        }

        private string GetComment(string text)
        {
            string comment = text;

            if (string.IsNullOrEmpty(comment))
            {
                if (_dialogService.YesOrNo("Вы точно желаете не писать комментарий?", "Вопрос"))
                {
                    comment = "Без комментария";
                }
            }

            return comment;
        }

        private int GetIDCompany()
        {
            return CurrentModel.SpecValInt($"SELECT ID FROM Companies WHERE Name = '{CurrentModel.NameCompany}'");
        }

        private int GetIDActive()
        {
            return CurrentModel.SpecValInt($"SELECT CA.ID FROM Company_Activity AS CA JOIN Companies AS C ON CA.ID_Company = C.ID WHERE CA.ID_Company = {GetIDCompany()} AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}'");
        }

        public RelayCommand AddAccount
        {
            get
            {
                return _addAccount ??
                    (_addAccount = new RelayCommand(obj =>
                    {
                        int temp = 0;
                        
                        if (int.TryParse(InputAccount, out temp))
                        {
                            if (temp >= 0)
                            {
                                switch (_addVal)
                                {
                                    case 0:
                                        {
                                            AddSpecValue($"INSERT INTO Money_Company (ID_Company,Account) VALUES ((SELECT ID FROM Companies WHERE Name = '{CurrentModel.NameCompany}'),'{temp}') ");
                                            CurrentModel.MoneyCompany = temp;
                                        }
                                        break;
                                    case 1:
                                        {
                                            AddSpecValue($"INSERT INTO Activity_Money (ID_Company,ID_Activity,Account_Activity) VALUES ((SELECT ID FROM Companies WHERE Name = '{CurrentModel.NameCompany}'), (SELECT ID FROM Company_Activity WHERE Name_of_Activity = '{CurrentModel.CompanyActivity}'), '{temp}')");
                                            CurrentModel.ActivityMoney = temp;
                                        }
                                        break;
                                }

                                InputAccount = null;
                            }
                            else
                            {
                                Message?.Invoke(this, "Вряд-ли счет будет в минусе...");
                            }
                        }
                        else
                        {
                            Message?.Invoke(this, "Кажется вы ввели неверное число. Повторите попытку");
                        }
                    },
                    (obj) => !string.IsNullOrWhiteSpace(InputAccount) && !_modelNull && _addVal != 3));
            }
        }

        private void AddSpecValue(string Query)
        {
            Message?.Invoke(this, _currentModel.AddValue(Query));
        }

        public RelayCommand AddValueComp
        {
            get
            {
                return _addValueComp ??
                    (_addValueComp = new RelayCommand(obj =>
                    {
                        if (CurrentModel.NameCompany != null || CurrentModel.NameCompany != string.Empty)
                            _addVal = 0;
                    },
                    (obj) => !_modelNull && CurrentModel.NameCompany != null));
            }
        }

        public RelayCommand AddValueAct
        {
            get
            {
                return _addValueAct ??
                    (_addValueAct = new RelayCommand(obj =>
                    {
                        if (CurrentModel.NameCompany != null || CurrentModel.NameCompany != string.Empty && CurrentModel.CompanyActivity != null || CurrentModel.CompanyActivity != string.Empty)
                            _addVal = 1;
                        else
                            Message?.Invoke(this, "Ошибка, ");
                    },
                    (obj) => !_modelNull && CurrentModel.CompanyActivity != null && CurrentModel.NameCompany != null));
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

        private RelayCommand? _openAddAccount;
        private RelayCommand? _openAddIncome;
        private RelayCommand? _openAddExpense;

        public RelayCommand OpenAddAccount
        {
            get
            {
                return _openAddAccount ??
                    (_openAddAccount = new RelayCommand(obj =>
                    {
                        OpenBorders?.Invoke(this, 0);
                    },
                    (obj) => !_modelNull && _addVal != 3));
            }
        }

        public RelayCommand OpenAddIncome
        {
            get
            {
                return _openAddIncome ??
                    (_openAddIncome = new RelayCommand(obj =>
                    {
                        OpenBorders?.Invoke(this, 1);
                    },
                    (obj) => !_modelNull && _addVal != 3));
            }
        }

        public RelayCommand OpenAddExpense
        {
            get
            {
                return _openAddExpense ??
                    (_openAddExpense = new RelayCommand(obj =>
                    {
                        OpenBorders?.Invoke(this, 2);
                    },
                    (obj) => !_modelNull && _addVal != 3));
            }
        }

        public VMControlExpInc(Model.Model model, IDialogService dialog) 
        {
            StartSettring(model);

            this._dialogService = dialog;
        }

        private void StartSettring(Model.Model model)
        {
            if (model == null)
            {
                _currentModel = new Model.Model();
                PresenterModel.Model = _currentModel;
            }
            else
            {
                _currentModel = model;

                if (_currentModel.NameCompany != null)
                {
                    _textAddComp = $"Добавлять значение для компании {_currentModel.NameCompany}";

                    if (_currentModel.CompanyActivity != null)
                    {
                        _textAddAct = $"Добавлять значение для активности {_currentModel.CompanyActivity}";
                    }
                    else
                    {
                        _textAddAct = "Увы, активность не выбрана";
                    }
                }
                else
                {
                    _textAddComp = "Увы, компания не выбрана";
                }
            }
        }
    }
}
