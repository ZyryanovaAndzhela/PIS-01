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
        public async Task UpdateForeignerInfo(long accountId, string fullName, string email, string phoneNumber, string inn, string citizen, string passport, string password)
        {
            if (await _foreignerRepository.IsEmailAlreadyRegistered(email, accountId))
            {
                throw new Exception("Этот email уже зарегистрирован. Пожалуйста, используйте другой email.");
            }

            var account = await _accountRepository.FindById(accountId);
            account.FullName = fullName;
            account.Password = password;
            account.Login = email;
            await _accountRepository.Save(account);

            var foreigner = await _foreignerRepository.FindById(accountId);
            foreigner.Email = email;
            foreigner.PhoneNumber = phoneNumber;
            foreigner.INN = inn;
            foreigner.Citizen = citizen;
            foreigner.Passport = passport;
            await _foreignerRepository.Save(foreigner);
        }
        public async Task<Foreigner> GetForeignerById(long foreignerId)
        {
            return await _foreignerRepository.FindById(foreignerId);
        }

        public async Task<Foreigner> RegisterForeigner(Foreigner foreignerData)
        {
            return await _foreignerRepository.Save(foreignerData);
        }

        public async Task<Foreigner> UpdateContactInfo(long foreignerId, string email, string number)
        {
            var foreigner = await _foreignerRepository.FindById(foreignerId);

            if (foreigner == null)
                throw new Exception($"Foreigner id = {foreignerId} was not found");

            foreigner.Email = email;
            foreigner.PhoneNumber = number;

            return await _foreignerRepository.Save(foreigner);
        }
    }
}
