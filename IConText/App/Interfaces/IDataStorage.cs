using IConText.Data;
using IConText.Data.Model;

namespace IConText.App
{
    public interface IDataStorage : IEnumerable<Employee>
    {
        IEnumerable<T> Get<T>(Func<bool, T> predicate) where T : EntityBase<T>;
    }
}