using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using System.Windows.Markup.Localizer;

namespace Absolut.Model
{
    public class Model : INotifyPropertyChanged
    {
        private const string _pathToDB = "Data Source = Finance.db";

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private string? _nameCompany;
        private int _moneyCompany;
        private string? _companyActivity;
        private int _activityMoney;
        private string? _status;

        private ObservableCollection<string> _companies;
        private ObservableCollection<string> _activities;
        private Dictionary<int, string> _historyIncome;
        private Dictionary<int, string> _historyExpense;
        private Dictionary<int, string> _historyIncAct;
        private Dictionary<int, string> _historyExpAct;
        private Dictionary<int, string> _testHistory;

        public Dictionary<int, string> TestHistory
        {
            get => _testHistory;
            set
            {
                _testHistory = value;
                OnPropertyChanged("TestHistory");
            }
        }

        public string? Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public string? NameCompany
        {
            get
            {
                return _nameCompany;
            }
            set
            {
                _nameCompany = value;
                OnPropertyChanged("NameCompany");
            }
        }
        public int MoneyCompany
        {
            get
            {
                return _moneyCompany;
            }
            set
            {
                _moneyCompany = value;
                OnPropertyChanged("MoneyCompany");
            }
        }
        public string? CompanyActivity
        {
            get
            {
                return _companyActivity;
            }
            set
            {
                _companyActivity = value;
                OnPropertyChanged("CompanyActivity");
            }
        }
        public int ActivityMoney
        {
            get
            {
                return _activityMoney;
            }
            set
            {
                _activityMoney = value;
                OnPropertyChanged("ActivityMoney");
            }
        }

        public ObservableCollection<string> Companies
        {
            get
            {
                return _companies;
            }
            set
            {
                _companies = value;
                OnPropertyChanged("Companies");
            }
        }

        public ObservableCollection<string> Activities
        {
            get
            {
                return _activities;
            }
            set
            {
                _activities = value;
                OnPropertyChanged("Activities");
            }
        }

        public Dictionary<int, string> HistoryIncome
        {
            get
            {
                return _historyIncome;
            }
            set
            {
                _historyIncome = value;
                OnPropertyChanged("HistoryIncome");
            }
        }

        public Dictionary<int, string> HistoryExpense
        {
            get
            {
                return _historyExpense;
            }
            set
            {
                _historyExpense = value;
                OnPropertyChanged("HistoryExpense");
            }
        }

        public Dictionary<int, string> HistoryIncAct
        {
            get
            {
                return _historyIncAct;
            }
            set
            {
                _historyIncAct = value;
                OnPropertyChanged("HistoryIncAct");
            }
        }

        public Dictionary<int, string> HistoryExpAct
        {
            get
            {
                return _historyExpAct;
            }
            set
            {
                _historyExpAct = value;
                OnPropertyChanged("HistoryExpAct");
            }
        }

        public int SpecValInt(string SQLQuery)
        {
            using (var connection = new SqliteConnection(_pathToDB))
            {
                connection.Open();

                using (var command = new SqliteCommand(SQLQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        internal string SpecValString(string SQLQuery)
        {
            using (var connection = new SqliteConnection(_pathToDB))
            {
                connection.Open();

                using (var command = new SqliteCommand(SQLQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
        }

        internal void GetSpecValue(string nameColumn, string nameTable, string someBrances = "")
        {
            string SQLQuery = @$"SELECT {nameColumn} FROM {nameTable} {someBrances};";

            switch (nameColumn, nameTable)
            {
                case ("Name", "Companies"):
                    {
                        NameCompany = SpecValString(SQLQuery);
                    }
                    break;
                case ("Name_of_Activity", "Company_Activity"):
                    {
                        CompanyActivity = SpecValString(SQLQuery);
                    }
                    break;
                case ("Account", "Money_Company"):
                    {
                        MoneyCompany = SpecValInt(SQLQuery);
                    }
                    break;
                case ("Account_Activity", "Activity_Money"):
                    {
                        ActivityMoney = SpecValInt(SQLQuery);
                    }
                    break;
            }
        }

        public void GetValues(string nameColumn, string nameTable, string someBrances = "")
        {
            string SQLQuery = @$"SELECT {nameColumn} FROM {nameTable} {someBrances};";

            switch (nameColumn,nameTable)
            {
                case ("Name","Companies"):
                    {
                        Companies = ValuesNames(SQLQuery);
                    }
                    break;
                case ("Name_of_Activity", "Company_Activity"):
                    {
                        Activities = ValuesNames(SQLQuery);
                    }
                    break;
                case ("CI.ID,CI.Income,CI.Date_Income,CI.Comments", "Company_Income"):
                    {
                        HistoryIncome = ValuesHistory(SQLQuery, 0);
                    }
                    break;
                case ("CE.ID,CE.Expense,CE.Date_Expense,CE.Comments", "Company_Expense"):
                    {
                        HistoryExpense = ValuesHistory(SQLQuery, 1);
                    }
                    break;
                case ("AI.ID,AI.Activity_Income,AI.Date_Income,AI.Comments", "Activity_Income"):
                    {
                        HistoryIncAct = ValuesHistory(SQLQuery, 0);
                    }
                    break;
                case ("AE.ID,AE.Activity_Expense,AE.Date_Expense,AE.Comments", "Activity_Expense"):
                    {
                        HistoryExpAct = ValuesHistory(SQLQuery, 1);
                    }
                    break;
            }
        }

        private ObservableCollection<string> ValuesNames(string SQLQuery)
        {
            using (var connection = new SqliteConnection(_pathToDB))
            {
                connection.Open();

                using (var command = new SqliteCommand(SQLQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        ObservableCollection<string> temp = new ObservableCollection<string>();

                        while (reader.Read())
                        {
                            temp.Add($"{reader.GetString(0)}");
                        }

                        return temp;
                    }
                }
            }
        }

        internal Dictionary<int, string> ValuesHistory(string SQLQuery, int index = 0)
        {
            using (var connection = new SqliteConnection(_pathToDB))
            {
                connection.Open();

                using (var command = new SqliteCommand(SQLQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Dictionary<int, string> temp = new Dictionary<int, string>();

                        switch (index)
                        {
                            case 0:
                                {
                                    while (reader.Read())
                                    {
                                        temp.Add(reader.GetInt32(0), $"+{reader.GetInt32(1)}$ | {reader.GetString(2)} | {reader.GetString(3)}");
                                    }
                                }
                                break;
                            case 1:
                                {
                                    while (reader.Read())
                                    {
                                        temp.Add(reader.GetInt32(0), $"-{reader.GetInt32(1)}$ | {reader.GetString(2)} | {reader.GetString(3)}");
                                    }
                                }
                                break;
                        }

                        return temp;
                    }
                }
            }
        }

        private int SQLQuery(string SQLQuery)
        {
            using (var connection = new SqliteConnection(_pathToDB))
            {
                connection.Open();

                using (var command = new SqliteCommand(SQLQuery, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public string AddValue(string query)
        {
            if (SQLQuery(query) > 0)
            {
                return "Успешно добавлено";
            }
            else
            {
                return "Не удалось добавить...";
            }
        }

        public string DeleteValue(string query)
        {
            if (SQLQuery(query) > 0)
            {
                return "Успешно удалено";
            }
            else
            {
                return "Не удалось удалить...";
            }
        }

        public string EditValue(string query)
        {
            if (SQLQuery(query) > 0)
            {
                return "Успешно изменено";
            }
            else
            {
                return "Не удалось изменить...";
            }
        }
    }
}
