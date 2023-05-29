using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OdinApi.Models.Data.Classes
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
                var query = (from u in _context.User
                             join r in _context.Rol
                             on u.idRol equals r.id
                             join b in _context.Branch
                             on u.idBranch equals b.id
                             where u.id == id
                             select new { User = u, Rol = r, Branch = b }).ToList();

                if (query.Count > 0)
                {
                    return query.FirstOrDefault().User;
                }
                else
                {
                    return new User();
                }
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
                var query = (from u in _context.User
                             join r in _context.Rol
                             on u.idRol equals r.id
                             join b in _context.Branch
                             on u.idBranch equals b.id
                             select new { User = u, Rol = r, Branch = b }).ToList();

                if (query != null)
                {
                    List<User> users = new List<User>();
                    foreach (var q in query)
                    {
                        users.Add(q.User);
                    }
                    return users;
                }
                else
                {
                    return new List<User>();
                }
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

        public User DeleteUser(int id)
        {
            try
            {
                User user = _context.User.Find(id);
                if (user != null)
                {
                    _context.Remove(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    return new User();
                }
                
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
