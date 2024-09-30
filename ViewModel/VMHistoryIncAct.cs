using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Absolut.Model;

namespace Absolut.ViewModel
{
    internal class VMHistoryIncAct : INotifyPropertyChanged
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

        private Dictionary<int, string> _allHistoryIncome;

        public Dictionary<int, string> AllHistoryIncome
        {
            get => _allHistoryIncome;
            set
            {
                _allHistoryIncome = value;
                OnPropertyChanged("AllHistoryIncome");
            }
        }

        private string _inc;
        private DateTime? _dateInc;
        private string _comInc;
        private int _inco;
        private string _dateinc;
        private string _cominc;

        public string Income
        {
            get => _inc;
            set
            {
                _inc = value;
                OnPropertyChanged("Income");
            }
        }

        public DateTime? DateIncome
        {
            get => _dateInc;
            set
            {
                _dateInc = value;
                OnPropertyChanged("DateIncome");
            }
        }

        public string CommentIncome
        {
            get => _comInc;
            set
            {
                _comInc = value;
                OnPropertyChanged("CommentIncome");
            }
        }

        private RelayCommand? _setCompany;
        private RelayCommand? _deleteItemHistory;
        private RelayCommand? _editItemHistory;
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
                    (obj) => !string.IsNullOrWhiteSpace(Income) && DateIncome != null && SetKey != -1));
            }
        }

        private void UpdateIncome()
        {
            if (int.TryParse(Income, out _inco) && _inco > 0)
            {
                _dateinc = DateIncome.Value.ToString("dd.MM.yyyy HH:mm");
                _cominc = GetComment(CommentIncome);

                int result = _inco - CurrentModel.SpecValInt($"SELECT Activity_Income FROM Activity_Income WHERE ID = {SetKey}");

                Message?.Invoke(this, CurrentModel.EditValue($"UPDATE Activity_Income SET Activity_Income = {_inco}, Date_Income = '{_dateinc}', Comments = '{_cominc}' WHERE ID = {SetKey}"));
                CurrentModel.ActivityMoney += result;
                CurrentModel.GetValues("AI.ID,AI.Activity_Income,AI.Date_Income,AI.Comments", "Activity_Income", $"AS AI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID JOIN Company_Activity AS CA ON AI.ID_Activity = CA.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' ORDER BY AI.ID DESC LIMIT 5");
                CurrentModel.AddValue($"INSERT INTO Activity_Money (ID_Company,ID_Activity,Account_Activity) VALUES ({GetIDCompany()}, {GetIDActive()}, {CurrentModel.ActivityMoney})");
                GetHistory();
            }
            else
            {
                Message?.Invoke(this, "Неверно введен доход");
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

        public RelayCommand DeleteIncome
        {
            get
            {
                return _deleteItemHistory ??
                    (_deleteItemHistory = new RelayCommand(obj =>
                    {
                        CurrentModel.HistoryIncAct.Remove(SetKey);
                        AllHistoryIncome.Remove(SetKey);
                        Message?.Invoke(this, CurrentModel.DeleteValue($"DELETE FROM Company_Income WHERE ID = {SetKey}"));
                        GetHistory();
                        CurrentModel.GetValues("AI.ID,AI.Activity_Income,AI.Date_Income,AI.Comments", "Activity_Income", $"AS AI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID JOIN Company_Activity AS CA ON AI.ID_Activity = CA.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' ORDER BY AI.ID DESC LIMIT 5");
                    },
                    (obj) => CurrentModel.Status == "admin" && SetKey != -1 && CurrentModel.HistoryIncAct.Count > 0));
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
            if (AllHistoryIncome.TryGetValue(SetKey, out string HistoryItem))
            {
                string[] parts = HistoryItem.Split('|');

                if (parts.Length == 3)
                {
                    Income = GetNumber(parts[0].Trim());
                    DateIncome = DateTime.Parse(parts[1].Trim());
                    CommentIncome = parts[2].Trim();
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

        public VMHistoryIncAct(Model.Model model, IDialogService dialog)
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
            string SQLQuery = $"SELECT AI.ID, AI.Activity_Income, AI.Date_Income, AI.Comments FROM Activity_Income AS AI JOIN Companies AS Comp ON AI.ID_Company = Comp.ID JOIN Company_Activity AS CA ON AI.ID_Activity = CA.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' AND CA.Name_of_Activity = '{CurrentModel.CompanyActivity}' ORDER BY AI.ID DESC";
            AllHistoryIncome = CurrentModel.ValuesHistory(SQLQuery, 0);
        }

        private void GetText(string namecomp)
        {
            if (namecomp != null)
            {
                TextInfo = $"История доходов {namecomp}";
            }
            else
            {
                TextInfo = "Похоже активность не выбрана. Можете ее выбрать по кнопке справа";
            }
        }
    }
}
