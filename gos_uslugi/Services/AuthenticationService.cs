using System.Threading.Tasks;

namespace gos_uslugi
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthenticationService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> Authenticate(string login, string password)
        {
            Account account = await _accountRepository.FindByLogin(login);

            if (account == null)
            {
                return null;
            }

            if (account.Password != password)
            {
                return null;
            }

            return account;
        }
    }
}