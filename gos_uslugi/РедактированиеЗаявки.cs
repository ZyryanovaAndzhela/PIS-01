using Npgsql;
using System;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class РедактированиеЗаявки : Form
    {
        private Request _request;
        private Account _account;
        private readonly IRequestService _requestService;
        private readonly string _connectionString;

        public РедактированиеЗаявки(Request application, Account account, IRequestService requestService, string connectionString)
        {
            InitializeComponent();
            _request = application;
            _account = account;
            _requestService = requestService;
            _connectionString = connectionString;
            InitializeStatusComboBox();
            LoadApplicationData();
        }

        private void InitializeStatusComboBox()
        {
            comboBoxStatus.Items.Clear();
            foreach (Status status in Enum.GetValues(typeof(Status)))
            {
                comboBoxStatus.Items.Add(status);
            }
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadApplicationData()
        {
            comboBoxStatus.SelectedItem = _request.Status;
            UpdateCompletionDateVisibility();

            textBoxResult.Text = _request.Result ?? "";
            label3.Visible = _request.Status == Status.Завершена;
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCompletionDateVisibility();
        }

        private void UpdateCompletionDateVisibility()
        {
            Status selectedStatus = (Status)comboBoxStatus.SelectedItem;
            bool isCompleted = selectedStatus == Status.Завершена;

            label3.Visible = isCompleted;
            dateTimePickerCompletionDate.Visible = isCompleted;

            if (isCompleted)
            {
                dateTimePickerCompletionDate.Checked = true;
                dateTimePickerCompletionDate.Value = _request.DateCreation;
            }
            else
            {
                dateTimePickerCompletionDate.Checked = _request.DateCompletion != null;
                dateTimePickerCompletionDate.Value = _request.DateCompletion ?? DateTime.Now;
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _request.Status = (Status)comboBoxStatus.SelectedItem;
                _request.Result = string.IsNullOrEmpty(textBoxResult.Text) ? null : textBoxResult.Text;
                _request.DateCompletion = dateTimePickerCompletionDate.Checked ? dateTimePickerCompletionDate.Value : (DateTime?)null;

                await _requestService.UpdateAsync(_request);

                MessageBox.Show("Заявка успешно обновлена!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при обновлении заявки: {ex.Message}");
            }
        }
    }
}