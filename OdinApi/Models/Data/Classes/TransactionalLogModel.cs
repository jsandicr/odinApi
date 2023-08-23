using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class TransactionalLogModel : ITransactionalLogModel
    {

        private readonly OdinContext _context;

        public TransactionalLogModel(OdinContext context)
        {
            _context = context;
        }
        public TransactionalLog GetTransactionalLogById(int id)
        {
            try
            {
                var query = (from t in _context.TransactionalLog
                             join u in _context.User
                             on t.idUser equals u.id
                             where t.id == id
                             select new { TransactionalLog = t, User = u }).ToList();

                if (query.Count > 0)
                {
                    return query.FirstOrDefault().TransactionalLog;
                }
                else
                {
                    return new TransactionalLog();
                }
            }
            catch (Exception)
            {
                return new TransactionalLog();
            }
        }

        public List<TransactionalLog> GetTransactionalLogs()
        {
            try
            {
                var query = (from t in _context.TransactionalLog
                             join u in _context.User
                             on t.idUser equals u.id
                             select new { TransactionalLog = t, User = u }).ToList();

                if (query != null)
                {
                    List<TransactionalLog> errorLogs = new List<TransactionalLog>();
                    foreach (var q in query)
                    {
                        errorLogs.Add(q.TransactionalLog);
                    }
                    return errorLogs;
                }
                else
                {
                    return new List<TransactionalLog>();
                }
            }
            catch (Exception)
            {
                return new List<TransactionalLog>();
            }
        }

        public TransactionalLog PostTransactionalLog(TransactionalLog transactionalLog)
        {
            try
            {
                _context.TransactionalLog.Add(transactionalLog);
                _context.SaveChanges();
                return transactionalLog;
            }
            catch (Exception)
            {
                return new TransactionalLog();
            }
        }

        public bool DeleteTransactionalLog(int days)
        {
            try
            {
                DateTime fechaLimite = DateTime.Now.AddDays(-days); // Resta "days" días de la fecha actual

                // Consulta todos los registros de la tabla "logs" con fecha de creación anterior a la fecha límite
                var logsToDelete = _context.TransactionalLog
                                          .Where(log => log.date < fechaLimite)
                                          .ToList();

                // Elimina los registros obtenidos
                _context.TransactionalLog.RemoveRange(logsToDelete);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TransactionalLog PutTransactionalLog(TransactionalLog transactionalLog)
        {
            try
            {
                _context.Update(transactionalLog);
                _context.SaveChanges();
                return transactionalLog;
            }
            catch (Exception)
            {
                return new TransactionalLog();
            }
        }
    }
}