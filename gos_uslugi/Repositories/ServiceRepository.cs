using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _connectionString;
        private readonly IRuleService _ruleService;
        public ServiceRepository(string connectionString, IRuleService ruleService)
        {
            _connectionString = connectionString;
            _ruleService = ruleService;
        }
        public async Task<Service> Save(Service service)
        {
            throw new NotImplementedException();
        }
        public async Task<Service> FindById(long serviceId)
        {
            Service service = null;
            string sqlQuery = @"SELECT id_service, description, start_date, end_date, instructions, price, id_administrator FROM service WHERE id_service = @id";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", serviceId);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                service = new Service
                                {
                                    Id = reader.GetInt64(0),
                                    Description = reader.GetString(1),
                                    StartDate = reader.GetDateTime(2),
                                    EndDate = reader.GetDateTime(3),
                                    Instructions = reader.GetString(4),
                                    Price = reader.GetDecimal(5),
                                    AdministratorId = reader.GetInt64(6),
                                    Rules = new List<ServiceRule>() // Initialize the Rules list
                                };

                                // Get the service rules
                                service.Rules = await _ruleService.GetServiceRules(serviceId);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return service;
        }

        public async Task<List<Service>> SearchServices(string searchQuery)
        {
            List<Service> services = new List<Service>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();

                    string sqlQuery = @"
                        SELECT 
                            s.id_service, s.description, s.start_date, s.end_date, s.price, s.instructions
                        FROM service s
                        WHERE s.description ILIKE @search";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@search", "%" + searchQuery + "%");

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Service service = new Service
                                {
                                    Id = reader.GetInt64(0),
                                    Description = reader.GetString(1),
                                    StartDate = reader.GetDateTime(2),
                                    EndDate = reader.GetDateTime(3),
                                    Price = reader.GetDecimal(4),
                                    Instructions = reader.GetString(5)
                                };
                                services.Add(service);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return services;
        }
        public async Task<List<GovernmentEmployee>> GetAllEmployees()
        {
            List<GovernmentEmployee> employees = new List<GovernmentEmployee>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                        SELECT ge.id_employee, a.full_name
                        FROM government_employee ge
                        INNER JOIN account a ON ge.id_account = a.id_account";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                GovernmentEmployee employee = new GovernmentEmployee
                                {
                                    Id = reader.GetInt64(0),
                                    FullName = reader.GetString(1)
                                };
                                employees.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка госслужащих: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return employees;
        }

        public async Task UpdateService(Service service)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        UPDATE service
                        SET description = @description,
                            instructions = @instructions,
                            price = @price,
                            start_date = @startDate,
                            end_date = @endDate
                        WHERE id_service = @id";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@description", service.Description);
                        command.Parameters.AddWithValue("@instructions", service.Instructions);
                        command.Parameters.AddWithValue("@price", service.Price);
                        command.Parameters.AddWithValue("@startDate", service.StartDate);
                        command.Parameters.AddWithValue("@endDate", service.EndDate);
                        command.Parameters.AddWithValue("@id", service.Id);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при обновлении услуги: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
        public async Task DeleteService(long serviceId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteRequestsQuery = "DELETE FROM request WHERE id_service = @id";
                        using (NpgsqlCommand commandDeleteRequests = new NpgsqlCommand(deleteRequestsQuery, connection, transaction))
                        {
                            commandDeleteRequests.Parameters.AddWithValue("@id", serviceId);
                            await commandDeleteRequests.ExecuteNonQueryAsync();
                        }

                        string deleteRulesQuery = "DELETE FROM service_rule WHERE id_service = @id";
                        using (NpgsqlCommand commandDeleteRules = new NpgsqlCommand(deleteRulesQuery, connection, transaction))
                        {
                            commandDeleteRules.Parameters.AddWithValue("@id", serviceId);
                            await commandDeleteRules.ExecuteNonQueryAsync();
                        }

                        string deleteServiceQuery = "DELETE FROM service WHERE id_service = @id";
                        using (NpgsqlCommand commandDeleteService = new NpgsqlCommand(deleteServiceQuery, connection, transaction))
                        {
                            commandDeleteService.Parameters.AddWithValue("@id", serviceId);
                            await commandDeleteService.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                    }
                    catch (NpgsqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при удалении услуги: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Произошла ошибка: {ex.Message}");
                    }
                }
            }
        }
    }
}