using Absolut.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Absolut.ViewModel
{
    internal class VMCompanies : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private string _inputText;

        public string InputText
        {
            get
            {
                return _inputText;
            }
            set
            {
                _inputText = value;
                OnPropertyChanged("InputText");
            }
        }

        public event EventHandler<string> Message;
        public event EventHandler<bool> OpenWindowMainMenu;

        private Model.Model _model;
        private string? _selectedModel;
        private RelayCommand? _addValue;
        private RelayCommand? _removeValue;
        private RelayCommand? _chooseComp;
        private readonly IDialogService _dialogService;

        public Model.Model Model
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

        public string? SelectedModel
        {
            get
            {
                return _selectedModel;
            }
            set
            {
                _selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }

        public RelayCommand AddValue
        {
            get
            {
                return _addValue ??
                    (_addValue = new RelayCommand(obj =>
                    {
                        if (_dialogService.YesOrNo($"Вы точно желаете создать новую компанию с именем {InputText}?", "Предупреждение"))
                        {
                            Model.Companies.Insert(0, InputText);
                            _selectedModel = InputText;
                            Message?.Invoke(this, Model.AddValue($@"INSERT INTO Companies (Name) VALUES ('{InputText}')"));
                        }
                        else
                        {
                            return;
                        }
                    },
                    obj => !string.IsNullOrWhiteSpace(InputText)));
            }
        }

        public RelayCommand RemoveValue
        {
            get
            {
                return _removeValue ??
                    (_removeValue = new RelayCommand(selectedValue =>
                    {
                        if (_dialogService.YesOrNo($"Вы точно желаете удалить компанию с именем {selectedValue as string}?", "Предупреждение"))
                        {
                            string? model = selectedValue as string;
                            if (model != null)
                            {
                                Model.Companies.Remove(model);
                                Message?.Invoke(this, Model.DeleteValue($@"DELETE FROM Companies WHERE Name = '{selectedValue}'"));
                            }
                        }
                        else
                        {
                            return;
                        }
                    },
                    (selectedValue) => Model.Companies.Count > 0 && Model.Status == "admin" && !string.IsNullOrWhiteSpace(SelectedModel)));
            }
        }

        public RelayCommand ChooseComp
        {
            get
            {
                return _chooseComp ??
                    (_chooseComp = new RelayCommand(selValToMainMenu =>
                    {
                        string? nameComp = selValToMainMenu as string;
                        if (nameComp != null)
                        {
                            Model.NameCompany = nameComp;

                            OpenWindowMainMenu?.Invoke(this, true);
                        }
                    },
                    selValToMainMenu => Model.Companies.Count > 0 && SelectedModel != null));
            }
        }

        public VMCompanies(Model.Model model, IDialogService dialogService)
        {
            if (model == null)
            {
                _model = new Model.Model();
                PresenterModel.Model = _model;
            }
            else
            {
                _model = model;
                
            }

            _model.GetValues("Name", "Companies");
            this._dialogService = dialogService;
        }
    }
}
