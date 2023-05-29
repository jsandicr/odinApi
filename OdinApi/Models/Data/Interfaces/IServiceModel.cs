using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IServiceModel
    {
        public List<Service> GetServices();
        public Service GetServiceById(int id);
        public Service PostService(Service service);
        public Service PutService(Service service);
        public Service DeleteService(int id);
    }
}
