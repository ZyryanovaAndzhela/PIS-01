using System.Collections.Generic;
using System.Threading.Tasks;

namespace gos_uslugi
{
    public interface IRequestRepository
    {
        Task<Request> Save(Request request);
        Task UpdateAsync(Request request);
        Task<Request> FindById(long requestId);
        Task<List<Service>> GetAllServices(string citizen, string purposeVisit);
        Task CreateRequest(Request request);
        Task<Foreigner> GetForeignerByAccountId(long accountId);
        Task<List<ServiceRule>> GetServiceRules(long serviceId, string citizen, string purposeVisit);
        Task<List<Request>> GetForeignerRequests(Account account, string filterStatus, string searchQuery);
        Task<(string EmployeeName, string UserName, string ServiceDescription)> GetRequestDetails(long requestId);
    }

    public interface IForeignerRepository
    {
        Task<Foreigner> Save(Foreigner foreigner);
        Task<Foreigner> FindById(long foreignerId);
        Task<Foreigner> GetForeignerByLogin(string login);
    }

    public interface IAccountRepository
    {
        Task<Account> Save(Account account);
        Task<Account> FindByLogin(string login);
        Task<Account> FindById(long accountId);
    }

    public interface IServiceRepository
    {
        Task<List<Service>> SearchServices(string searchQuery);
        Task<Service> FindById(long serviceId);
        Task<List<GovernmentEmployee>> GetAllEmployees(); 
        Task UpdateService(Service service);
        Task DeleteService(long serviceId);
    }

    public interface IRuleRepository
    {
        Task<List<ServiceRule>> GetServiceRules(long serviceId);
        Task<ServiceRule> SaveServiceRule(ServiceRule serviceRule);
        Task UpdateServiceRule(ServiceRule serviceRule);
        Task DeleteServiceRule(long serviceId);
    }
}
