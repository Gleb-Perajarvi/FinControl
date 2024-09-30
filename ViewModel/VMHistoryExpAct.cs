using Absolut.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Absolut.ViewModel
{
    class VMHistoryExpAct : INotifyPropertyChanged
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
        public event EventHandler<byte> OpenCompanies;

        private IDialogService _dialogService;
        private Model.Model _currentModel;
        private int _setKey = -1;
        private string _textInfo;

        private Dictionary<int, string> _allHistoryExpense;

        public Dictionary<int, string> AllHistoryExpense
        {
            get => _allHistoryExpense;
            set
            {
                _allHistoryExpense = value;
                OnPropertyChanged("AllHistoryExpense");
            }
        }

        private string _exp;
        private DateTime? _dateExp;
        private string _comExp;
        private int _expe;
        private string _dateexp;
        private string _comexp;

        public string Expense
        {
            get => _exp;
            set
            {
                _exp = value;
                OnPropertyChanged("Expense");
            }
        }

        public DateTime? DateExpense
        {
            get => _dateExp;
            set
            {
                _dateExp = value;
                OnPropertyChanged("DateExpense");
            }
        }

        public string CommentExpense
        {
            get => _comExp;
            set
            {
                _comExp = value;
                OnPropertyChanged("CommentExpense");
            }
        }

        private RelayCommand? _setCompany;
        private RelayCommand? _deleteItemHistory;
        private RelayCommand? _updateInfo;

        public RelayCommand SetActivity
        {
            get
            {
                return _setCompany ??
                    (_setCompany = new RelayCommand(obj =>
                    {
                        if (CurrentModel.CompanyActivity == null && CurrentModel.Activities != null && CurrentModel.NameCompany != null)
                        {
                            OpenCompanies?.Invoke(this, 0);
                        }
                        else if (CurrentModel.NameCompany == null)
                        {
                            OpenCompanies?.Invoke(this, 1);
                        }
                    },
                    (obj) => CurrentModel.CompanyActivity == null || CurrentModel.NameCompany == null));
            }
        }

        public RelayCommand UpdateInfo
        {
            get
            {
                return _updateInfo ??
                    (_updateInfo = new RelayCommand(obj =>
                    {
                        UpdateIncome();
                    },
                    (obj) => !string.IsNullOrWhiteSpace(Expense) && DateExpense != null && SetKey != -1));
            }
        }

        private void UpdateIncome()
        {
            if (int.TryParse(Expense, out _expe) && _expe > 0)
            {
                _dateexp = DateExpense.Value.ToString("dd.MM.yyyy HH:mm");
                _comexp = GetComment(CommentExpense);

                int result = CurrentModel.SpecValInt($"SELECT Activity_Expense FROM Activity_Expense WHERE ID = {SetKey}") - _expe;

                Message?.Invoke(this, CurrentModel.EditValue($"UPDATE Activity_Expense SET Activity_Expense = {_expe}, Date_Expense = '{_dateexp}', Comments = '{_comexp}' WHERE ID = {SetKey}"));
                CurrentModel.ActivityMoney += result;
                CurrentModel.GetValues("AE.ID,AE.Activity_Expense,AE.Date_Expense,AE.Comments", "Activity_Expense", $"AS AE JOIN Companies AS Comp ON AE.ID_Company = Comp.ID JOIN Company_Activity AS CA ON AE.ID_Activity = CA.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' ORDER BY AE.ID DESC LIMIT 5");
                CurrentModel.AddValue($"INSERT INTO Activity_Money (ID_Company,ID_Activity,Account_Activity) VALUES ({GetIDCompany()}, {GetIDActive()}, {CurrentModel.ActivityMoney})");
                GetHistory();
            }
            else
            {
                Message?.Invoke(this, "Неверно введен расход");
            }
        }

        private int GetIDCompany()
        {
            return CurrentModel.SpecValInt($"SELECT ID FROM Companies WHERE Name = '{CurrentModel.NameCompany}'");
        }

        private int GetIDActive()
        {
            return CurrentModel.SpecValInt($"SELECT CA.ID FROM Company_Activity AS CA JOIN Companies AS C ON CA.ID_Company = C.ID WHERE CA.ID_Company = {GetIDCompany()} AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}'");
        }

        private string GetComment(string text)
        {
            string comment = text;

            if (string.IsNullOrWhiteSpace(comment))
            {
                if (_dialogService.YesOrNo("Вы точно желаете не писать комментарий?", "Вопрос"))
                {
                    comment = "Без комментария";
                }
            }

            return comment;
        }

        public RelayCommand DeleteExpense
        {
            get
            {
                return _deleteItemHistory ??
                    (_deleteItemHistory = new RelayCommand(obj =>
                    {
                        CurrentModel.HistoryExpAct.Remove(SetKey);
                        AllHistoryExpense.Remove(SetKey);
                        Message?.Invoke(this, CurrentModel.DeleteValue($"DELETE FROM Activity_Expense WHERE ID = {SetKey}"));
                        GetHistory();
                        CurrentModel.GetValues("AE.ID,AE.Activity_Expense,AE.Date_Expense,AE.Comments", "Activity_Expense", $"AS AE JOIN Companies AS Comp ON AE.ID_Company = Comp.ID JOIN Company_Activity AS CA ON AE.ID_Activity = CA.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' ORDER BY AE.ID DESC LIMIT 5");
                    },
                    (obj) => CurrentModel.Status == "admin" && SetKey != -1 && CurrentModel.HistoryExpAct.Count > 0));
            }
        }

        public int SetKey
        {
            get => _setKey;
            set
            {
                _setKey = value;
                GetInfoToUpdate();
                OnPropertyChanged("SetKey");
            }
        }

        private void GetInfoToUpdate()
        {
            if (AllHistoryExpense.TryGetValue(SetKey, out string HistoryItem))
            {
                string[] parts = HistoryItem.Split('|');

                if (parts.Length == 3)
                {
                    Expense = GetNumber(parts[0].Trim());
                    DateExpense = DateTime.Parse(parts[1].Trim());
                    CommentExpense = parts[2].Trim();
                }
            }
        }

        private string GetNumber(string input)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(input);

            if (match.Success)
            {
                return match.Value;
            }

            return string.Empty;
        }

        public string TextInfo
        {
            get => _textInfo;
            set => _textInfo = value;
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

        public VMHistoryExpAct(Model.Model model, IDialogService dialog)
        {
            if (model == null)
            {
                CurrentModel = new Model.Model();
                PresenterModel.Model = CurrentModel;
            }
            else
            {
                CurrentModel = model;
            }

            StartSettings();

            this._dialogService = dialog;
        }

        private void StartSettings()
        {
            GetText(CurrentModel.CompanyActivity);
            GetHistory();
        }

        private void GetHistory()
        {
            string SQLQuery = $"SELECT AE.ID, AE.Activity_Expense, AE.Date_Expense, AE.Comments FROM Activity_Expense AS AE JOIN Companies AS Comp ON AE.ID_Company = Comp.ID JOIN Company_Activity AS CA ON AE.ID_Activity = CA.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' ORDER BY AE.ID DESC";
            AllHistoryExpense = CurrentModel.ValuesHistory(SQLQuery, 1);
        }

        private void GetText(string namecomp)
        {
            if (namecomp != null)
            {
                TextInfo = $"История расходов {namecomp}";
            }
            else
            {
                TextInfo = "Похоже активность не выбрана. Можете ее выбрать по кнопке справа";
            }
        }
    }
}
