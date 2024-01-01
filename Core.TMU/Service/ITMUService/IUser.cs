using Data.TMU.Model;
using Data.TMU.Model.Wallet;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMU.Service.ITMUService
{
    public interface IUser
    {
        void AddUser(User user);
        void InsertLevelUser(string level, string idcode);

        int getPkId(string IdCode);
        int GetiduserWhitidcode(string idcode);
        ListUserViewModel GetAllUser(int page = 1, string username = null, string email = null, string CodeId = null, string tag = null, int take = 0);
        User FindUser(string IdCode);
        bool FindUserwhitcodeperseneli(string codeperseneli);
        User FindUser(int Id);
        bool DeleteListUser(List<string> listuserIdCode);
        UpdateregisterViewModel FindUserForUpdate(string IdCode);
        bool DeleteUser(string IdCode);
        void UpdateUser(User user);
        bool ActiveUser(string Activecode);
        bool ActiveUserIdCode(string Idcode);
        void SendActiveEmail(string email);
        bool ComparePassword(string pass1, string pass2);
        bool IsExistEmail(string email);
        bool IsExistMobile(string mobile);
        bool IsExistPost(int post);
        bool IsUpperLevelUserAutorize(string IdCode);
        bool IsDownLevelUserAutorize(string IdCode);
        string Getname(string IdCode);
        List<User> GetUserWhitLevel(string level);
        #region wallet
        int BalanceWalletUser(string idcode);
        List<Wallet> GetListWallet(string idcode);
        void AddWallet(int Amount, bool Ispay = false, string idcode=null);
        #endregion

        List<SelectListItem> finduserwhitpostandlevel(string? post,string level);
        List<UserViewModel> finduserwhitpostandlevel2(string? post, string level);
        User LoginUser(LoginViewModel login);

    }
}
