using IConText.Data.Model;

namespace IConText.Data
{
    public interface IStorage
    {
        int HighestId { get; }
        void Create(Employee employee);
        IEnumerable<Employee> Read(Func<Employee, bool> predicate = null);
        void Update(Employee overwriteData, Func<Employee, bool> predicate);
        IEnumerable<Employee> Delete(Func<Employee, bool> predicate);
        void SaveChanges();
    }
}