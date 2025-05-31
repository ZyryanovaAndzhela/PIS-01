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
        private Status _originalStatus;

        public РедактированиеЗаявки(Request request, Account account, IRequestService requestService)
        {
            InitializeComponent();
            _request = request;
            _account = account;
            _requestService = requestService;
            InitializeStatusComboBox();
            LoadRequestData();
            _originalStatus = _request.Status;
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

        private void LoadRequestData()
        {
            comboBoxStatus.SelectedItem = _request.Status;
            UpdateCompletionDateVisibility();

            textBoxResult.Text = _request.Result ?? "";
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCompletionDateVisibility();
        }

        private void UpdateCompletionDateVisibility()
        {
            Status selectedStatus = (Status)comboBoxStatus.SelectedItem;
            bool isCompletedOrRejected = selectedStatus == Status.Завершена || selectedStatus == Status.Отклонено;

            label3.Visible = isCompletedOrRejected;
            dateTimePickerCompletionDate.Visible = isCompletedOrRejected;

            if (isCompletedOrRejected)
            {
                if (_request.DateCompletion.HasValue)
                {
                    dateTimePickerCompletionDate.Checked = true;
                    dateTimePickerCompletionDate.Value = _request.DateCompletion.Value;
                }
                else
                {
                    dateTimePickerCompletionDate.Checked = true;
                    dateTimePickerCompletionDate.Value = DateTime.Now;
                    _request.DateCompletion = DateTime.Now; 
                }
            }
            else
            {
                dateTimePickerCompletionDate.Checked = false;
                _request.DateCompletion = null;
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _request.Status = (Status)comboBoxStatus.SelectedItem;
                _request.Result = string.IsNullOrEmpty(textBoxResult.Text) ? null : textBoxResult.Text;

                if (dateTimePickerCompletionDate.Checked)
                {
                    _request.DateCompletion = dateTimePickerCompletionDate.Value;
                }
                else
                {
                    _request.DateCompletion = null;
                }

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