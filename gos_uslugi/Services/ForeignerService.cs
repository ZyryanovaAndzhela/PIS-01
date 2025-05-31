using System;
using System.Threading.Tasks;

namespace gos_uslugi.Services
{
    class ForeignerService : IForeignerService
    {
        private readonly IForeignerRepository _foreignerRepository;
        private readonly IAccountRepository _accountRepository;

        public ForeignerService(IForeignerRepository foreignerRepository, IAccountRepository accountRepository)
        {
            _foreignerRepository = foreignerRepository;
            _accountRepository = accountRepository;
        }
        public async Task<Foreigner> GetForeignerByLogin(string login)
        {
            return await _foreignerRepository.GetForeignerByLogin(login);
        }
        public async Task UpdateForeignerInfo(Foreigner foreigner)
        {
            await _foreignerRepository.Update(foreigner);
        }
        public async Task<Foreigner> GetForeignerById(long foreignerId)
        {
            return await _foreignerRepository.FindById(foreignerId);
        }

        public async Task<Foreigner> RegisterForeigner(Foreigner foreignerData)
        {
            return foreignerData;
        }

        public async Task<Foreigner> UpdateContactInfo(long foreignerId, string email, string number)
        {
            var foreigner = await _foreignerRepository.FindById(foreignerId);

            if (foreigner == null)
                throw new Exception($"Foreigner c id = {foreignerId} не найден");

            foreigner.Email = email;
            foreigner.PhoneNumber = number;

            return foreigner;
        }
    }
}