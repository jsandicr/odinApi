using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdinApi.Models;
using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentModel _DocumentModel;

        public DocumentController(IDocumentModel DocumentModel)
        {
            _DocumentModel = DocumentModel;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Document>> GetDocuments()
        {
            try
            {
                var Documents = _DocumentModel.GetDocuments();
                return Ok(Documents);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Document>>> GetDocumentById(int id)
        {
            //Retorna el Ok  que es igual al 200 (Status)
            try
            {
                var Document = _DocumentModel.GetDocumentById(id);
                if (Document == null)
                    return NotFound();
                return Ok(Document);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Document>> PostDocument(Document Document)
        {
            try
            {
                var response = _DocumentModel.PostDocument(Document);
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
        public async Task<ActionResult<Document>> PutDocument(int id, Document Document)
        {
            try
            {
                Document.id = id;
                var response = _DocumentModel.PutDocument(Document);
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
        public async Task<ActionResult<List<Document>>> DeleteDocument(int id)
        {
            try
            {
                var response = _DocumentModel.DeleteDocument(id);
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
