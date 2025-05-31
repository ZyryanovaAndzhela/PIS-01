using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using gos_uslugi.Repositories;
using gos_uslugi.Services;

namespace gos_uslugi
{
    public partial class Заявки : Form
    {
        private List<Request> _request;
        private readonly Account _account;
        private readonly IRequestService _requestService;

        public Заявки(Account account, IRequestService requestService)
        {
            InitializeComponent();
            _request = new List<Request>();
            _account = account;
            _requestService = requestService;
        }

        private void Заявки_Load(object sender, EventArgs e)
        {
            comboBoxFilterStatus.Items.Clear();
            comboBoxFilterStatus.Items.Add("Все");
            foreach (Status status in Enum.GetValues(typeof(Status)))
            {
                comboBoxFilterStatus.Items.Add(status);
            }
            comboBoxFilterStatus.SelectedIndex = 0;
            if (_account.Role == "foreigner")
            {
                buttonCreateRequest.Visible = true;
            }
            else { buttonCreateRequest.Visible = false; }
        }

        private async Task LoadForeignerRequestsAsync()
        {
            listViewRequests.Items.Clear();
            _request.Clear();

            try
            {
                string filterStatus = comboBoxFilterStatus.SelectedItem?.ToString();
                string searchQuery = textBoxSearch.Text.Trim();

                _request = await _requestService.GetForeignerRequests(_account, filterStatus, searchQuery);

                foreach (Request request in _request)
                {
                    string serviceName = await GetServiceName(request.ServiceId);

                    ListViewItem item = new ListViewItem(request.Id.ToString());
                    item.SubItems.Add(serviceName);
                    item.SubItems.Add(request.DateCreation.ToShortDateString());
                    string dateCompletion = request.DateCompletion.HasValue ? request.DateCompletion.Value.ToShortDateString() : "";
                    item.SubItems.Add(dateCompletion);
                    item.SubItems.Add(request.Status.ToString());
                    item.Tag = request;
                    listViewRequests.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заявок: {ex.Message}");
            }
        }
        private async Task<string> GetServiceName(long? serviceId)
        {
            if (!serviceId.HasValue)
            {
                return "Удалена"; 
            }

            try
            {
                RuleService ruleService = new RuleService(new RuleRepository());
                IServiceRepository serviceRepository = new ServiceRepository(ruleService);
                Service service = await serviceRepository.FindById(serviceId.Value);

                return service.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения названия услуги: {ex.Message}");
                return "Ошибка получения названия";
            }
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            LoadForeignerRequestsAsync();
        }

        private void comboBoxFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadForeignerRequestsAsync();
        }

        private void buttonCreateRequest_Click(object sender, EventArgs e)
        {
            try
            {
                IRequestRepository requestRepository = new RequestRepository();
                НоваяЗаявка newRequestForm = new НоваяЗаявка(_account, _requestService, requestRepository);
                this.Hide();
                newRequestForm.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void listViewRequests_MouseClick(object sender, MouseEventArgs e)
        {
            if (listViewRequests.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewRequests.SelectedItems[0];
                Request selectedRequest = selectedItem.Tag as Request;

                if (selectedRequest != null)
                {
                    IRequestService requestService = new RequestService(new RequestRepository());
                    ДеталиЗаявки detailsForm = new ДеталиЗаявки(selectedRequest, _account, requestService);
                    this.Hide();
                    detailsForm.ShowDialog();
                    this.Show();
                }
            }
        }
    }
}