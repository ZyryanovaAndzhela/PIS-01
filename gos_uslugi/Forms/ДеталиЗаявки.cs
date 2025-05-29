using gos_uslugi.Repositories;
using gos_uslugi.Services;
using Npgsql;
using System;
using System.Windows.Forms;

namespace gos_uslugi
{
    public partial class ДеталиЗаявки : Form
    {
        public Request Request { get; set; }
        private Account _account;
        private readonly IRequestService _requestService; 
        private readonly string _connectionString;

        public ДеталиЗаявки(Request request, Account account, IRequestService requestService, string connectionString)
        {
            InitializeComponent();
            Request = request;
            _account = account;
            _requestService = requestService; 
            _connectionString = connectionString;
            LoadRequestDetails();

            buttonEditRequest.Visible = _account.Role == "employee";
        }

        public async void LoadRequestDetails()
        {
            if (Request != null)
            {
                try
                {
                    var details = await _requestService.GetRequestDetails(Request.Id);

                    if (details.EmployeeName != null || details.ForeignerName != null || details.ServiceDescription != null)
                    {
                        labelEmployeeId.Text = "Сотрудник: " + (details.EmployeeName ?? "Не указан");
                        labelForeignerId.Text = "Пользователь: " + (details.ForeignerName ?? "Не указан");
                        labelServiceId.Text = "Услуга: " + (details.ServiceDescription ?? "Не указана");
                    }
                    else
                    {
                        labelEmployeeId.Text = "Сотрудник: Не указан";
                        labelForeignerId.Text = "Пользователь: Не указан";
                        labelServiceId.Text = "Услуга: Не указана";
                    }

                    labelID.Text = "ID: " + Request.Id.ToString();
                    textBoxStatus.Text = Request.Status.ToString();
                    dateTimePickerCreation.Value = Request.DateCreation;

                    dateTimePickerCompletion.Visible = Request.DateCompletion.HasValue;
                    if (Request.DateCompletion.HasValue)
                    {
                        dateTimePickerCompletion.Value = Request.DateCompletion.Value;
                    }
                    textBoxDeadline.Text = Request.Deadline;
                    richTextBoxResult.Text = Request.Result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузке деталей заявки: {ex.Message}");
                }
            }
        }

        private void buttonEditRequest_Click(object sender, EventArgs e)
        {
            IRequestService requestService = new RequestService(new RequestRepository(ConfigurationManager.ConnectionString));
            РедактированиеЗаявки editForm = new РедактированиеЗаявки(Request, _account, _requestService, _connectionString);
            this.Hide();
            DialogResult result = editForm.ShowDialog();
            this.Show();
            if (result == DialogResult.OK)
            {
                LoadRequestDetails();
            }
        }
    }
}