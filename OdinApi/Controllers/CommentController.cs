using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentModel _commentModel;

        public CommentController(ICommentModel commentModel)
        {
            _commentModel = commentModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Comment>> GetComments()
        {
            try
            {
                var comments = _commentModel.GetComments();
                return Ok(comments);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Comment>>> GetCommentById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var comment = _commentModel.GetCommentById(id);
                if (comment == null)
                    return NotFound();
                return Ok(comment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            try
            {
                var response = _commentModel.PostComment(comment);
                if (response.id != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Comment>> PutComment(int id, Comment comment)
        {
            try
            {
                comment.id = id;
                var response = _commentModel.PutComment(comment);
                if (response.id != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Comment>>> DeleteComment(int id)
        {
            try
            {
                var response = _commentModel.DeleteComment(id);
                if (response.id != 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
