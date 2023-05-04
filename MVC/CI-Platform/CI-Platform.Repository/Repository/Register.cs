using CI_Platform.Entities.DataModels;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
namespace CI_Platform.Repository.Repository
{
    public class Register:IRegister
    {
        private readonly CIdbcontext _objdb;
        public Register(CIdbcontext objdb)
        {
                _objdb= objdb;
        }
        public User IsExists(string Email)
        {
            var user=_objdb.Users.FirstOrDefault(u => u.Email == Email);
            return user;
        }
        public void AddData(User user)
        {
            user.Role = "user";
            user.Avatar = "../CI-Imgs/user1.png";
            _objdb.Users.Add(user);
            _objdb.SaveChanges();

        }
        public PasswordReset PassReset(string email, string token)
        {
            var passwordReset= _objdb.PasswordResets.FirstOrDefault(pr => pr.Email == email && pr.Token == token);
            return passwordReset;
        }

        public User PasswordReset(Resetpass respa)
        {
            var user=_objdb.Users.FirstOrDefault(u => u.Email == respa.Email);
            return user;
        }

        public User Pass(Resetpass respa)
        {
            var user = _objdb.Users.FirstOrDefault(u => u.Email == respa.Email);
            return user;
        }
        public PasswordReset reset(Resetpass respa)
        {
            var passwordReset = _objdb.PasswordResets.FirstOrDefault(pr => pr.Email == respa.Email && pr.Token == respa.Token);
            return passwordReset;
        }
        public bool AddPass(Resetpass respa)
        {
            var user = _objdb.Users.FirstOrDefault(u => u.Email == respa.Email);
            user.Password = respa.Password;
            _objdb.Users.Update(user);
            _objdb.SaveChanges();
            return true;
        }

        public User loguser(LoginModel model)
        {
            var user = _objdb.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            return user;
        }
        public User forget(ForgetModel model)
        {
            var user = _objdb.Users.FirstOrDefault(u => u.Email == model.Email);
            return user;
        }
        public int passres(PasswordReset model)
        {
            var passwordReset = new PasswordReset
            {
                Email = model.Email,
                Token = model.Token
            };
            _objdb.PasswordResets.Add(passwordReset);
            _objdb.SaveChanges();
            return 1;
        }
        public List<Banner> getbanners()
        {
            List<Banner> banners=_objdb.Banners.OrderBy(b=>b.SortOrder).ToList();
            return banners;
        }
    }
}
