using System.Collections.Generic;
using System.Threading.Tasks;

namespace gos_uslugi
{
    public interface IRequestService
    {
        Task UpdateRequest(Request request);
        Task UpdateAsync(Request request);
        Task<List<Request>> GetForeignerRequests(Account account, string filterStatus, string searchQuery);
        Task<(string EmployeeName, string UserName, string ServiceDescription)> GetRequestDetails(long requestId);
    }

    public interface IForeignerService
    {
        Task<Foreigner> GetForeignerByLogin(string login);
        Task UpdateForeignerInfo(long accountId, string fullName, string email, string phoneNumber, string inn, string citizen, string passport, string password);
        Task<Foreigner> GetForeignerById(long foreignerId);
        Task<Foreigner> RegisterForeigner(Foreigner foreignerId);
        Task<Foreigner> UpdateContactInfo(long foreignerId, string email, string number);
    }

    public interface IAuthenticationService
    {
        Task<Account> Authenticate(string login, string password);
    }

    public interface IRuleService
    {
        Task<List<ServiceRule>> GetServiceRules(long serviceId);
        Task<ServiceRule> SaveServiceRule(ServiceRule serviceRule);
        Task UpdateServiceRule(ServiceRule serviceRule);
    }
}
