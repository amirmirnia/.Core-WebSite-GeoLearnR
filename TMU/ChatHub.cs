using Core.TMU.Service.ITMUService;
using Data.TMU.User;
using Microsoft.AspNetCore.SignalR;


namespace TMU
{
    public class ChatHub :Hub , IChatHub
    {
        private IGeneric<Useronline> _gUseronline;
        private IUser _user;
        public ChatHub(IGeneric<Useronline> gUseronline, IUser user)
        {
            _gUseronline = gUseronline;
            _user = user;
        }

        public override async Task OnConnectedAsync()
        {

            await Clients.Caller.SendAsync("StartConnection", "ارتباط زنده با سرور");
            await base.OnConnectedAsync();
        }

        public async Task NotifyDisconnect( string id)
        {
            //await Clients.All.NotifyDisconnect(name, id);
            var user = _gUseronline.GetEntity(p => p.Idcode == id && p.IsOnline == true);
            if (user != null)
            {
                user.IsOnline = false;
                _gUseronline.Update(user);
            }
        }
        //public override async Task OnDisconnectedAsync(Exception? exception)
        //{
        //    await Clients.Caller.SendAsync("CloseConnection", "ارتباط قطع شد");
        //    await base.OnDisconnectedAsync(exception);
        //}
        public async Task Stopconnection(string idcode)
        {
            var user = _gUseronline.GetEntity(p => p.Idcode == idcode && p.IsOnline == true);
            if (user!=null)
            {
                _gUseronline.Delete(user);
            }

        }
        public  async Task  sendMessage(string text,string name)
        {
            await Clients.All.SendAsync("sendmessage", text, name, DateTime.Now.ToString("HH:mm:ss"));
        }
        
        public async Task senduser(string idcode)
        {
            var user = _gUseronline.GetEntity(p => p.Idcode == idcode && p.IsOnline == true);


            if (user == null)
            {
                Useronline Useronline = new Useronline()
                {
                    Idcode = idcode,
                    name = _user.FindUser(idcode).FullName,
                    IsOnline = true,
                    DetaNews = DateTime.Now
                };
                _gUseronline.Insert(Useronline);

                await Clients.All.SendAsync("ReciveUserOnline", Useronline.name);
            }
            else
            {
                _gUseronline.Delete(user);
                Useronline Useronline = new Useronline()
                {
                    Idcode = idcode,
                    name = _user.FindUser(idcode).FullName,
                    IsOnline = true,
                    DetaNews = DateTime.Now
                };
                _gUseronline.Insert(Useronline);
            }


        }
    }
}
