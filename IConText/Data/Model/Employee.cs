using System.Collections;

namespace IConText.Data.Model
{
    public class Employee : EntityBase<Employee>, IFormattable
    {
        [CanNotBeChangedByUser]
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal? SalaryPerHour { get; set; }

        public Employee()
        {}

        public Employee(Employee entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            SalaryPerHour = entity.SalaryPerHour;
        }

        public override Employee Copy(Employee entity)
        {
            return new Employee(entity);
        }

        public override Employee Parse(string[] properties)
        {
            return ParseObjectFromProperties<Employee>(
                properties,
                ':',
                new() { { "Salary", "SalaryPerHour" } });
        }

        public override string ToString()
        {
            return $"Id = {Id}, FirstName = {FirstName}, LastName = {LastName}, SalaryPerHour = {SalaryPerHour}";
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return ToString();
        }
    }

    public class Employees : IList<Employee>
    {
        public List<Employee> EmployeeList { get; set; } = new();

        public int Count => EmployeeList.Count;

        public bool IsReadOnly => false;

        public Employee this[int idx]
        {
            get { return EmployeeList[idx]; }
            set { EmployeeList[idx] = value; }
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            return EmployeeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(Employee item)
        {
            return EmployeeList.IndexOf(item);
        }

        public void Insert(int index, Employee item)
        {
            EmployeeList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            EmployeeList.RemoveAt(index);
        }

        public void Add(Employee item)
        {
            EmployeeList.Add(item);
        }

        public void Clear()
        {
            EmployeeList.Clear();
        }

        public bool Contains(Employee item)
        {
            return EmployeeList.Contains(item);
        }

        public void CopyTo(Employee[] array, int arrayIndex)
        {
            EmployeeList.CopyTo(array, arrayIndex);
        }

        public bool Remove(Employee item)
        {
            return EmployeeList.Remove(item);
        }
    }
}
