### Using SimpleAccess v3.1 SqlEntityRepository

SqlEntityRepository implements the ISqlRepository and it generates all the sql queries from entities

##### People Table
```Sql
CREATE TABLE [dbo].[People](
	[Id] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [NVARCHAR](100) NOT NULL,
	[PhoneNumbers] [NVARCHAR](30) NULL,
	[Address] [NVARCHAR](300) NULL,
	[IsDeleted] [BIT] NOT NULL,
	[CreatedBy] [BIGINT] NULL,
	[CreatedOn] [SMALLDATETIME] NULL,
	[ModifiedBy] [BIGINT] NULL,
	[ModifiedOn] [SMALLDATETIME] NULL
)
```

##### C# Entity
````C#
    [Entity("People")]
    public class Person
    {
        [Identity]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumbers { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
````

##### C# Code
````C#
using System.Data;
using System.Collections.Generic;
using SimpleAccess;
using SimpleAccess.Repository;

namespace SimpleAccess.SqlServer.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            ISqlRepository repo = new SqlEntityRepository("Data Source=SqlServerName;Initial Catalog=SimpleAccessTest;Persist Security Info=True;Integrated Security=True;);

            // Retrive data using SimpleAccess SqlEntityRepository
            var people = repo.GetAll<Person>();

            var person = repo.Get<Person>(1);

            person = repo.Find<Person>(b => b.Id == 1);

            people = repo.FindAll<Person>(b => b.Address.EndsWith("Munawwarah")
						&& b.Name == "البيداء"); // EndsWith & StartsWith uses LIKE

            people = repo.FindAll<Person>(b => b.Address == null);  // Where Address is null

	    // Insert
	    var newPerson = new Person {
            	Name = "Ahemd"
                , PhoneNumbers = "1231231323"
                , Address = "Some address"
                , CreatedOn = DateTime.Now
                , CreatedBy = 1 // user id
            };

	    repo.Insert<Person>(newPerson);
            Console.Write("New person id: {0}", newPerson.Id);

	    //Update
            var personToUpdate = repo.GetById(1);

            personToUpdate.Name = "Muhammad";
            personToUpdate.ModifiedOn = DateTime.Now
            personToUpdate.ModifiedBy = 1 // user id

	    var rowAffected = repo.Update<Person>(personToUpdate);

            var rowAffected = repo.Delete<Person>(1);

            // Retrive data using SqlRepository.SimpleAccess
            var peopleDeleted = repo.SimpleAccess.ExecuteDynamics("SELECT * FROM People");
    	}
    }
}
````
