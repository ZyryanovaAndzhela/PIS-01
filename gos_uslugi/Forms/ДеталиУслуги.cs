using gos_uslugi.Repositories;
using gos_uslugi.Services;
using System;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class ДеталиУслуги: Form
    {
        private Service _service;
        private Account _account;

        public ДеталиУслуги(Service service, Account account)
        {
            InitializeComponent();
            _service = service;
            _account = account;
            DisplayServiceDetails();

            if (_account.Role == "admin")
            {
                buttonEditService.Visible = true;
            }
            else
            {
                buttonEditService.Visible = false;
            }
        }

        private void DisplayServiceDetails()
        {
            textBox1.Text = _service.Description;
            textBox2.Text = _service.Price.ToString("C");
            dateTimePicker1.Value = _service.StartDate;
            dateTimePicker2.Value = _service.EndDate;
            richTextBoxInstructions.Text = _service.Instructions;

            richTextBoxRules.Clear();
            if (_service.Rules != null && _service.Rules.Count > 0)
            {
                richTextBoxRules.AppendText(Environment.NewLine + "Сроки и условия:" + Environment.NewLine);
                foreach (ServiceRule rule in _service.Rules)
                {
                    string parameter = rule.ConditionValues;
                    string parameterType = rule.ConditionType;
                    int term = rule.TermOfServiceProvision;

                    richTextBoxRules.AppendText($"*   Параметр: {parameter}, Тип: {parameterType}, Срок: {term} дней" + Environment.NewLine);
                }

                richTextBoxRules.AppendText(Environment.NewLine);
            }
            else
            {
                richTextBoxRules.AppendText("Правила для данной услуги отсутствуют." + Environment.NewLine);
            }
        }

        private void buttonEditService_Click(object sender, EventArgs e)
        {
            RuleRepository ruleRepository = new RuleRepository(ConfigurationManager.ConnectionString);
            RuleService ruleService = new RuleService(ruleRepository);
            IServiceRepository serviceRepository = new ServiceRepository(ConfigurationManager.ConnectionString, ruleService);
            ИзменениеУслуги изменениеУслуги = new ИзменениеУслуги(_service.Id, serviceRepository);
            this.Hide();
            изменениеУслуги.ShowDialog();
            this.Show();
        }
    }
}
