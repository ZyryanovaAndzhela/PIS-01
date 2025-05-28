using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi.Repositories
{
    public class ForeignerRepository : IForeignerRepository
    {
        public Task Delete(long foreignerId)
        {
            throw new NotImplementedException();
        }

        public Task<Foreigner> FindById(long foreignerId)
        {
            throw new NotImplementedException();
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

        public Task<Foreigner> Save(Foreigner foreigner)
        {
            throw new NotImplementedException();
        }
    }
}