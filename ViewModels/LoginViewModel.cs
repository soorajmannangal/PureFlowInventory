using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureFlow
{
    public class LoginViewModel : ViewModelBase
    {      
        private readonly UserTable userTable;
        LoginView login;
        public LoginViewModel(LoginView loginView) : base(null)
        {
            userTable = new UserTable();
            login = loginView;
            SetDefaults();
            login.passwordBox.KeyDown += PasswordBox_KeyDown;
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login();
            }
        }

        public override void SetDefaults()
        {
            UserName = "";
            login.passwordBox.Password = "";
            ErrorMessage = "";
            login.txtUser.Focus();
            login.errTxtBox.Visibility = System.Windows.Visibility.Hidden;
        }


        private ICommand loginCommand;
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new RelayCommand(Login, ()=>true));
        private void Login()
        {
            try
            {
                string pswd = login.passwordBox.Password;
                int userId = userTable.CheckPassword(UserName, pswd);
                if (userId == -1)
                {
                    login.passwordBox.Password = "";
                    login.passwordBox.Focus();
                }
                else
                {
                    bool isAdmin = userTable.CheckIsAdmin(userId);
                    UserInfo.GetInstance().SetUserID(userId, isAdmin);

                    var contextView = new MainWindow();
                    contextView.Show();
                    login.Close();
                }
            }
            catch(Exception e)
            {
                login.errTxtBox.Visibility = System.Windows.Visibility.Visible;
                ErrorMessage = e.Message;
            }
        }
       

        private string userName;
        public String UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string errorMessage;
        public String ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

     
  
    }
}
