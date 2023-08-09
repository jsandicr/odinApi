using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class ErrorLogModel : IErrorLogModel
    {

        private readonly OdinContext _context;

        public ErrorLogModel(OdinContext context)
        {
            _context = context;
        }
        public ErrorLog GetErrorLogById(int id)
        {
            try
            {
                var query = (from e in _context.ErrorLog
                             join u in _context.User
                             on e.idUser equals u.id
                             where e.id == id
                             select new { ErrorLog = e, User = u }).ToList();

                if (query.Count > 0)
                {
                    return query.FirstOrDefault().ErrorLog;
                }
                else
                {
                    return new ErrorLog();
                }
            }
            catch (Exception)
            {
                return new ErrorLog();
            }
        }

        public List<ErrorLog> GetErrorLogs()
        {
            try
            {
                var query = _context.ErrorLog.ToList();

                if (query != null)
                {
                    return query;
                }
                else
                {
                    return new List<ErrorLog>();
                }
            }
            catch (Exception)
            {
                return new List<ErrorLog>();
            }
        }

        public ErrorLog PostErrorLog(ErrorLog errorLog)
        {
            try
            {
                _context.ErrorLog.Add(errorLog);
                _context.SaveChanges();
                return errorLog;
            }
            catch (Exception)
            {
                return new ErrorLog();
            }
        }

        public ErrorLog DeleteErrorLog(int id)
        {
            try
            {
                ErrorLog errorLog = _context.ErrorLog.Find(id);
                if (errorLog != null)
                {
                    _context.Remove(errorLog);
                    _context.SaveChanges();
                    return errorLog;
                }
                else
                {
                    return new ErrorLog();
                }
            }
            catch (Exception)
            {
                return new ErrorLog();
            }
        }

        public ErrorLog PutErrorLog(ErrorLog errorLog)
        {
            try
            {
                _context.Update(errorLog);
                _context.SaveChanges();
                return errorLog;
            }
            catch (Exception)
            {
                return new ErrorLog();
            }
        }
    }
}
