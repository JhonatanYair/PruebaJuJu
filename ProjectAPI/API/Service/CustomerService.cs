using API.Common;
using Business;
using System.Threading.Tasks;
using CustomerEntity = DataAccess.Data.Customer;
using PostEntity = DataAccess.Data.Post;

namespace API.Service
{
    public class CustomerService : ICustomerService
    {

        private BaseService<CustomerEntity> _customerService;
        private BaseService<PostEntity> _postService;

        public CustomerService(BaseService<CustomerEntity> customerService, BaseService<PostEntity> postService) 
        {
            _customerService = customerService;
            _postService = postService;
        }

        public async Task<ResponseApi> GetAllCustomersAsync()
        {
            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.OK,
                ObjectResponse = _customerService.GetAll()
            };
        }

        public async Task<ResponseApi> GetByIdCustomerAsync(int id)
        {
            var customerFind = _customerService.GetById(id);
            ResponseApi responseApi = new ResponseApi();

            if (customerFind == null)
            {
                responseApi.CodeHttp = System.Net.HttpStatusCode.NotFound;
                responseApi.Message = "No se encontró con el customer";
            }

            responseApi.CodeHttp = System.Net.HttpStatusCode.OK;
            responseApi.ObjectResponse = customerFind;
            return responseApi;
        }

        public async Task<ResponseApi> CreateCustomerAsync(CustomerEntity customerEntity)
        {
            var searchCustomer = _customerService.FindByAttribute(nameof(CustomerEntity.Name),customerEntity.Name);

            if (searchCustomer != null)
            {
                return new ResponseApi
                {
                    CodeHttp= System.Net.HttpStatusCode.Conflict,
                    Message = "Se encontró un customer con el mismo nombre"
                };
            }

            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.Created,
                Message = "Se ha creado el customer existosamente",
                ObjectResponse = _customerService.Create(customerEntity)
            };
        }

        public async Task<ResponseApi> UpdateCustomerAsync(int id, CustomerEntity customerEntity)
        {
            var searchCustomer = _customerService.FindByAttribute(nameof(CustomerEntity.Name), customerEntity.Name);

            if (searchCustomer != null && searchCustomer.CustomerId != id)
            {
                return new ResponseApi
                {
                    CodeHttp = System.Net.HttpStatusCode.Conflict,
                    Message = "Se encontró un customer con el mismo nombre"
                };
            }

            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.OK,
                Message = "Se ha editado el customer existosamente",
                ObjectResponse = _customerService.Update(id, customerEntity, out bool changed)
            };
        }

        public async Task<ResponseApi> DeleteCustomerAsync(int id)
        {
            var customerFind = _customerService.GetById(id);

            if (customerFind == null)
            {
                return new ResponseApi
                {
                    CodeHttp = System.Net.HttpStatusCode.Found,
                    Message = "No se encontró con el customer"
                };
            }

            var postFindList = _postService.FindByAttributeList(nameof(PostEntity.CustomerId), id);
            _postService.DeleteList(postFindList);
            _customerService.Delete(customerFind);
            return new ResponseApi
            {
                CodeHttp = System.Net.HttpStatusCode.OK,
                Message = "Se ha eliminado el customer exitosamente",
            };
        }

    }

    public interface ICustomerService
    {
        Task<ResponseApi> GetAllCustomersAsync();
        Task<ResponseApi> GetByIdCustomerAsync(int id);
        Task<ResponseApi> CreateCustomerAsync(CustomerEntity customerEntity);
        Task<ResponseApi> UpdateCustomerAsync(int id, CustomerEntity customerEntity);
        Task<ResponseApi> DeleteCustomerAsync(int id);
    }

}
