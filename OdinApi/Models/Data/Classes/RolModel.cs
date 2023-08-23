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

        public List<Rol> GetRoles()
        {
            return _context.Rol.ToList();   
        }

        public Rol GetFirstRol()
        {
            return _context.Rol.FirstOrDefault();              
        }

        public Rol PostRol(Rol rol)
        {
            _context.Rol.Add(rol);
            _context.SaveChanges();
            return rol;                 
    }

        public Rol DeleteRol(int id)
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

        public Rol PutRol(Rol rol)
        {
            _context.Update(rol);
            _context.SaveChanges();
            return rol;            
        }
    }
}