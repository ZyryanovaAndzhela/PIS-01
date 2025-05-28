using System.Collections.Generic;
using System.Threading.Tasks;

namespace gos_uslugi.Services
{
    class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public async Task UpdateAsync(Request request)
        {
            await _requestRepository.UpdateAsync(request);
        }
        public async Task<List<Request>> GetForeignerRequests(Account account, string filterStatus, string searchQuery)
        {
            return await _requestRepository.GetForeignerRequests(account, filterStatus, searchQuery);
        }
        public async Task<(string EmployeeName, string UserName, string ServiceDescription)> GetRequestDetails(long requestId)
        {
            return await _requestRepository.GetRequestDetails(requestId);
        }
        public async Task UpdateRequest(Request request)
        {
            await _requestRepository.Save(request);
        }
    }
}
