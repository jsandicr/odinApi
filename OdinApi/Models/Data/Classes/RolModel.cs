using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class RolModel : IRolModel
    {

        private readonly OdinContext _context;

        public RolModel(OdinContext context)
        {
            _context = context;
        }
        public Rol GetRolById(int id)
        {
            try
            {
                Rol rol = _context.Rol.Find(id);
                if(rol != null)
                {
                    return rol;
                }
                else
                {
                    return new Rol();
                }
            }
            catch (Exception)
            {
                return new Rol();
            }
        }

        public List<Rol> GetRoles()
        {
            try
            {
                return _context.Rol.ToList();
            }
            catch (Exception)
            {
                return new List<Rol>();
            }
        }

        public Rol PostRol(Rol rol)
        {
            try
            {
                _context.Rol.Add(rol);
                _context.SaveChanges();
                return rol;
            }
            catch (Exception)
            {
                return new Rol();
            }
        }

        public Rol DeleteRol(int id)
        {
            try
            {
                Rol rol = _context.Rol.Find(id);
                if (rol != null)
                {
                    _context.Remove(rol);
                    _context.SaveChanges();
                    return rol;
                }
                else
                {
                    return new Rol();
                }
            }
            catch (Exception)
            {
                return new Rol();
            }
        }

        public Rol PutRol(Rol rol)
        {
            try
            {
                _context.Update(rol);
                _context.SaveChanges();
                return rol;
            }
            catch (Exception)
            {
                return new Rol();
            }
        }
    }
}
