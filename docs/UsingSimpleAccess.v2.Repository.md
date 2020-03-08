
##### Using SimpleAccess v2 Sql Server Repository (SqlRepository)

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

            ISqlRepository repo = new SqlRepository("connectionStringName");

            // Retrive data using SimpleAccess SqlRepository
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
            var peopleDeleted = repo.SimpleAccess.ExecuteDynamics("People_GetAllDeleted");

            // while using SqlRepository with StoredProcedures SimpleAccess default command type will be stored procedure
			var peopleInDyanmics = repo.SimpleAccess.ExecuteDynamics("Select * FROM people", CommandType.Text);
			var peopleEnumerable = repo.SimpleAccess.ExecuteEntities<Person>("Select * FROM people", CommandType.Text);

			// Retrive scalar value with query
            var totalPeople = repo.SimpleAccess.ExecuteScalar<int>("SELECT COUNT([Id]) FROM people;", CommandType.Text);


    	}
    }

}
````
