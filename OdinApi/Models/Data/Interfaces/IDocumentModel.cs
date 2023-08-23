using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IDocumentModel
    {
        public List<Document> GetDocuments();
        public Document GetDocumentById(int id);
        public Document PostDocument(Document Document);
        public Document PutDocument(Document Document);
        public Document DeleteDocument(long id);
    }
}