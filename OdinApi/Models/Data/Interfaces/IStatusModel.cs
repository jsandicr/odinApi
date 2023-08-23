using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IStatusModel
    {
        public List<Status> GetStatus();
        public Status GetStatusById(int id);
        public Status PostStatus(Status status);
        public Status PutStatus(Status status);
        public Status DeleteStatus(int id);
    }
}