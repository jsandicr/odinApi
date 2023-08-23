using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IChatModel
    {
        public List<Chat> GetChat();
        public Chat GetChatById(int id);
        public bool DeleteChat(int id);
        public Chat PutChat(Chat chat);
        public Chat PostChat(Chat chat);
    }
}