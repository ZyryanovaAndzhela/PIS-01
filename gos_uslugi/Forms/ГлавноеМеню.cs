using Npgsql;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using gos_uslugi.Repositories;
using gos_uslugi.Services;

namespace gos_uslugi
{
    public partial class ГлавноеМеню: Form
    {
        private readonly Account _account;
        private readonly IForeignerRepository _foreignerRepository;
        private readonly IAuthenticationService _authenticationService;
        public ГлавноеМеню(Account account, IForeignerRepository foreignerRepository, IAuthenticationService authenticationService)
        {
            InitializeComponent();
            _account = account;
            _foreignerRepository = foreignerRepository;
            _authenticationService = authenticationService;

            button1.Visible = !(_account.Role == "admin" || _account.Role == "employee");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IAccountRepository accountRepository = new AccountRepository();
            IForeignerRepository foreignerRepository = new ForeignerRepository();
            IForeignerService foreignerService = new ForeignerService(foreignerRepository, accountRepository);

            ЛичныйКабинет form3 = new ЛичныйКабинет(_account, foreignerService, accountRepository);
            this.Hide();
            form3.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RuleRepository ruleRepository = new RuleRepository(ConfigurationManager.ConnectionString);
            RuleService ruleService = new RuleService(ruleRepository);
            IServiceRepository serviceRepository = new ServiceRepository(ConfigurationManager.ConnectionString, ruleService);
            Услуги form6 = new Услуги(_account, serviceRepository);
            this.Hide();
            form6.ShowDialog();
            this.Show();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5433;Database=gos_uslugi;Username=postgres;Password=9943;";
            Foreigner foreigner = await _foreignerRepository.GetForeignerByLogin(_account.Login);
            IRequestRepository requestRepository = new RequestRepository(connectionString);
            IRequestService requestService = new RequestService(requestRepository);
            Заявки form5 = new Заявки(_account, requestService); 
            this.Hide();
            form5.ShowDialog();
            this.Show();
        }
    }
}
