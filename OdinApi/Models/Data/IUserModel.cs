using OdinApi.Models.Obj;

namespace OdinApi.Models.Data
{
    public interface IUserModel
    {
        public List<User> GetUsers();
        public User GetUserById(int id);
        public User PostUser(User user);
        public User PutUser(User user);
        public User DeleteUser(User user);
    }
}
