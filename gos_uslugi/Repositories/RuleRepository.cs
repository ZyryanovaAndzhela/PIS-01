using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gos_uslugi.Repositories
{
    class RuleRepository : IRuleRepository
    {
        private readonly string _connectionString;
        public RuleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<ServiceRule>> GetServiceRules(long serviceId)
        {
            List<ServiceRule> rules = new List<ServiceRule>();
            string sqlQuery = @"SELECT id_service_rule, id_service, description, condition_values, condition_type, operator_values, term_of_service_provision 
                                FROM service_rule 
                                WHERE id_service = @id";

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
                            while (await reader.ReadAsync())
                            {
                                ServiceRule rule = new ServiceRule
                                {
                                    Id = reader.GetInt64(0),
                                    ServiceId = reader.GetInt64(1),
                                    Description = reader.GetString(2),
                                    ConditionValues = reader.GetString(3),
                                    ConditionType = reader.GetString(4),
                                    OperatorValues = reader.GetString(5),
                                    TermOfServiceProvision = reader.GetInt32(6)
                                };
                                rules.Add(rule);
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при загрузке правил услуги: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return rules;
        }

        public async Task<ServiceRule> SaveServiceRule(ServiceRule serviceRule)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();

                    string sqlQuery = @"
                    INSERT INTO service_rule (id_service, description, condition_values, condition_type, operator_values, term_of_service_provision)
                    VALUES (@id_service, @description, @condition_values, @condition_type, @operator_values, @term_of_service_provision)
                    RETURNING id_service_rule";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id_service", serviceRule.ServiceId);
                        command.Parameters.AddWithValue("@description", serviceRule.Description);
                        command.Parameters.AddWithValue("@condition_values", serviceRule.ConditionValues);
                        command.Parameters.AddWithValue("@condition_type", serviceRule.ConditionType);
                        command.Parameters.AddWithValue("@operator_values", serviceRule.OperatorValues);
                        command.Parameters.AddWithValue("@term_of_service_provision", serviceRule.TermOfServiceProvision);

                        long newId = await command.ExecuteScalarAsync() as long? ?? 0;
                        serviceRule.Id = newId;
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при сохранении правила услуги: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

            return serviceRule;
        }

        public async Task UpdateServiceRule(ServiceRule serviceRule)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionString))
                {
                    await connection.OpenAsync();

                    string sqlQuery = @"
                    UPDATE service_rule
                    SET description = @description,
                        condition_values = @condition_values,
                        condition_type = @condition_type,
                        operator_values = @operator_values,
                        term_of_service_provision = @term_of_service_provision
                    WHERE id_service_rule = @id";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@description", serviceRule.Description);
                        command.Parameters.AddWithValue("@condition_values", serviceRule.ConditionValues);
                        command.Parameters.AddWithValue("@condition_type", serviceRule.ConditionType);
                        command.Parameters.AddWithValue("@operator_values", serviceRule.OperatorValues);
                        command.Parameters.AddWithValue("@term_of_service_provision", serviceRule.TermOfServiceProvision);
                        command.Parameters.AddWithValue("@id", serviceRule.Id);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка при обновлении правила услуги: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
