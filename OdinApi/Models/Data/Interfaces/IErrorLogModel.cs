using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IErrorLogModel
    {
        public List<ErrorLog> GetErrorLogs();
        public ErrorLog GetErrorLogById(int id);
        public ErrorLog PostErrorLog(ErrorLog errorLog);
        public ErrorLog PutErrorLog(ErrorLog errorLog);
        public ErrorLog DeleteErrorLog(int id);
    }
}
