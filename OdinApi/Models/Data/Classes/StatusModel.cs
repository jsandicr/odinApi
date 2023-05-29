using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class StatusModel : IStatusModel
    {

        private readonly OdinContext _context;

        public StatusModel(OdinContext context)
        {
            _context = context;
        }
        public Status GetStatusById(int id)
        {
            try
            {
                Status status = _context.Status.Find(id);
                if (status != null)
                {
                    return status;
                }
                else
                {
                    return new Status();
                }
            }
            catch (Exception)
            {
                return new Status();
            }
        }

        public List<Status> GetStatus()
        {
            try
            {
                return _context.Status.ToList();
            }
            catch (Exception)
            {
                return new List<Status>();
            }
        }

        public Status PostStatus(Status status)
        {
            try
            {
                _context.Status.Add(status);
                _context.SaveChanges();
                return status;
            }
            catch (Exception)
            {
                return new Status();
            }
        }

        public Status DeleteStatus(int id)
        {
            try
            {
                Status status = _context.Status.Find(id);
                if (status != null)
                {
                    _context.Remove(status);
                    _context.SaveChanges();
                    return status;
                }
                else
                {
                    return new Status();
                }
            }
            catch (Exception)
            {
                return new Status();
            }
        }

        public Status PutStatus(Status status)
        {
            try
            {
                _context.Update(status);
                _context.SaveChanges();
                return status;
            }
            catch (Exception)
            {
                return new Status();
            }
        }
    }
}
