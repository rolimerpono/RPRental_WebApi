
using RPRENTAL_WEBAPP.Models.DTO;
using RPRENTAL_WEBAPP.Services.Interface;

namespace RPRENTAL_WEBAPP.Services.Implementation
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly IHttpClientFactory _clientFactory;

        public RoomService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory  = clientFactory;
        }

        public Task<T> AnyAsync<T>(RoomDTO objRoom)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAsync<T>(RoomDTO objRoom)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(RoomDTO objRoom)
        {
            throw new NotImplementedException();
        }
    }
}
