#### Using SimpleAccess v1 Sql Server Repository (Repository)
***It is recommanded to use SimpleAccess latest SqlRepository instead of SimpleAccess v1 repository. Although SimpleAccess v1 repository is supported and included in SimpleAccess v2 for backward compatibility***

#### SimpleAccess SqlRepository Stored Procedures

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

GO
```
##### People_GetById
```Sql
CREATE PROC [dbo].[People_GetById]
	@id INT
AS
BEGIN
    SELECT  Id, Name, PhoneNumbers, Address, IsDeleted, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn
         FROM dbo.People
		 WHERE Id = @Id AND IsDeleted = 0;
END
```
##### People_GetAll
```Sql
CREATE PROC [dbo].[People_GetAll]
AS
BEGIN
    SELECT  Id, Name, PhoneNumbers, Address, IsDeleted, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn
         FROM dbo.People;
         WHERE IsDeleted = 0;
END
```

##### People_Find
```Sql
CREATE PROC [dbo].[People_Find]
	@whereClause NVARCHAR(4000)
    WITH EXEC AS CALLER
AS
BEGIN
    DECLARE @sql NVARCHAR(4000);
    SET @sql =
		'SELECT  Id, Name, PhoneNumbers, Address, IsDeleted, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn ' + ' FROM dbo.People ' +
        ISNULL(@whereClause, '') + 'AND IsDeleted = 0';

     EXEC sp_executesql @sql;
END
```
##### People_Insert
```Sql
CREATE PROC [dbo].[People_Insert]
	  @name NVARCHAR(100)
	 , @phoneNumbers NVARCHAR(30)
	 , @address NVARCHAR(300)
	 , @isDeleted BIT
	 , @createdBy BIGINT
	 , @createdOn SMALLDATETIME
	 , @modifiedBy BIGINT
	 , @modifiedOn SMALLDATETIME
	,@Id INT OUTPUT
AS
BEGIN
     INSERT INTO dbo.People 
        ( Name, PhoneNumbers, Address, IsDeleted, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn )
        VALUES ( @name, @phoneNumbers, @address, @isDeleted, @createdBy, @createdOn, @modifiedBy, @modifiedOn );

	SELECT @Id = SCOPE_IDENTITY();
END
```
##### People_Update
```Sql
CREATE PROC [dbo].[People_Update]
	@Id INT
	 , @name NVARCHAR(100)
	 , @phoneNumbers NVARCHAR(30)
	 , @address NVARCHAR(300)
	 , @isDeleted BIT
	 , @createdBy BIGINT
	 , @createdOn SMALLDATETIME
	 , @modifiedBy BIGINT
	 , @modifiedOn SMALLDATETIME
AS
BEGIN
    UPDATE dbo.People SET Name =  @name
         , PhoneNumbers = @phoneNumbers
         , Address = @address
         , IsDeleted = @isDeleted
         , CreatedBy = @createdBy
         , CreatedOn = @createdOn
         , ModifiedBy = @modifiedBy
         , ModifiedOn = @modifiedOn
     WHERE Id = @Id

END
```
##### People_Delete
```Sql
CREATE PROC [dbo].[People_Delete]
	@Id INT
AS
BEGIN
    DELETE FROM dbo.People
    	WHERE Id = @Id

END
```
##### People_SoftDelete
```Sql
CREATE PROC [dbo].[People_SoftDelete]
	@Id INT
AS
BEGIN
    UPDATE dbo.People SET IsDelete = 1
    	WHERE Id = @Id
END
```

##### c# Entity
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
