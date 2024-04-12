using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignInAndSignUp.Entity
{
    public  class BaseEntity
    {

        public static int _id = 1;
        public int ID { get; set; }
        public BaseEntity()
        {
            ID = ++_id;
        }
    }
}