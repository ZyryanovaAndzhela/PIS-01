using gos_uslugi.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class ИзменениеУслуги : Form
    {
        private long _serviceId;
        private Service _service;
        private List<GovernmentEmployee> _employees;
        private readonly IServiceRepository _serviceRepository; 

        public ИзменениеУслуги(long serviceId, IServiceRepository serviceRepository)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _serviceRepository = serviceRepository;

            LoadService();
            LoadEmployees();
        }

        private async void LoadService()
        {
            try
            {
                _service = await _serviceRepository.FindById(_serviceId);

                if (_service != null)
                {
                    DisplayServiceDetails();
                }
                else
                {
                    MessageBox.Show("Услуга не найдена.");
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
                this.Close();
            }
        }

        private async void LoadEmployees()
        {
            _employees = await _serviceRepository.GetAllEmployees();
            comboBoxEmployee.DataSource = _employees;
            comboBoxEmployee.DisplayMember = "FullName";
            comboBoxEmployee.ValueMember = "Id";
        }

        private void DisplayServiceDetails()
        {
            if (_service != null)
            {
                textBoxDescription.Text = _service.Description;
                richTextBoxInstructions.Text = _service.Instructions;
                numericUpDownPrice.Value = (decimal)_service.Price;
                dateTimePickerStartDate.Value = _service.StartDate;
                dateTimePickerEndDate.Value = _service.EndDate;
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
                {
                    MessageBox.Show("Описание не может быть пустым.");
                    return;
                }

                _service.Description = textBoxDescription.Text;
                _service.Instructions = richTextBoxInstructions.Text;
                _service.Price = numericUpDownPrice.Value;
                _service.StartDate = dateTimePickerStartDate.Value;
                _service.EndDate = dateTimePickerEndDate.Value;

                await _serviceRepository.UpdateService(_service);

                MessageBox.Show("Услуга успешно изменена.");
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить эту услугу?", "Подтверждение удаления", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    await _serviceRepository.DeleteService(_serviceId);

                    MessageBox.Show("Услуга успешно удалена.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
                }
            }
        }

        private void buttonEditRules_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionString; 
            RuleRepository ruleRepository = new RuleRepository(connectionString);
            ИзменениеПравилУслуги editRules = new ИзменениеПравилУслуги(_serviceId, _serviceRepository, ruleRepository);
            this.Hide();
            editRules.ShowDialog();
            this.Show();
        }
    }
}