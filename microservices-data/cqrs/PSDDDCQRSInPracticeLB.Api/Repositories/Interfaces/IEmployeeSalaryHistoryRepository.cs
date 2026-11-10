using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IEmployeeSalaryHistoryRepository
    {
        Task Add(EmployeeSalaryHistory salaryHistory);
    }
}
