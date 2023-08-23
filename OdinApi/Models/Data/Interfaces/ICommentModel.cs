using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface ICommentModel
    {
        public List<Comment> GetComments();
        public Comment GetCommentById(int id);
        public Comment PostComment(Comment comment);
        public Comment PutComment(Comment comment);
        public Comment DeleteComment(int id);
    }
}