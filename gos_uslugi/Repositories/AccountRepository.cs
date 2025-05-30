using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString = "Server=localhost;Port=5433;Database=gos_uslugi;Username=postgres;Password=9943;";

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var existingAccount = await FindById(account.Id);

                if (existingAccount == null)
                {
                    string sql = @"INSERT INTO account (login, full_name, password, role) 
                               VALUES (@login, @fullName, @password, @role)
                               RETURNING id_account";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", account.Login);
                        cmd.Parameters.AddWithValue("@fullName", account.FullName);
                        cmd.Parameters.AddWithValue("@password", account.Password);
                        cmd.Parameters.AddWithValue("@role", account.Role);

                        account.Id = (long)await cmd.ExecuteScalarAsync();
                    }
                }
                else
                {
                    string sql = @"UPDATE account 
                               SET login = @login,
                                   full_name = @fullName, 
                                   password = @password
                               WHERE id_account = @accountId";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@accountId", account.Id);
                        cmd.Parameters.AddWithValue("@login", account.Login);
                        cmd.Parameters.AddWithValue("@fullName", account.FullName);
                        cmd.Parameters.AddWithValue("@password", account.Password);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            return account;
        }

        public async Task<Account> FindById(long accountId)
        {
            Account account = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT id_account, login, full_name, password, role FROM account WHERE id_account = @accountId";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            account = new Account
                            {
                                Id = reader.GetInt64(0),
                                Login = reader.GetString(1),
                                FullName = reader.GetString(2),
                                Password = reader.GetString(3),
                                Role = reader.GetString(4)
                            };
                        }
                    }
                }
            }
            return account;
        }
    }
}