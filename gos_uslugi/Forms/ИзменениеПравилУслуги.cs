using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class ИзменениеПравилУслуги : Form
    {
        private long _serviceId;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRuleRepository _ruleRepository;
        List<ServiceRule> rulesFromDb;

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
            dataGridViewRules.Rows.Clear();

            try
            {
                rulesFromDb = await _ruleRepository.GetServiceRules(_serviceId);

                foreach (ServiceRule rule in rulesFromDb)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = rule.Description });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = rule.ConditionType });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = rule.ConditionValues });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = rule.OperatorValues });
                    row.Cells.Add(new DataGridViewTextBoxCell() { Value = rule.TermOfServiceProvision });

                    row.Tag = rule.Id;

                    dataGridViewRules.Rows.Add(row);
                }
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
                for (int i = 0; i < dataGridViewRules.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridViewRules.Rows[i];

                    if (!row.IsNewRow) 
                    {
                        if (row.Tag != null && row.Tag is long)
                        {
                            long ruleId = (long)row.Tag;

                            foreach (ServiceRule rule in rulesFromDb)
                            {
                                if (rule.Id == ruleId)
                                {
                                    rule.Description = (string)row.Cells["Description"].Value; 
                                    rule.ConditionType = (string)row.Cells["ConditionType"].Value;
                                    rule.ConditionValues = (string)row.Cells["ConditionValues"].Value;
                                    rule.OperatorValues = (string)row.Cells["OperatorValues"].Value;
                                    int term = Int32.Parse(row.Cells["TermOfServiceProvision"].Value.ToString());
                                    rule.TermOfServiceProvision = (int)term;

                                    if (rule.Id == 0)
                                    {
                                        await _ruleRepository.SaveServiceRule(rule);
                                    }
                                    else
                                    {
                                        await _ruleRepository.UpdateServiceRule(rule);
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Правила успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }

            LoadRules();
        }

        private async void buttonDeleteRule_Click(object sender, EventArgs e)
        {
            if (dataGridViewRules.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewRules.SelectedRows[0];

                if (selectedRow.Tag != null && selectedRow.Tag is long)
                {
                    long ruleId = (long)selectedRow.Tag;

                    try
                    {
                        await _ruleRepository.DeleteServiceRule(ruleId);

                        dataGridViewRules.Rows.Remove(selectedRow);

                        MessageBox.Show("Правило услуги успешно удалено!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении правила услуги: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Не удалось получить ID правила.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите правило услуги для удаления.");
            }
        }
    }
}