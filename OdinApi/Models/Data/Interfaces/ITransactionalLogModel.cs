using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface ITransactionalLogModel
    {
        public List<TransactionalLog> GetTransactionalLogs();
        public TransactionalLog GetTransactionalLogById(int id);
        public TransactionalLog PostTransactionalLog(TransactionalLog transactionalLog);
        public TransactionalLog PutTransactionalLog(TransactionalLog transactionalLog);
        public bool DeleteTransactionalLog(int days);
    }
}