using API.Dtos;
using API.Service;
using Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using PostEntity = DataAccess.Data.Post;

namespace API.Controllers.Post
{
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        //private BaseService<PostEntity> PostService;
        //public PostController(BaseService<PostEntity> postService)
        //{
        //    PostService = postService;
        //}

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllPostAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var postFind = await _postService.GetByIdPostAsync(id);

            if (postFind.CodeHttp == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(postFind);
            }
            else if (postFind.CodeHttp == System.Net.HttpStatusCode.OK)
            {
                return Ok(postFind);
            }
            else
            {
                return BadRequest(postFind);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostEntity entity)
        {
            var postCreated = await _postService.CreatePostAsync(entity);
            if (postCreated.CodeHttp == System.Net.HttpStatusCode.Created)
            {
                return Ok(postCreated);
            }
            else
            {
                return BadRequest(postCreated);
            }
        }

        [HttpPost("List")]
        public async Task<IActionResult> CreateList([FromBody] CreatePostsRequest createPosts)
        {
            var postCreated = await _postService.CreatePostListAsync(createPosts.Posts);
            if (postCreated.CodeHttp == System.Net.HttpStatusCode.Created)
            {
                return Ok(postCreated);
            }
            else
            {
                return BadRequest(postCreated);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostEntity entity)
        {
            var customerUpdated = await _postService.UpdatePostAsync(id, entity);
            if (customerUpdated.CodeHttp == System.Net.HttpStatusCode.Created)
            {
                return Ok(customerUpdated);
            }
            else
            {
                return BadRequest(customerUpdated);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerDelete = await _postService.DeletePostAsync(id);

            if (customerDelete.CodeHttp == System.Net.HttpStatusCode.OK)
            {
                return Ok(customerDelete);
            }
            else
            {
                return BadRequest(customerDelete);
            }
        }

    }
}
