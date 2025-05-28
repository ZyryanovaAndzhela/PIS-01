using System;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi
{
    public partial class СозданиеНовогоАккаунта: Form
    {
        public СозданиеНовогоАккаунта()
        {
            InitializeComponent();
        }

        private void buttonДобавить_Click(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            string connectionString = "Server=localhost;Port=5433;Database=gos_uslugi;Username=postgres;Password=9943;";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string checkEmailQuery = "SELECT COUNT(*) FROM account WHERE login = @login";
                    using (NpgsqlCommand checkEmailCommand = new NpgsqlCommand(checkEmailQuery, connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@login", login);
                        long emailCount = (long)checkEmailCommand.ExecuteScalar();

                        if (emailCount > 0)
                        {
                            MessageBox.Show("Этот email (логин) уже используется. Пожалуйста, выберите другой.");
                            return;
                        }
                    }

                    string accountQuery = @"
                        INSERT INTO account (full_name, login, password, role)
                        VALUES (@fullName, @login, @password, 'user')
                        RETURNING id_account";

                    long accountId;

                    using (NpgsqlCommand accountCommand = new NpgsqlCommand(accountQuery, connection))
                    {
                        accountCommand.Parameters.AddWithValue("@fullName", fullName);
                        accountCommand.Parameters.AddWithValue("@login", login);
                        accountCommand.Parameters.AddWithValue("@password", password);
                        accountId = (long)accountCommand.ExecuteScalar(); 
                    }

                    string userQuery = @"
                        INSERT INTO user (id_account, citizen, passport, INN, purpose_visit, date_birth, phone_number, email)
                        VALUES (@accountId, @citizen, @passport, @INN, @purposeVisit, @dateBirth, @phoneNumber, @email)";

                    using (NpgsqlCommand userCommand = new NpgsqlCommand(userQuery, connection))
                    {
                        userCommand.Parameters.AddWithValue("@accountId", accountId);
                        userCommand.Parameters.AddWithValue("@citizen", citizen);
                        userCommand.Parameters.AddWithValue("@passport", passport);
                        userCommand.Parameters.AddWithValue("@INN", inn);
                        userCommand.Parameters.AddWithValue("@purposeVisit", purposeOfVisit);
                        userCommand.Parameters.AddWithValue("@dateBirth", dateBirth);
                        userCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        userCommand.Parameters.AddWithValue("@email", email);
                        userCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Аккаунт успешно создан!");
                    this.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при создании аккаунта: {ex.Message}");
            }
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
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
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
