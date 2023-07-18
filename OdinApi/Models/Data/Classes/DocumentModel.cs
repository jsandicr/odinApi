using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class DocumentModel : IDocumentModel
    {
        private readonly OdinContext _context;

        public DocumentModel(OdinContext context)
        {
            _context = context;
        }
        public Document GetDocumentById(int id)
        {
            try
            {
                var query = (from c in _context.Document
                             join u in _context.User
                             on c.idUser equals u.id
                             join t in _context.Ticket
                             on c.idTicket equals t.id
                             where c.id == id
                             select new { Document = c, User = u, Ticket = t }).ToList();

                if (query.Count > 0)
                {
                    return query.FirstOrDefault().Document;
                }
                else
                {
                    return new Document();
                }
            }
            catch (Exception)
            {
                return new Document();
            }
        }

        public List<Document> GetDocuments()
        {
            try
            {
                var query = (from c in _context.Document
                             join u in _context.User
                             on c.idUser equals u.id
                             join t in _context.Ticket
                             on c.idTicket equals t.id
                             select new { Documents = c, Users = u, Tickets = t }).ToList();

                if (query != null)
                {
                    List<Document> Documents = new List<Document>();
                    foreach (var q in query)
                    {
                        Documents.Add(q.Documents);
                    }
                    return Documents;
                }
                else
                {
                    return new List<Document>();
                }
            }
            catch (Exception)
            {
                return new List<Document>();
            }
        }

        public Document PostDocument(Document Document)
        {
            try
            {
                _context.Document.Add(Document);
                _context.SaveChanges();
                return Document;
            }
            catch (Exception)
            {
                return new Document();
            }
        }

        public Document DeleteDocument(long id)
        {
            try
            {
                Document Document = _context.Document.Find(id);
                if (Document != null)
                {
                    _context.Remove(Document);
                    _context.SaveChanges();
                    return Document;
                }
                else
                {
                    return new Document();
                }
            }
            catch (Exception)
            {
                return new Document();
            }
        }

        public Document PutDocument(Document Document)
        {
            try
            {
                _context.Update(Document);
                _context.SaveChanges();
                return Document;
            }
            catch (Exception)
            {
                return new Document();
            }
        }
    }
}
