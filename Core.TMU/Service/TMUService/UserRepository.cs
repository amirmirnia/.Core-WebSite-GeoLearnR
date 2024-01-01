using Core.TMU.Genarator;
using Core.TMU.Security;
using Core.TMU.Service.ITMUService;
using Data.TMU.Context;
using Data.TMU.Model;
using Data.TMU.Model.Wallet;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.TMUService
{
    public class UserRepository : IUser
    {
        private ContextTMU _db;
        private IPermision _Permition;
        public UserRepository(ContextTMU db,IPermision Permition)
        {
            _db = db;
            _Permition = Permition;
        }
        public bool ActiveUser(string Activecode)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(p => p.ActiveCode == Activecode);
                user.IsActive = true;
                user.ActiveCode = Code.UniqCode();
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

        }

        public bool DeleteUser(string IdCode)
        {
            var user = _db.Users.First(p => p.IdCode == IdCode);
            if (user != null)
            {
                user.IsDelete = true;
                _db.Update(user);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public void SendActiveEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            _db.Update(user);
            _db.SaveChanges();
        }

        public bool ComparePassword(string pass1, string pass2)
        {
            if (pass1 == pass2)
            {
                return true;
            }
            return false;
        }

        public bool IsExistEmail(string email)
        {

            return _db.Users.Any(n => n.Email == email);
        }

        public bool IsExistMobile(string mobile)
        {
            return _db.Users.Any(n => n.Mobile == mobile);
        }

        public User FindUser(string IdCode)
        {
            return _db.Users.First(o => o.IdCode == IdCode);

        }

        public User LoginUser(LoginViewModel login)
        {
            string pass = login.Password.EncodePasswordMd5();
            //string email = (login.Email).FixEmail();
            var user = _db.Users.SingleOrDefault(o => o.IdCode == login.CodeId);
            //var user = _db.Users.SingleOrDefault(o => o.IdCode == login.CodeId && o.Password == pass);
            return user;
        }

        public UpdateregisterViewModel FindUserForUpdate(string IdCode)
        {
            return _db.Users.Where(o => o.IdCode == IdCode).Select(u => new UpdateregisterViewModel()
            {
                Email = u.Email,
                FullName = u.FullName,
                IdCode = u.IdCode,
                Mobile = u.Mobile,
                UserAvatar = u.UserAvatar,
                post=u.post,
                level=u.level,
                CodePerseneli=u.CodePerseneli

            }).Single();
        }

        public ListUserViewModel GetAllUser(int page = 1, string username = null, string email = null, string CodeId = null, string tag = null, int take = 0)
        {
            IQueryable<User> result = _db.Users;
            if (!string.IsNullOrEmpty(tag)|| !string.IsNullOrEmpty(username))
            {
                result = result.Where(p => p.FullName.Contains(tag) || p.FullName.Contains(username));
            }
            if (!string.IsNullOrEmpty(CodeId))
            {
                result = result.Where(p => p.IdCode.Contains(CodeId));
            }
            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(p => p.Email.Contains(email));
            }

            result = result.Where(p => p.IsDelete==false);


            int skip = (page - 1) * take;

            var count = result.Count();
            return new ListUserViewModel()
            {
                Listuser = result.OrderBy(p => p.post).Skip(skip).Take(take).ToList(),
                CountPage = (count/take)+1,
                IdPage = page            
            };
        }

        public bool DeleteListUser(List<string> listuserIdCode)
        {
            if (listuserIdCode != null)
            {
                foreach (var item in listuserIdCode)
                {
                    DeleteUser(item);
                }
                return true;
            }
            return false;
        }

        public int getPkId(string IdCode)
        {
            return _db.Users.First(p => p.IdCode == IdCode).Id;
        }

        public string Getname(string IdCode)
        {
            return _db.Users.First(p => p.IdCode == IdCode && p.IsActive==true).FullName;
        }

        public bool ActiveUserIdCode(string Idcode)
        {
            var user = _db.Users.FirstOrDefault(p => p.IdCode == Idcode);
            try
            {
                if (user.IsActive==false)
                {
                    user.IsActive = true;
                }
                else
                {
                    user.IsActive = false;
                }
                user.ActiveCode = Code.UniqCode();
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public User FindUser(int Id)
        {
            return _db.Users.First(o => o.Id == Id);

        }
        public void InsertLevelUser(string level, string idcode)
        {
            var user = _db.Users.SingleOrDefault(u => u.IdCode == idcode);
            user.level = level;         
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public List<SelectListItem> finduserwhitpostandlevel(string? post, string level)
        {
            IQueryable<User> result = _db.Users;
            
            if (!string.IsNullOrEmpty(post))
            {
                result = result.Where(p => p.post.Contains(post));
            }
            if (!string.IsNullOrEmpty(level))
            {
                result = result.Where(p => p.level.Contains(level));
            }
            return result.Select(p => new SelectListItem()
            {    
                Text = p.FullName,
                Value = p.IdCode.ToString(),
            }).ToList();
        }

        public List<User> GetUserWhitLevel(string level)
        {
            return _db.Users.Where(p => p.level.Contains(level)).ToList();
        }

        public bool FindUserwhitcodeperseneli(string codeperseneli)
        {
            var user = _db.Users.FirstOrDefault(p => p.CodePerseneli == codeperseneli);
            if (user!=null)
            {
                return true;
            }
            return false;
        }

        public bool IsExistPost(int post)
        {
            var user = _db.Users.FirstOrDefault(p => p.post == (post).ToString());
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public List<UserViewModel> finduserwhitpostandlevel2(string? post, string level)
        {
            IQueryable<User> result = _db.Users;

            if (!string.IsNullOrEmpty(post))
            {
                result = result.Where(p => p.post.Contains(post));
            }
            if (!string.IsNullOrEmpty(level))
            {
                result = result.Where(p => p.level.Contains(level));
            }
            return result.Select(p => new UserViewModel()
            {
                idcode=p.IdCode

            }).ToList();
        }

        public bool IsUpperLevelUserAutorize(string IdCode)
        {
            var user = FindUser(IdCode);
            if (user!=null)
            {
                var countuseruperr = _db.Users.Where(p => p.level == (int.Parse(user.level) + 1).ToString()).Count();
                if (countuseruperr>0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool IsDownLevelUserAutorize(string IdCode)
        {
            var user = FindUser(IdCode);
            if (user != null)
            {
                var countuseruperr = _db.Users.Where(p => p.level == (int.Parse(user.level) - 1).ToString()).Count();
                if (countuseruperr > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public int BalanceWalletUser(string idcode)
        {
            var enter = _db.Wallet.Where(w => w.UserId == GetiduserWhitidcode(idcode) && w.TypeId == 1 && w.IsPay==true).Select(w=>w.Amount);
            var exit = _db.Wallet.Where(w => w.UserId == GetiduserWhitidcode(idcode) && w.TypeId == 2).Select(w => w.Amount);
            return (enter.Sum() - exit.Sum());
        }

        public int GetiduserWhitidcode(string idcode)
        {
            return _db.Users.FirstOrDefault(u => u.IdCode == idcode).Id;
        }

        public List<Wallet> GetListWallet(string idcode)
        {
            return _db.Wallet.Where(w => w.UserId == GetiduserWhitidcode(idcode)).ToList();
        }

        public void AddWallet(int Amount,bool Ispay=false,string idcode=null)
        {
            Wallet W = new Wallet()
            {
                Amount=Amount,
                DetaTime=DateTime.Now,
                Description="واریز",
                IsPay=Ispay,
                TypeId=1,
                UserId= GetiduserWhitidcode(idcode)
            };
            _db.Wallet.Add(W);
            _db.SaveChanges();
        }
    }
}
