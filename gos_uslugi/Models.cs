using System;
using System.Collections.Generic;

namespace gos_uslugi
{
    public class Account
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Foreigner : Account
    {
        public long Id { get; set; }
        public string Citizen { get; set; }
        public string Passport { get; set; }
        public string INN { get; set; }
        public string PurposeVisit { get; set; }
        public DateTime DateBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public class GovernmentEmployee : Account
    {
        public long Id { get; set; }
        public string Department { get; set; }
        public string Post { get; set; }
    }

    public class Administartor : Account
    {
        public long Id { get; set; }
        public string Role { get; set; }
    }

    public class Request
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long ForeignerId { get; set; }
        public long? ServiceId { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateCompletion { get; set; }
        public string Deadline { get; set; }
        public string Result { get; set; }
    }

    public class Service
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public long AdministratorId { get; set; }
        public string Instructions { get; set; }
        public List<ServiceRule> Rules { get; set; } = new List<ServiceRule>();
    }

    public class ServiceRule
    {
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public string Description { get; set; }
        public string ConditionValues { get; set; }
        public string ConditionType { get; set; }
        public string OperatorValues { get; set; }
        public int TermOfServiceProvision { get; set; }
    }

    public enum Status
    {
        Создана,
        Обрабатывается,
        Завершена,
        Отклонено
    }
}
