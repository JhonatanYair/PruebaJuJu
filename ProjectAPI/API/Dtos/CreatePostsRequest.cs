using System.Collections.Generic;
using DataAccess.Data;

namespace API.Dtos
{
    public class CreatePostsRequest
    {
        public List<Post> Posts { get; set; }

    }
}
