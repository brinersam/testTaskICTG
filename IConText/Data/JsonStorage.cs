using IConText.Data.Model;
using System.Reflection;
using System.Text.Json;

namespace IConText.Data
{
    public class JsonStorage : IStorage
    {
        private readonly string _storagePath;
        private readonly Employees _data;

        public int HighestId { get => GetId(); }

        public JsonStorage(string relativeStoragePath = null)
        {
            _data = new Employees();

            if (relativeStoragePath == null)
            {
                Console.WriteLine($"Warning: {typeof(JsonStorage)} has been initialized without file path! Data will not persist between sessions!");
                return;
            }

            _storagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + relativeStoragePath;

            if (File.Exists(_storagePath))
            {
                string json = File.ReadAllText(_storagePath);
                try
                {
                    _data = JsonSerializer.Deserialize<Employees>(json);
                }
                catch (JsonException ex)
                {
                    throw new Exception($"Corrupted json file at {_storagePath}", ex);
                }

            }
            else
            {
                File.WriteAllText(_storagePath, JsonSerializer.Serialize(_data));
            }
        }

        public void Create(Employee employee)
        {
            Employee copy = new Employee(employee);
            copy.Id = HighestId + 1;
            _data.Add(copy);
        }

        public IEnumerable<Employee> Read(Func<Employee, bool> predicate = null)
        {
            return _data.Where(x => predicate(x));
        }

        public void Update(Employee overwriteData, Func<Employee, bool> predicate)
        {
            IEnumerable<Employee> searchResult = Read(predicate);

            if (searchResult.Count() <= 0)
                throw new KeyNotFoundException("No results found!");

            foreach (Employee existingData in searchResult.ToArray())
            {
                foreach (PropertyInfo prop in typeof(Employee).GetProperties())
                {
                    var value = prop.GetValue(overwriteData);
                    if (value == null || prop.GetCustomAttribute<CanNotBeChangedByUserAttribute>() != null)
                        continue;

                    prop.SetValue(existingData, value);
                }
            }
        }

        public IEnumerable<Employee> Delete(Func<Employee, bool> predicate)
        {
            List<Employee> toRemove = new List<Employee>();
            foreach (Employee emp in Read(predicate).ToArray())
            {
                toRemove.Add(emp);
                _data.Remove(emp);
            }

            return toRemove;
        }

        public void SaveChanges()
        {
            if (_storagePath == null)
            { 
                Console.WriteLine($"Warning : {typeof(JsonStorage)} was saved with no storage path configured! Changes will not be saved!");
                return;
            }
            
            File.WriteAllText(_storagePath, JsonSerializer.Serialize(_data));
        }
        private int GetId()
        {
            if (_data.Count <= 0)
            {
                return 0;
            }
            else
            {
                return _data.Max(e => (int)e.Id);
            }
        }
    }
}
