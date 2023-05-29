using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IRolModel
    {
        public List<Rol> GetRoles();
        public Rol GetRolById(int id);
        public Rol PostRol(Rol rol);
        public Rol PutRol(Rol rol);
        public Rol DeleteRol(int id);
    }
}
