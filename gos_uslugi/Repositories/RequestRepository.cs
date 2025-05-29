using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace gos_uslugi.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly string _connectionString;

        public RequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<(string EmployeeName, string ForeignerName, string ServiceDescription)> GetRequestDetails(long requestId)
        {
            (string EmployeeName, string ForeignerName, string ServiceDescription) details = (null, null, null);

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT
                            CASE
                                WHEN r.id_foreigner IS NOT NULL THEN a_usr.full_name
                                WHEN r.id_employee IS NOT NULL THEN a_emp.full_name
                                ELSE NULL
                            END AS ForeignerName,
                            a_emp.full_name AS EmployeeName,
                            s.description AS ServiceDescription
                        FROM request r
                        LEFT JOIN government_employee ge ON r.id_employee = ge.id_employee
                        LEFT JOIN account a_emp ON ge.id_account = a_emp.id_account
                        LEFT JOIN foreigner f ON r.id_foreigner = f.id_foreigner
                        LEFT JOIN account a_usr ON f.id_account = a_usr.id_account
                        LEFT JOIN service s ON r.id_service = s.id_service
                        WHERE r.id_request = @requestId";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@requestId", requestId);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                details = (
                                    reader.IsDBNull(1) ? null : reader.GetString(1),
                                    reader.IsDBNull(0) ? null : reader.GetString(0),
                                    reader.IsDBNull(2) ? null : reader.GetString(2)
                                );
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }

            return details;
        }
        
        public async Task<Request> Save(Request request)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"INSERT INTO request (id_employee, id_foreigner, id_service, status, date_creation, date_completion, deadline, result)
                            VALUES (@EmployeeId, @ForeignerId, @ServiceId, @Status, @DateCreation, @DateCompletion, @Deadline, @Result)
                            RETURNING id_request;";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("EmployeeId", request.EmployeeId);
                    cmd.Parameters.AddWithValue("ForeignerId", request.ForeignerId);
                    cmd.Parameters.AddWithValue("ServiceId", request.ServiceId);
                    cmd.Parameters.AddWithValue("Status", request.Status.ToString());
                    cmd.Parameters.AddWithValue("DateCreation", request.DateCreation);
                    cmd.Parameters.AddWithValue("DateCompletion", request.DateCompletion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Deadline", request.Deadline ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Result", request.Result ?? (object)DBNull.Value);

                    request.Id = (long)await cmd.ExecuteScalarAsync();
                }
            }

            return request;
        }

        public async Task UpdateAsync(Request request)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"UPDATE request 
                        SET status = @Status, 
                            date_completion = @DateCompletion, 
                            deadline = @Deadline, 
                            result = @Result
                        WHERE id_request = @Id";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", request.Id);
                    cmd.Parameters.AddWithValue("@Status", request.Status.ToString());
                    cmd.Parameters.AddWithValue("@DateCompletion", request.DateCompletion.HasValue ? (object)request.DateCompletion.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Deadline", request.Deadline ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Result", request.Result ?? (object)DBNull.Value);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Request> FindById(long requestId)
        {
            Request request = null;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT id, employeeid, foreignerid, serviceid, status, datecreation, datecompletion, deadline, result FROM request WHERE id = @RequestId";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("RequestId", requestId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            request = new Request()
                            {
                                Id = (long)reader["id"],
                                EmployeeId = (long)reader["employeeid"],
                                ForeignerId = (long)reader["foreignerid"],
                                ServiceId = (long)reader["serviceid"],
                                Status = (Status)Enum.Parse(typeof(Status), (string)reader["status"]),
                                DateCreation = (DateTime)reader["datecreation"],
                                DateCompletion = reader["datecompletion"] == DBNull.Value ? null : (DateTime?)reader["datecompletion"],
                                Deadline = reader["deadline"] == DBNull.Value ? null : (string)reader["deadline"],
                                Result = reader["result"] == DBNull.Value ? null : (string)reader["result"]
                            };
                        }
                    }

                }

            }
            return request;
        }

        public async Task<List<Request>> GetForeignerRequests(Account account, string filterStatus, string searchQuery)
        {
            List<Request> requests = new List<Request>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
SELECT r.id_request, r.id_employee, r.id_foreigner, r.id_service, r.status, r.date_creation, r.date_completion, r.deadline, r.result, s.description
FROM request r
LEFT JOIN service s ON r.id_service = s.id_service
LEFT JOIN foreigner f ON r.id_foreigner = f.id_foreigner
LEFT JOIN government_employee ge ON r.id_employee = ge.id_employee
WHERE 
    ((@role = 'employee' AND r.id_employee IN (SELECT ge.id_employee FROM government_employee ge WHERE ge.id_account = @accountId)) AND (@filterStatus IS NULL OR r.status = @filterStatus) AND (@search IS NULL OR r.id_request::TEXT ILIKE @search OR s.description ILIKE @search))
    OR 
    ((@role = 'foreigner' AND f.id_account = @accountId) AND (@filterStatus IS NULL OR r.status = @filterStatus) AND (@search IS NULL OR r.id_request::TEXT ILIKE @search OR s.description ILIKE @search))";

                    List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
                    parameters.Add(new NpgsqlParameter("@accountId", account.Id));
                    parameters.Add(new NpgsqlParameter("@role", account.Role));

                    NpgsqlParameter statusParameter = new NpgsqlParameter("@filterStatus", NpgsqlTypes.NpgsqlDbType.Text);
                    statusParameter.Value = string.IsNullOrEmpty(filterStatus) || filterStatus == "Все"
                        ? (object)DBNull.Value
                        : filterStatus;
                    parameters.Add(statusParameter);

                    NpgsqlParameter searchParameter = new NpgsqlParameter("@search", NpgsqlTypes.NpgsqlDbType.Text);
                    searchParameter.Value = string.IsNullOrEmpty(searchQuery) ? (object)DBNull.Value : "%" + searchQuery + "%";
                    parameters.Add(searchParameter);


                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {  
                            while (await reader.ReadAsync())
                            {
                                long? serviceId = reader["id_service"] == DBNull.Value ? null : (long?)reader.GetInt64(reader.GetOrdinal("id_service"));
                                Request request = new Request
                                {
                                    Id = reader.GetInt64(0),
                                    EmployeeId = reader.GetInt64(1),
                                    ForeignerId = reader.GetInt64(2),
                                    ServiceId = serviceId,
                                    Status = (Status)Enum.Parse(typeof(Status), reader.GetString(4)),
                                    DateCreation = reader.GetDateTime(5),
                                    DateCompletion = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                    Deadline = reader.GetString(7),
                                    Result = reader.IsDBNull(8) ? null : reader.GetString(8)
                                };
                                requests.Add(request);
                                
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }

            return requests;
        }

        public async Task<List<Service>> GetAllServices(string citizen, string purposeVisit)
        {
            List<Service> services = new List<Service>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    SELECT s.id_service, s.description, s.start_date, s.end_date, s.price, s.id_administrator, s.instructions,
                           sr.id_service_rule AS rule_id, sr.id_service, sr.description AS rule_description, sr.condition_values, sr.condition_type, sr.operator_values, sr.term_of_service_provision
                    FROM service s
                    LEFT JOIN service_rule sr ON s.id_service = sr.id_service";
                Debug.WriteLine($"SQL: {sql}");

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        int rowCount = 0;
                        while (await reader.ReadAsync())
                        {
                            rowCount++;
                            long serviceId = reader.GetInt64(reader.GetOrdinal("id_service"));
                            string description = reader.GetString(reader.GetOrdinal("description"));
                            Debug.WriteLine($"Service ID: {serviceId}, Description: {description}");

                            Service service = services.FirstOrDefault(s => s.Id == serviceId);
                            if (service == null)
                            {
                                service = new Service
                                {
                                    Id = serviceId,
                                    Description = description,
                                    StartDate = reader.GetDateTime(reader.GetOrdinal("start_date")),
                                    EndDate = reader.GetDateTime(reader.GetOrdinal("end_date")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                    AdministratorId = reader.GetInt64(reader.GetOrdinal("id_administrator")),
                                    Instructions = reader.GetString(reader.GetOrdinal("instructions")),
                                    Rules = new List<ServiceRule>()
                                };
                                services.Add(service);
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("rule_id")))
                            {
                                ServiceRule rule = new ServiceRule
                                {
                                    Id = reader.GetInt64(reader.GetOrdinal("rule_id")),
                                    ServiceId = serviceId,
                                    Description = reader.GetString(reader.GetOrdinal("rule_description")),
                                    ConditionValues = reader.GetString(reader.GetOrdinal("condition_values")),
                                    ConditionType = reader.GetString(reader.GetOrdinal("condition_type")),
                                    OperatorValues = reader.GetString(reader.GetOrdinal("operator_values")),
                                    TermOfServiceProvision = reader.GetInt32(reader.GetOrdinal("term_of_service_provision"))
                                };
                                service.Rules.Add(rule);
                            }
                        }
                    }
                }

                foreach (var service in services)
                {
                    if (service.Rules != null)
                    {
                        service.Rules = service.Rules.OrderBy(r => {
                            int order = 5;

                            if (r.ConditionType == "Гражданство" && r.ConditionValues == citizen)
                                order = 1;
                            else if (r.ConditionType == "Работа" && r.ConditionValues == purposeVisit)
                                order = 2;
                            else if (r.ConditionType == "Гражданство" && r.ConditionValues == "ALL")
                                order = 3;
                            else if (r.ConditionType == "Гос. программа переселения соотечественников" && r.ConditionValues == "ALL")
                                order = 4;

                            return order;
                        }).ToList();
                    }
                }

                return services;
            }
        }

        public async Task<List<ServiceRule>> GetServiceRules(long serviceId, string citizen, string purposeVisit)
        {
            List<ServiceRule> rules = new List<ServiceRule>();
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    SELECT id_service_rule, id_service, description, condition_values, condition_type, operator_values, term_of_service_provision
                    FROM service_rule
                    WHERE id_service = @ServiceId
                      AND (
                        (condition_type = 'Гражданство' AND condition_values = @Citizen) OR
                        (condition_type = 'Работа' AND condition_values = @PurposeVisit) OR
                        (condition_type = 'Гос. программа переселения соотечественников' AND condition_values = 'ALL') OR
                        (condition_type = 'Гражданство' AND condition_values = 'ALL')
                      )
                    ORDER BY
                        CASE
                            WHEN condition_type = 'Гражданство' AND condition_values = @Citizen THEN 1
                            WHEN condition_type = 'Работа' AND condition_values = @PurposeVisit THEN 2
                            WHEN condition_type = 'Гражданство' AND condition_values = 'ALL' THEN 3 -- переместили это условие
                            WHEN condition_type = 'Гос. программа переселения соотечественников' AND condition_values = 'ALL' THEN 4
                            ELSE 5
                        END;
                ";
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ServiceId", serviceId);
                    command.Parameters.AddWithValue("@Citizen", citizen);
                    command.Parameters.AddWithValue("@PurposeVisit", purposeVisit);

                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ServiceRule rule = new ServiceRule
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("id_service_rule")),
                                ServiceId = reader.GetInt64(reader.GetOrdinal("id_service")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                ConditionValues = reader.GetString(reader.GetOrdinal("condition_values")),
                                ConditionType = reader.GetString(reader.GetOrdinal("condition_type")),
                                OperatorValues = reader.GetString(reader.GetOrdinal("operator_values")),
                                TermOfServiceProvision = reader.GetInt32(reader.GetOrdinal("term_of_service_provision"))
                            };
                            rules.Add(rule);
                        }
                    }
                }
            }
            return rules;
        }

        public async Task<Foreigner> GetForeignerByAccountId(long accountId)
        {
            Foreigner foreigner = null;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"SELECT id_foreigner, id_account, citizen, passport, INN, purpose_visit, date_birth, phone_number, email FROM foreigner WHERE id_account = @accountId";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountId", accountId);

                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                foreigner = new Foreigner
                                {
                                    Id = reader.GetInt64(0),
                                    Citizen = reader.GetString(2),
                                    Passport = reader.GetString(3),
                                    INN = reader.GetString(4),
                                    PurposeVisit = reader.GetString(5),
                                    DateBirth = reader.GetDateTime(6),
                                    PhoneNumber = reader.GetString(7),
                                    Email = reader.GetString(8)
                                };
                            }
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Ошибка подключения: {ex.Message}");
                throw;
            }

            return foreigner;
        }

        public async Task CreateRequest(Request request)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
                INSERT INTO request (id_employee, id_foreigner, id_service, status, date_creation, date_completion, deadline, result)
                VALUES (@EmployeeId, @ForeignerId, @ServiceId, @Status, @DateCreation, @DateCompletion, @Deadline, @Result);
            ";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", request.EmployeeId);
                    command.Parameters.AddWithValue("@ForeignerId", request.ForeignerId);
                    command.Parameters.AddWithValue("@ServiceId", request.ServiceId);
                    command.Parameters.AddWithValue("@Status", request.Status.ToString());
                    command.Parameters.AddWithValue("@DateCreation", request.DateCreation);
                    command.Parameters.AddWithValue("@DateCompletion", request.DateCompletion.HasValue ? (object)request.DateCompletion.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@Deadline", request.Deadline);
                    command.Parameters.AddWithValue("@Result", string.IsNullOrEmpty(request.Result) ? (object)DBNull.Value : request.Result);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}