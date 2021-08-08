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
            login.txtUser.Focus();
        }

        public override void SetDefaults()
        {
            UserName = "";
            Password = "";
        }


        private ICommand loginCommand;
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new RelayCommand(Login, ()=>true));
        private void Login()
        {
            int userId = userTable.CheckPassword(UserName, Password);
            if (userId == -1)
            {
                Password = "";
                login.txtPswd.Focus();
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

        private string password;
        public String Password
        {
            get => password;
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

  
    }
}
