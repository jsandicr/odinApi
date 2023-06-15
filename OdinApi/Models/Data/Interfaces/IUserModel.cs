using OdinApi.Models.Obj;
using System.Security.Claims;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IUserModel
    {
        public List<User> GetUsers();
        public List<User> GetClients();
        public List<User> GetSupervisors();
        public User GetUserById(int id);
        public User GetUserByMail(string mail);
        public User PostUser(User user);
        public User PutUser(User user);
        public User DeleteUser(int id);
        public User Login(UserDTO userDTO);
        public User RestorePasword(RestorePassword user);
        string HashPassword(string password);

    }
}
