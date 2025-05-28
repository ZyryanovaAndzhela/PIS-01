using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class НоваяЗаявка : Form
    {
        private readonly Account _account;
        private readonly IRequestService _requestService;
        private readonly IRequestRepository _requestRepository;
        private readonly string _connectionString;
        private Foreigner _foreigner;

        public НоваяЗаявка(Account account, IRequestService requestService, IRequestRepository requestRepository, string connectionString)
        {
            InitializeComponent();
            _account = account;
            _requestService = requestService;
            _requestRepository = requestRepository;
            _connectionString = connectionString;
        }

        private void НоваяЗаявка_Load(object sender, EventArgs e)
        {
            LoadForeignerData();
        }

        private async void LoadForeignerData()
        {
            _foreigner = await _requestRepository.GetForeignerByAccountId(_account.Id);

            if (_foreigner != null)
            {
                await LoadServices();
            }
            else
            {
                MessageBox.Show("Не удалось получить данные об иностранце.");
            }
        }

        private async Task LoadServices()
        {
            if (_foreigner != null)
            {
                try
                {
                    string citizen = _foreigner.Citizen;
                    string purposeVisit = _foreigner.PurposeVisit;

                    List<Service> services = await _requestRepository.GetAllServices(citizen, purposeVisit);

                    comboBoxService.DataSource = services;
                    comboBoxService.DisplayMember = "Description";
                    comboBoxService.ValueMember = "Id";

                    await LoadServiceRules();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке услуг: {ex.Message}");
                }
            }
        }

        private async Task LoadServiceRules()
        {
            if (comboBoxService.SelectedItem != null && _foreigner != null)
            {
                Service selectedService = (Service)comboBoxService.SelectedItem;
                long serviceId = selectedService.Id;

                if (selectedService != null && selectedService.Rules != null && selectedService.Rules.Count > 0)
                {
                    ServiceRule rule = selectedService.Rules[0];
                    labelRuleType.Text = "Тип параметра: " + rule.ConditionType;
                    labelRuleParameter.Text = "Параметр: " + rule.ConditionValues;
                    labelRuleTerm.Text = $"Срок: " + rule.TermOfServiceProvision + " дней";
                }
                else
                {
                    labelRuleType.Text = "Тип параметра: Нет данных";
                    labelRuleParameter.Text = "Параметр: Нет данных";
                    labelRuleTerm.Text = "Срок: Нет данных";
                }
            }
        }

        private async void comboBoxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadServiceRules();
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_foreigner == null)
                {
                    MessageBox.Show("Необходимо загрузить данные об иностранце.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxService.SelectedItem == null)
                {
                    MessageBox.Show("Необходимо выбрать услугу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                long employeeId = _account.Id;
                long foreignerId = _foreigner.Id;
                long serviceId = ((Service)comboBoxService.SelectedItem).Id;
                Status status = Status.Создана;
                DateTime dateCreation = DateTime.Now;
                DateTime? dateCompletion = null;

                string deadline = null;
                if (comboBoxService.SelectedItem != null && _foreigner != null)
                {
                    Service selectedService = (Service)comboBoxService.SelectedItem;

                    if (selectedService.Rules != null && selectedService.Rules.Count > 0)
                    {
                        ServiceRule rule = selectedService.Rules[0];
                        deadline = rule.TermOfServiceProvision + " дней";
                    }
                    else
                    {
                        deadline = "Срок не определен";
                    }
                }
                else
                {
                    deadline = "Срок не определен";
                }

                string result = null;

                Request newRequest = new Request
                {
                    EmployeeId = employeeId,
                    ForeignerId = foreignerId,
                    ServiceId = serviceId,
                    Status = status,
                    DateCreation = dateCreation,
                    DateCompletion = dateCompletion,
                    Deadline = deadline,
                    Result = result
                };

                await _requestRepository.CreateRequest(newRequest);

                MessageBox.Show("Заявка успешно создана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заявки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}