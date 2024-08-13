using IConText.Data;
using IConText.Data.Model;

namespace IConText.Tests
{
    public class JsonStorageTests_Update
    {
        JsonStorage storage;
        Employee employeeOriginal; 

        public JsonStorageTests_Update() 
        {
            storage = new JsonStorage();
            employeeOriginal = new Employee() { FirstName = "A", LastName = "A", SalaryPerHour = 1 };
            storage.Create(employeeOriginal);
        }

        [Fact]
        public void Update_Complete_Success()
        {
            Employee employeeOverrwrite = new Employee() {FirstName = "B", LastName = "B", SalaryPerHour = 2 };
            storage.Update(employeeOverrwrite, x => x.Id == 1);

            Employee resultEmployee = storage.Read(x => x.Id == 1).First();

            Assert.True(resultEmployee.SalaryPerHour == employeeOverrwrite.SalaryPerHour);
            Assert.True(resultEmployee.FirstName == employeeOverrwrite.FirstName);
            Assert.True(resultEmployee.LastName == employeeOverrwrite.LastName);
        }

        [Fact]
        public void Update_Complete_Success_IgnoreId()
        {
            Employee employeeOverrwrite = new Employee() {Id = 255, FirstName = "B", LastName = "B", SalaryPerHour = 2 };
            storage.Update(employeeOverrwrite, x => x.Id == 1);

            Employee resultEmployee = storage.Read(x => x.Id == 1).First();

            Assert.True(resultEmployee.Id != employeeOverrwrite.Id);
        }

        [Fact]
        public void Update_FirstNameOnly_Success()
        {
            Employee employeeOverrwrite = new Employee() { FirstName = "B"};
            storage.Update(employeeOverrwrite, x => x.Id == 1);

            Employee result = storage.Read(x => x.Id == 1).First();

            Assert.True(result.FirstName == employeeOverrwrite.FirstName);
            Assert.True(result.FirstName != employeeOriginal.FirstName);
        }
        [Fact]
        public void Update_FirstNameOnly_DoesntChangeOtherFields()
        {
            Employee employeeOverrwrite = new Employee() { FirstName = "B" };
            storage.Update(employeeOverrwrite, x => x.Id == 1);

            Employee result = storage.Read(x => x.Id == 1).First();

            Assert.True(result.FirstName == employeeOverrwrite.FirstName);
            Assert.True(result.LastName == employeeOriginal.LastName);
            Assert.True(result.SalaryPerHour == employeeOriginal.SalaryPerHour);
        }

        [Fact]
        public void Update_ErrorOnMissingId()
        {
            Employee employeeOverrwrite = new Employee() { FirstName = "B", LastName = "B", SalaryPerHour = 2 };

            Assert.Throws<KeyNotFoundException>(() => storage.Update(employeeOverrwrite, x => x.Id == 255));
        }
    }
}