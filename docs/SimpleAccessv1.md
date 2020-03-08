##### Using SimpleAccess v1 Sql Server Repository (Repository)
***It is recommanded to use SimpleAccess v2 SqlRepository instead of SimpleAccess v1 repository. Although SimpleAccess v1 repository is supported and included in SimpleAccess v2 for backward compatibility***

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

            IRepository repo = new Repository("connectionStringName");

            // Retrive data using SimpleAccess SqlRepository
            var people = repo.GetAll<Person>();
            var person = repo.Get<Person>(1);

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

			// Delete
            var rowAffected = repo.Delete<Person>(1);

            // Retrive data using SimpleAccess Repository v1
            var peopleDeleted = repo.ExecuteReader("People_GetAllDeleted", CommandType.StoredProcedure);

			var peopleInDyanmics = repo.ExecuteReader("Select * FROM people", CommandType.Text);
			var peopleEnumerable = repo.ExecuteReader<Person>("Select * FROM people", CommandType.Text);

			// Retrive scalar value with query
            var totalPeople = repo.ExecuteScalar<int>("SELECT COUNT([Id]) FROM people;", CommandType.Text);
    	}
    }

}
````
