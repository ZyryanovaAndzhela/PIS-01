using System;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi
{
    public partial class СозданиеНовогоАккаунта : Form
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IForeignerRepository _foreignerRepository;
        private readonly IForeignerService _foreignerService;

        public СозданиеНовогоАккаунта(IAccountRepository accountRepository, IForeignerRepository foreignerRepository, IForeignerService foreignerService)
        {
            InitializeComponent();
            _accountRepository = accountRepository;
            _foreignerRepository = foreignerRepository;
            _foreignerService = foreignerService;
        }

        private async void buttonДобавить_Click(object sender, EventArgs e)
        {
            string fullName = textBoxFIO.Text;
            string phoneNumber = textBoxPhone.Text;
            string inn = textBoxINN.Text;
            string citizen = textBoxCitizen.Text;
            string purposeOfVisit = textBoxPurpose.Text;
            string passport = textBoxPassport.Text;
            DateTime dateBirth = dateTimePickerDateBirth.Value;
            string email = textBoxEmail.Text;
            string login = email;
            string password = textBoxPassword.Text;

            bool loginAlreadyRegistered = await _accountRepository.IsLoginAlreadyRegistered(login, 0);
            if (loginAlreadyRegistered)
            {
                MessageBox.Show("Аккаунт с таким email уже существует. Укажите другой email.");
                return;
            }

            try
            {
                Account newAccount = new Account
                {
                    FullName = fullName,
                    Login = login,
                    Password = password,
                    Role = "foreigner"
                };

                newAccount = await _accountRepository.Save(newAccount);

                Foreigner newForeigner = new Foreigner
                {
                    Id = newAccount.Id,
                    Citizen = citizen,
                    Passport = passport,
                    INN = inn,
                    PurposeVisit = purposeOfVisit,
                    DateBirth = dateBirth,
                    PhoneNumber = phoneNumber,
                    Email = email
                };

                await _foreignerRepository.Save(newForeigner);

                MessageBox.Show("Аккаунт успешно создан!");
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при создании аккаунта: {ex.Message}");
            }
            this.Close();
        }

        private void textBoxFIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (textBoxPhone.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxINN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (textBoxINN.Text.Length >= 12 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxCitizen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxPurpose_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (textBoxPassport.Text.Length >= 6 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}