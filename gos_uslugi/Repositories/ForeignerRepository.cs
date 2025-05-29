using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi.Repositories
{
    public class ForeignerRepository : IForeignerRepository
    {
        public async Task<Foreigner> FindById(long foreignerId)
        {
            Foreigner foreigner = null;

            using (var connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
            {
                await connection.OpenAsync();

                string sql = @"
                    SELECT 
                        a.id_account, a.login, a.full_name, a.password, a.role,
                        f.id_foreigner, f.citizen, f.passport, f.INN, f.purpose_visit, f.date_birth, f.phone_number, f.email
                    FROM account a
                    INNER JOIN foreigner f ON a.id_account = f.id_account
                    WHERE f.id_foreigner = @foreignerId";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@foreignerId", foreignerId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            foreigner = new Foreigner
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("id_account")),
                                Login = reader.GetString(reader.GetOrdinal("login")),
                                FullName = reader.GetString(reader.GetOrdinal("full_name")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                Role = reader.GetString(reader.GetOrdinal("role")),

                                Citizen = reader.GetString(reader.GetOrdinal("citizen")),
                                Passport = reader.GetString(reader.GetOrdinal("passport")),
                                INN = reader.GetString(reader.GetOrdinal("INN")),
                                PurposeVisit = reader.GetString(reader.GetOrdinal("purpose_visit")),
                                DateBirth = reader.GetDateTime(reader.GetOrdinal("date_birth")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("phone_number")),
                                Email = reader.GetString(reader.GetOrdinal("email"))
                            };
                        }
                    }
                }

                return foreigner;
            }
        }
        public async Task<bool> IsEmailAlreadyRegistered(string email, long currentAccountId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
            {
                await connection.OpenAsync();

                string sqlQuery = @"
                    SELECT EXISTS (
                        SELECT 1
                        FROM foreigner
                        WHERE email = @email AND id_account != @currentAccountId
                    );";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@currentAccountId", currentAccountId);

                    bool exists = (bool)await command.ExecuteScalarAsync();
                    return exists;
                }
            }
        }
        public async Task<Foreigner> GetForeignerByLogin(string login)
        {
            string sqlQuery = @"
                SELECT f.id_foreigner, f.citizen, f.passport, f.inn, f.purpose_visit, f.date_birth, f.phone_number, f.email, a.id_account, a.login, a.full_name, a.password, a.role
                FROM foreigner f
                INNER JOIN account a ON f.id_account = a.id_account
                WHERE a.login = @login";

            Foreigner foreigner = null;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                foreigner = new Foreigner
                                {
                                    Id = reader.GetInt64(0),
                                    Citizen = reader.GetString(1),
                                    Passport = reader.GetString(2),
                                    INN = reader.GetString(3),
                                    PurposeVisit = reader.GetString(4),
                                    DateBirth = reader.GetDateTime(5),
                                    PhoneNumber = reader.GetString(6),
                                    Email = reader.GetString(7),
                                    Login = reader.GetString(9),
                                    FullName = reader.GetString(10),
                                    Password = reader.GetString(11),
                                    Role = reader.GetString(12)
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

            return foreigner;
        }

        public async Task<Foreigner> Save(Foreigner foreigner)
        {
            using (var connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sqlAccount = @"
                        UPDATE account 
                        SET login = @login, full_name = @fullName, password = @password
                        WHERE id_account = @accountId";

                        using (var cmdAccount = new NpgsqlCommand(sqlAccount, connection, transaction))
                        {
                            cmdAccount.Parameters.AddWithValue("@accountId", foreigner.Id);
                            cmdAccount.Parameters.AddWithValue("@login", foreigner.Login);
                            cmdAccount.Parameters.AddWithValue("@fullName", foreigner.FullName);
                            cmdAccount.Parameters.AddWithValue("@password", foreigner.Password);

                            await cmdAccount.ExecuteNonQueryAsync();
                        }

                        string sqlForeigner = @"
                        UPDATE foreigner 
                        SET citizen = @citizen, passport = @passport, INN = @INN, 
                            purpose_visit = @purposeVisit, date_birth = @dateBirth, 
                            phone_number = @phoneNumber, email = @email
                        WHERE id_account = @accountId";

                        using (var cmdForeigner = new NpgsqlCommand(sqlForeigner, connection, transaction))
                        {
                            cmdForeigner.Parameters.AddWithValue("@accountId", foreigner.Id); 
                            cmdForeigner.Parameters.AddWithValue("@citizen", foreigner.Citizen);
                            cmdForeigner.Parameters.AddWithValue("@passport", foreigner.Passport);
                            cmdForeigner.Parameters.AddWithValue("@INN", foreigner.INN);
                            cmdForeigner.Parameters.AddWithValue("@purposeVisit", foreigner.PurposeVisit);
                            cmdForeigner.Parameters.AddWithValue("@dateBirth", foreigner.DateBirth);
                            cmdForeigner.Parameters.AddWithValue("@phoneNumber", foreigner.PhoneNumber);
                            cmdForeigner.Parameters.AddWithValue("@email", foreigner.Email);

                            await cmdForeigner.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Ошибка при сохранении данных Foreigner: {ex.Message}");
                        throw;
                    }
                }
            }

            return foreigner;
        }
    }
}