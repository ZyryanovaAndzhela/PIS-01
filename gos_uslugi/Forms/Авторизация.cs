using System;
using System.Windows.Forms;
using gos_uslugi.Repositories;
using gos_uslugi.Services;

namespace gos_uslugi
{
    public partial class Авторизация : Form
    {
        private readonly IAuthenticationService _authenticationService;
        IAccountRepository accountRepository = new AccountRepository();
        private readonly IForeignerRepository foreignerRepository = new ForeignerRepository();

        public Авторизация(IAuthenticationService authenticationService)
        {
            InitializeComponent();
            _authenticationService = authenticationService;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            Account account = await _authenticationService.Authenticate(login, password);

            if (account != null)
            {
                ГлавноеМеню form2 = new ГлавноеМеню(account);
                this.Hide();
                form2.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
            IForeignerService foreignerService = new ForeignerService(foreignerRepository, accountRepository);
            СозданиеНовогоАккаунта form4 = new СозданиеНовогоАккаунта(accountRepository, foreignerRepository, foreignerService);
            this.Hide();
            form4.ShowDialog();
            this.Show();
        }
    }
}