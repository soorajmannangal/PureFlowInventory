using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFlow
{
    public class UserInfo
    {
        private static UserInfo instance;
        private int userId;
        public int UserID => userId;

        private bool isAdmin;
        public bool IsAdmin => isAdmin;

        private UserInfo() 
        {
            SetUserID(1, true);
        }

        public static UserInfo GetInstance()
        {
            if(instance == null)
            {
                instance = new UserInfo();
            }
            return instance;
        }

        public void SetUserID(int userId, bool isAdmin)
        {
            this.isAdmin = isAdmin;
            this.userId = userId;
        }
    }
}
