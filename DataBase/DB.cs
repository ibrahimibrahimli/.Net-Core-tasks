using SignInAndSignUp.Entity;
using SignInAndSignUp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignInAndSignUp.DataBase
{
    public class DB
    {
        private static DB _dB;
        public static DB DataBase
        {
            get
            {
                if (_dB == null)
                    _dB = new DB();
                return _dB;
            }
        }
        private DB()
        {

        }

        public List<User> users = new List<User>()
        {
            new User{ID = 1, Email = "Admin@gmail.com", Password = "admin", UserName = "admin" }
        };
    }
}