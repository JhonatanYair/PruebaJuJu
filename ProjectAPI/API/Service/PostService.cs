using API.Common;
using Business;
using System.Threading.Tasks;
using PostEntity = DataAccess.Data.Post;
using CustomerEntity = DataAccess.Data.Customer;
using System.Collections.Generic;


namespace API.Service
{
    public class PostService : IPostService
    {

        private BaseService<PostEntity> _postService;
        private BaseService<CustomerEntity> _customerService;

        public PostService(BaseService<PostEntity> postService, BaseService<CustomerEntity> customerService)
        {
            _postService = postService;
            _customerService = customerService;
        }

        public async Task<ResponseApi> GetAllPostAsync()
        {
            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.OK,
                ObjectResponse = _postService.GetAll()
            };
        }

        public async Task<ResponseApi> GetByIdPostAsync(int id)
        {
            var postFind = _postService.GetById(id);
            ResponseApi responseApi = new ResponseApi();

            if (postFind == null)
            {
                responseApi.CodeHttp = System.Net.HttpStatusCode.NotFound;
                responseApi.Message = "No se encontró con el post";
            }

            responseApi.CodeHttp = System.Net.HttpStatusCode.OK;
            responseApi.ObjectResponse = postFind;
            return responseApi;
        }

        public async Task<ResponseApi> CreatePostAsync(PostEntity postEntity)
        {
            var searchCustomer = _customerService.GetById(postEntity.CustomerId);
            if (searchCustomer == null)
            {
                return new ResponseApi
                {
                    CodeHttp = System.Net.HttpStatusCode.NotFound,
                    Message = "No se encontró el customer"
                };
            }

            postEntity.Body = TruncateText(postEntity.Body);
            switch (postEntity.Type)
            {
                case 1:
                    postEntity.Category = "Farándula";
                        break;
                case 2:
                    postEntity.Category = "Política";
                    break;
                case 3:
                    postEntity.Category = "Futbol";
                    break;
                default:
                    postEntity.Type = 0;
                    break;
            }

            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.Created,
                Message = "Se ha creado el post existosamente",
                ObjectResponse = _postService.Create(postEntity)
            };
        }

        public async Task<ResponseApi> CreatePostListAsync(List<PostEntity> postsEntities)
        {
            foreach (var postEntity in postsEntities)
            {
                var searchCustomer = _customerService.GetById(postEntity.CustomerId);
                if (searchCustomer == null)
                {
                    return new ResponseApi
                    {
                        CodeHttp = System.Net.HttpStatusCode.NotFound,
                        Message = $"No se encontró el customer {postEntity.CustomerId}"
                    };
                }

                postEntity.Body = TruncateText(postEntity.Body);
                switch (postEntity.Type)
                {
                    case 1:
                        postEntity.Category = "Farándula";
                        break;
                    case 2:
                        postEntity.Category = "Política";
                        break;
                    case 3:
                        postEntity.Category = "Futbol";
                        break;
                    default:
                        postEntity.Type = 0;
                        break;
                }
            }

            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.Created,
                Message = "Se han creado los post existosamente",
                ObjectResponse = _postService.CreateList(postsEntities)
            };

        }

        public async Task<ResponseApi> UpdatePostAsync(int id, PostEntity postEntity)
        {
            var searchCustomer = _customerService.GetById(postEntity.CustomerId);
            if (searchCustomer == null)
            {
                return new ResponseApi
                {
                    CodeHttp = System.Net.HttpStatusCode.NotFound,
                    Message = "No se encontró el customer"
                };
            }

            postEntity.Body = TruncateText(postEntity.Body);
            switch (postEntity.Type)
            {
                case 1:
                    postEntity.Category = "Farándula";
                    break;
                case 2:
                    postEntity.Category = "Política";
                    break;
                case 3:
                    postEntity.Category = "Futbol";
                    break;
                default:
                    postEntity.Type = 0;
                    break;
            }

            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.OK,
                Message = "Se ha editado el post existosamente",
                ObjectResponse = _postService.Update(id, postEntity, out bool changed)
            };
        }

        public async Task<ResponseApi> DeletePostAsync(int id)
        {
            var postFind = _postService.GetById(id);

            if (postFind == null)
            {
                return new ResponseApi
                {
                    CodeHttp = System.Net.HttpStatusCode.Found,
                    Message = "No se encontró con el post"
                };
            }

            _postService.Delete(postFind);
            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.OK,
                Message = "Se ha eliminado el post exitosamente",
            };
        }

        private string TruncateText(string text)
        {
            if (text.Length > 20)
            {
                return text.Substring(0, 20) + "...";
            }
            return text;
        }

    }

    public interface IPostService
    {
        Task<ResponseApi> GetAllPostAsync();
        Task<ResponseApi> GetByIdPostAsync(int id);
        Task<ResponseApi> CreatePostAsync(PostEntity postEntity);
        Task<ResponseApi> CreatePostListAsync(List<PostEntity> postsEntities);
        Task<ResponseApi> UpdatePostAsync(int id, PostEntity postEntity);
        Task<ResponseApi> DeletePostAsync(int id);
    }
}
