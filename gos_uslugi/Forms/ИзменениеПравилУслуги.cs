using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class ИзменениеПравилУслуги: Form
    {
        private long _serviceId;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRuleRepository _ruleRepository;
        private List<ServiceRule> _rules;

        public ИзменениеПравилУслуги(long serviceId, IServiceRepository serviceRepository, IRuleRepository ruleRepository)
        {
            InitializeComponent();
            _serviceId = serviceId;
            _serviceRepository = serviceRepository;
            _ruleRepository = ruleRepository;

            LoadRules();
        }

        private async void LoadRules()
        {
            try
            {
                List<ServiceRule> rulesFromDb = await _ruleRepository.GetServiceRules(_serviceId);
                _rules = new List<ServiceRule>(rulesFromDb);
                
                dataGridViewRules.DataSource = _rules;
                dataGridViewRules.Columns.Clear();
                dataGridViewRules.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Description",
                    HeaderText = "Описание",
                    Width = 150
                });

                dataGridViewRules.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ConditionType",
                    HeaderText = "Тип условия",
                    Width = 150
                });

                dataGridViewRules.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ConditionValues",
                    HeaderText = "Значение условия",
                    Width = 150
                });

                dataGridViewRules.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OperatorValues",
                    HeaderText = "Оператор",
                    Width = 100
                });

                dataGridViewRules.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TermOfServiceProvision",
                    HeaderText = "Срок",
                    Width = 75
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }
        }

        private async void buttonSaveRules_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ServiceRule rule in _rules)
                {
                    if (rule.Id == 0) 
                    {
                        await _ruleRepository.SaveServiceRule(rule);
                    }
                    else
                    {
                        await _ruleRepository.UpdateServiceRule(rule);
                    }
                }

                MessageBox.Show("Правила успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }
        }

        private async void buttonDeleteRule_Click(object sender, EventArgs e)
        {
            if (dataGridViewRules.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewRules.SelectedRows[0];
                long ruleId = (long)selectedRow.Index;

                try
                {
                    await _ruleRepository.DeleteServiceRule(ruleId);

                    LoadRules();

                    MessageBox.Show("Правило услуги успешно удалено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении правила услуги: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите правило услуги для удаления.");
            }
        }
    }
}
