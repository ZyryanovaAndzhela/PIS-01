using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using Npgsql;

namespace gos_uslugi
{
    public partial class ЛичныйКабинет : Form
    {
        private readonly Account _account;
        private readonly IForeignerService _foreignerService;
        private readonly IAccountRepository _accountRepository;

        private string _originalPassword;
        private string _originalFullName;
        private string _originalCitizen;
        private string _originalPassport;
        private string _originalINN;
        private string _originalPurposeVisit;
        private DateTime _originalDateBirth;
        private string _originalPhoneNumber;
        private string _originalEmail;
        private char _originalPasswordChar;

        public ЛичныйКабинет(Account account, IForeignerService foreignerService, IAccountRepository accountRepository)
        {
            InitializeComponent();
            _account = account;
            _foreignerService = foreignerService;
            _accountRepository = accountRepository;

            LoadUserDataAsync();
            InitializeReadOnly();
        }

        private async Task LoadUserDataAsync()
        {
            try
            {
                string login = _account.Login;

                try
                {
                    Account account = await _accountRepository.FindByLogin(login);

                    if (account != null)
                    {
                        Foreigner foreigner = await _foreignerService.GetForeignerByLogin(login);

                        if (foreigner != null)
                        {
                            textBox1.Text = account.FullName;
                            textBoxPassword.Text = account.Password;
                            textBox2.Text = foreigner.Email;
                            textBox3.Text = foreigner.PhoneNumber;
                            textBox4.Text = foreigner.INN;
                            textBox5.Text = foreigner.Citizen;
                            textBox6.Text = foreigner.PurposeVisit;
                            textBox8.Text = foreigner.Passport;
                            dateTimePicker1.Value = foreigner.DateBirth; 

                            _originalFullName = account.FullName;
                            _originalEmail = foreigner.Email;
                            _originalPhoneNumber = foreigner.PhoneNumber;
                            _originalINN = foreigner.INN;
                            _originalCitizen = foreigner.Citizen;
                            _originalPurposeVisit = foreigner.PurposeVisit;
                            _originalPassport = foreigner.Passport;
                            _originalDateBirth = foreigner.DateBirth;
                            _originalPassword = account.Password;
                        }
                        else
                        {
                            MessageBox.Show("Foreigner не найден.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account не найден.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void InitializeReadOnly()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            dateTimePicker1.Enabled = false;
            textBox8.ReadOnly = true;
            textBoxPassword.ReadOnly = true;
            button3.Visible = false;
            button2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _originalFullName = textBox1.Text;
            _originalEmail = textBox2.Text;
            _originalPhoneNumber = textBox3.Text;
            _originalINN = textBox4.Text;
            _originalCitizen = textBox5.Text;
            _originalPurposeVisit = textBox6.Text;
            _originalPassport = textBox8.Text;
            _originalDateBirth = dateTimePicker1.Value;
            _originalPassword = textBoxPassword.Text;

            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            dateTimePicker1.Enabled = true;
            textBoxPassword.ReadOnly = false;
            textBox8.ReadOnly = false;

            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = _originalFullName;
            textBox2.Text = _originalEmail;
            textBox3.Text = _originalPhoneNumber;
            textBox4.Text = _originalINN;
            textBox5.Text = _originalCitizen;
            textBox6.Text = _originalPurposeVisit;
            textBox8.Text = _originalPassport;
            textBoxPassword.Text = _originalPassword;
            dateTimePicker1.Value = _originalDateBirth;

            InitializeReadOnly();
            button1.Visible = true;
            textBoxPassword_Leave(textBoxPassword, EventArgs.Empty);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await SaveUserDataAsync();
            textBoxPassword_Leave(textBoxPassword, EventArgs.Empty);
        }

        private async Task SaveUserDataAsync()
        {
            try
            {
                string newFullName = textBox1.Text;
                string newEmail = textBox2.Text;
                string newPhoneNumber = textBox3.Text;
                string newINN = textBox4.Text;
                string newCitizen = textBox5.Text;
                string newPassport = textBox8.Text;
                string newPassword = textBoxPassword.Text;
                string newLogin = textBox2.Text;

                if (await _accountRepository.IsLoginAlreadyRegistered(newLogin, _account.Id))
                {
                    MessageBox.Show("Этот логин уже зарегистрирован");
                    return;
                }

                await _foreignerService.UpdateForeignerInfo(_account.Id, newFullName, newEmail, newPhoneNumber, newINN, newCitizen, newPassport, newPassword, newLogin);

                _account.FullName = newFullName;
                _account.Login = newLogin;
                _account.Password = newPassword;
                await _accountRepository.Save(_account);

                MessageBox.Show("Данные успешно обновлены!");

                _originalFullName = newFullName;
                _originalEmail = newEmail;
                _originalPhoneNumber = newPhoneNumber;
                _originalINN = newINN;
                _originalCitizen = newCitizen;
                _originalPassport = newPassport;
                _originalPassword = newPassword;
                _account.Login = newLogin;

                InitializeReadOnly();
                button1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (textBox3.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (textBox4.Text.Length >= 12 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (textBox8.Text.Length >= 6 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            _originalPasswordChar = textBoxPassword.PasswordChar;
            textBoxPassword.PasswordChar = '\0';
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = _originalPasswordChar;
        }
    }
}