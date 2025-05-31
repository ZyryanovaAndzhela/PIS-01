using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using gos_uslugi.Repositories;
using gos_uslugi.Services;

namespace gos_uslugi
{
    public partial class Услуги : Form
    {
        private List<Service> _services;
        private readonly Account _account;
        private readonly IServiceRepository _serviceRepository;

        public Услуги(Account account, IServiceRepository serviceRepository)
        {
            InitializeComponent();
            _account = account;
            _serviceRepository = serviceRepository;
            LoadServicesAsync();
        }

        private async Task LoadServicesAsync()
        {
            try
            {
                string searchQuery = textBoxServiceSearch.Text.Trim();
                _services = await _serviceRepository.SearchServices(searchQuery);

                listView1.Items.Clear();

                if (_services != null && _services.Count > 0)
                {
                    foreach (Service service in _services)
                    {
                        ListViewItem item = new ListViewItem(service.Description);
                        item.SubItems.Add(service.Price.ToString("C"));
                        item.Tag = service.Id;
                        listView1.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("В таблице service нет данных, соответствующих критериям поиска");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке услуг: {ex.Message}");
            }
        }

        private async void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                long serviceId = (long)selectedItem.Tag;

                RuleService ruleService = new RuleService(new RuleRepository());
                IServiceRepository serviceRepository = new ServiceRepository(ruleService);
                Service selectedService = await serviceRepository.FindById(serviceId);

                if (selectedService != null)
                {
                    ДеталиУслуги detailsForm = new ДеталиУслуги(selectedService, _account);
                    this.Hide();
                    detailsForm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Информация об услуге не найдена.");
                }
            }
        }

        private void buttonServiceSearch_Click(object sender, EventArgs e)
        {
            LoadServicesAsync();
        }
    }
}