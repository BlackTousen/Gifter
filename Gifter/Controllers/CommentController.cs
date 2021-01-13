using Gifter.Models;
using Gifter.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_commentRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null) { return NotFound(); }
            return Ok(comment);
        }
        [HttpGet("getbyuser/{id}")]
        public IActionResult GetByUser(int id)
        {
            return Ok(_commentRepository.GetByUserProfileId(id));
        }
        [HttpGet("getbypost/{id}")]
        public IActionResult GetByPost(int id)
        {
            return Ok(_commentRepository.GetByPostId(id));
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Comment comment)
        {
            if (id != comment.Id || comment.Id < 1 || _commentRepository.GetById(comment.Id) == null)
            {
                return BadRequest();
            }
            _commentRepository.Update(comment);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _commentRepository.Delete(id);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
