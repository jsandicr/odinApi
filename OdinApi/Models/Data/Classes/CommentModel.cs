using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class CommentModel : ICommentModel
    {
        private readonly OdinContext _context;

        public CommentModel(OdinContext context)
        {
            _context = context;
        }
        public Comment GetCommentById(int id)
        {
            var query = (from c in _context.Comment
                            join u in _context.User
                            on c.idUser equals u.id
                            join t in _context.Ticket
                            on c.idTicket equals t.id
                            where c.id == id
                            select new { Comment = c, User = u, Ticket = t }).ToList();

            if (query.Count > 0)
            {
                return query.FirstOrDefault().Comment;
            }
            else
            {
                return new Comment();
            }
        }

        public List<Comment> GetComments()
        {
            var query = (from c in _context.Comment
                            join u in _context.User
                            on c.idUser equals u.id
                            join t in _context.Ticket
                            on c.idTicket equals t.id
                            select new { Comments = c, Users = u, Tickets = t }).ToList();

            if (query != null)
            {
                List<Comment> comments = new List<Comment>();
                foreach (var q in query)
                {
                    comments.Add(q.Comments);
                }
                return comments;
            }
            else
            {
                return new List<Comment>();
            }
        }

        public Comment PostComment(Comment comment)
        {
            _context.Comment.Add(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment DeleteComment(int id)
        {
            Comment comment = _context.Comment.Find(id);
            if (comment != null)
            {
                _context.Remove(comment);
                _context.SaveChanges();
                return comment;
            }
            else
            {
                return new Comment();
            }
        }

        public Comment PutComment(Comment comment)
        {
            _context.Update(comment);
            _context.SaveChanges();
            return comment;
        }
    }
}