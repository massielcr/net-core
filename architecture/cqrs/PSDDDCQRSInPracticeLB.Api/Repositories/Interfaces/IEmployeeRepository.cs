using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee?> GetById(int id);
        Task<Employee> Create(Employee employee);
        Task Update(Employee employee);
    }
}
