using System;
using System.Windows.Forms;

namespace gos_uslugi
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigurationManager.ConnectionString = "Server=localhost;Port=5433;Database=gos_uslugi;Username=postgres;Password=9943;";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IAccountRepository accountRepository = new AccountRepository(ConfigurationManager.ConnectionString);
            IAuthenticationService authenticationService = new AuthenticationService(accountRepository);
            Application.Run(new Авторизация(authenticationService));
        }
    }
}
