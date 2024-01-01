namespace TMU
{
    public interface IChatHub
    {

        Task NotifyDisconnect(string id);
        Task Stopconnection(string idcode);
        Task sendMessage(string text, string name);

        Task senduser(string idcode);

    }
}
