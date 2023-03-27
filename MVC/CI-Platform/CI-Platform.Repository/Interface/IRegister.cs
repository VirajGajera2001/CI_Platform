using CI_Platform.Entities.DataModels;
using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IRegister
    {
        public User IsExists(string Email);
        public void AddData(User user);
        public PasswordReset PassReset(string email, string token);
        public User PasswordReset(Resetpass respa);
        public User Pass(Resetpass respa);
        public PasswordReset reset(Resetpass respa);
        public bool AddPass(Resetpass respa);
        public User loguser(LoginModel model);
        public User forget(ForgetModel model);

        public int passres(PasswordReset model);
    }
}
