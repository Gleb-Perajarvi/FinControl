using Absolut.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace Absolut.ViewModel
{
    internal class VMHistoryIncome : INotifyPropertyChanged
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
        public event EventHandler<bool> OpenCompanies;

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

        public DateTime? DateInc
        {
            get => _dateInc;
            set
            {
                _dateInc = value;
                OnPropertyChanged("DateInc");
            }
        }

        public string ComInc
        {
            get => _comInc;
            set
            {
                _comInc = value;
                OnPropertyChanged("ComInc");
            }
        }

        private RelayCommand? _setCompany;
        private RelayCommand? _deleteItemHistory;
        private RelayCommand? _editItemHistory;
        private RelayCommand? _updateInfo;

        public RelayCommand SetCompany
        {
            get
            {
                return _setCompany ??
                    (_setCompany = new RelayCommand(obj =>
                    {
                        OpenCompanies?.Invoke(this, true);
                    },
                    (obj) => CurrentModel.NameCompany == null));
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
                    (obj) => !string.IsNullOrWhiteSpace(Income) && DateInc != null && SetKey != -1));
            }
        }

        private void UpdateIncome()
        {
            if (int.TryParse(Income, out _inco) && _inco > 0)
            {
                _dateinc = DateInc.Value.ToString("dd.MM.yyyy HH:mm");
                _cominc = GetComment(ComInc);

                int result = _inco - CurrentModel.SpecValInt($"SELECT Income FROM Company_Income WHERE ID = {SetKey}");

                Message?.Invoke(this, CurrentModel.EditValue($"UPDATE Company_Income SET Income = {_inco}, Date_Income = '{_dateinc}', Comments = '{_cominc}' WHERE ID = {SetKey}"));
                CurrentModel.MoneyCompany += result;
                CurrentModel.GetValues("CI.ID,CI.Income,CI.Date_Income,CI.Comments", "Company_Income", $"AS CI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID WHERE Comp.Name = '{PresenterModel.Model.NameCompany}' ORDER BY CI.ID DESC LIMIT 5");
                CurrentModel.AddValue($"INSERT INTO Money_Company (ID_Company,Account) VALUES ({GetIDCompany()}, '{CurrentModel.MoneyCompany}')");
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
                        CurrentModel.HistoryIncome.Remove(SetKey);
                        AllHistoryIncome.Remove(SetKey);
                        Message?.Invoke(this, CurrentModel.DeleteValue($"DELETE FROM Company_Income WHERE ID = {SetKey}"));
                        GetHistory();
                        CurrentModel.GetValues("CI.ID,CI.Income,CI.Date_Income,CI.Comments", "Company_Income", $"AS CI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID WHERE Comp.Name = '{PresenterModel.Model.NameCompany}' ORDER BY CI.ID DESC LIMIT 5");
                    },
                    (obj) => CurrentModel.Status == "admin" && SetKey != -1 && CurrentModel.HistoryIncome.Count > 0));
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
                    DateInc = DateTime.Parse(parts[1].Trim());
                    ComInc = parts[2].Trim();
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

        public VMHistoryIncome(Model.Model model, IDialogService dialog)
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
            GetText(CurrentModel.NameCompany);
            GetHistory();
        }

        private void GetHistory()
        {
            string SQLQuery = $"SELECT CI.ID,CI.Income,CI.Date_Income,CI.Comments FROM Company_Income AS CI JOIN Companies AS Comp ON CI.ID_Company = Comp.ID WHERE Comp.Name = '{CurrentModel.NameCompany}' ORDER BY CI.ID DESC";
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
                TextInfo = "Похоже компания не выбрана. Можете ее выбрать по кнопке справа";
            }
        }
    }
}

// Версия кода для парса в инт int.Parse(CurrentModel.HistoryIncome[SetKey].Split('|')[0].TrimStart('+', '-').Trim());