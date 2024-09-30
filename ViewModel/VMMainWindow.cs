using Absolut.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Absolut.ViewModel
{
    class VMMainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private string? _login;
        private SecureString? _password;
        private Model.Model? _model;

        public Model.Model? Currentmodel
        {
            get => _model;
            set => _model = value;
        }

        public string? Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public SecureString? Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private RelayCommand? _enter;
        public event EventHandler<string>? Message;
        public event EventHandler<bool>? Status;

        public RelayCommand? Enter 
        { 
            get
            {
                return _enter ??
                    (_enter = new RelayCommand(obj =>
                    {
                        var password = "";

                        if (Password != null)
                            password = ConvertSecureStringToString(Password);

                        switch (Login, password)
                        {
                            case ("userbde", "Lou35Rango"):
                                {
                                    Currentmodel.Status = "user";
                                    Message?.Invoke(this, "Вы успешно авторизовались как пользователь");
                                    Status?.Invoke(this, true);
                                }
                                break;
                            case ("admin", "123"):
                                {
                                    Currentmodel.Status = "admin";
                                    Message?.Invoke(this, "Вы успешно авторизовались как администратор");
                                    Status?.Invoke(this, true);
                                }
                                break;
                            default:
                                {
                                    Message?.Invoke(this, "Неверный логин или пароль");
                                }
                                break;
                        }
                    }));
            }
        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public VMMainWindow()
        {
            Currentmodel = new Model.Model();
            PresenterModel.Model = Currentmodel;
        }
    }
}
