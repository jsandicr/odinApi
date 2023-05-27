using OdinApi.Models.Obj;

namespace OdinApi.Models.Data
{
    public class UserModel : IUserModel
    {

        private readonly OdinContext _context;

        public UserModel(OdinContext context)
        {
            _context = context;
        }
        public User GetUserById(int id)
        {
            try
            {
                return _context.User.Find(id);
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                return _context.User.ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public User PostUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User DeleteUser(User user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User PutUser(User user)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                return new User();
            }
        }
    }
}
