using OdinApi.Models.Obj;
using System.Security.Claims;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IUserModel
    {
        public List<User> GetUsers();
        public User GetUserById(int id);
        public User PostUser(User user);
        public User PutUser(User user);
        public User DeleteUser(int id);
        public User Login(UserDTO userDTO);
    }
}
