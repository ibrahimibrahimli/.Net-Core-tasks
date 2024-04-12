using SignInAndSignUp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignInAndSignUp.Controls.Abstract
{
    public interface IBaseOperation<T> where T : BaseEntity, new()
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
        List<T> GetAll();
        T GetById(int id);
    }
}