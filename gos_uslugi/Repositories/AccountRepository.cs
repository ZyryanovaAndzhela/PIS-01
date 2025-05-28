using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString = "Server=localhost;Port=5433;Database=gos_uslugi;Username=postgres;Password=9943;";

        public AccountRepository() { }

        public async Task<Account> FindByLogin(string login)
        {
            string sqlQuery = "SELECT id_account, login, password, role, full_name FROM account WHERE login = @login";
            Account account = null;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                account = new Account
                                {
                                    Id = reader.GetInt64(0),
                                    Login = reader.GetString(1),
                                    Password = reader.GetString(2),
                                    Role = reader.GetString(3),
                                    FullName = reader.GetString(4)
                                };
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }

            return account;
        }

        public async Task<Account> Save(Account account)
        {
            // сохранение/обновление аккаунта
            throw new NotImplementedException();
        }

        public async Task<Account> FindById(long accountId)
        {
            // поиск аккаунта по ID
            throw new NotImplementedException();
        }
    }
}