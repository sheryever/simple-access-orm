# Simple Access ORM
SimpleAccess provides a simple database access as well as simple repository for CURD and other helper methods.

SimpleAccess provides excpetion logging.

SimpleAccess returns data in Entity and dynamic data type but also allow developers to work on direct DataReader or DataSet

## Nuget package
```powershell
PM > Install-Package SimpleAccess.SqlServer
```

## SqlSimpleAccess
SimpleAccess implementaion for Sql Server.

| Methods | Description |
|--------------------|--------|
| BeginTrasaction  | Begins a database transaction.|
| CloseDbConnection | Close the current open connection.|
| EndTransaction   | Close an open database transaction.|
| ExecuteEntity&lt;TEntity&gt; | Sends the CommandText to the Database Connection and builds a TEntity from DataReader. |
| ExecuteEntities&lt;TEntity&gt; | Sends the CommandText to the Database Connection and builds a IEnumerable&lt;TEntity&gt; from DataReader. |
| ExecuteDynamic | Sends the CommandText to the Database Connection and builds a dynamic object from DataReader. |
| ExecuteDynamics | Sends the CommandText to the Database Connection and builds a IEnumerable&lt;dynamic&gt; from DataReader. |
| ExecuteNonQuery  | Execute CommandText and returns the count of rows affected.|
| ExecuteReader    | Executes the commandText and returns a DataReader.|
| ExecuteScalar&lt;T&gt; | Executes the command text, and returns the first column of the first row in the result set returned by the query.Additional columns or rows are ignored. |
| Fill | Execute commant text against connection and add or refresh rows in DataSet or DataTable. |
| GetNewConnection | Gets the new connection with the SimpleAccess Ojbect ConnectionString.|

** *All Execute and Fill methods have multiple overloads.* **

## Using SimpleAccess
Reading single record from the database as dynamic object
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
var person = simpleAccess.ExecuteDynamic("Select * FROM people where id = @id;", new { id  = 12});
```

Reading records from the database as IEnumerable&lt;dynamic&gt;
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
var people = simpleAccess.ExecuteDynamic("Select * FROM people;");
```

Reading single record from the database as Person object
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
var person = simpleAccess.ExecuteEntity<Person>("Select * FROM people where id = @id;", new { id  = 12});
```

Reading records from the database as IEnumerable&lt;Person&gt;
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
var people = simpleAccess.ExecuteEntities<Person>("Select * FROM people;");
```

Reading records from the database using DataReader
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
var dataReader = simpleAccess.ExecuteReader("Select * FROM people;");
```

Executing aggregate query using SimpleAccess
``` C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
var totalPeople = simpleAccess.ExecuteScalar<int>("Select COUNT(*) FROM people;");
```

Executes a SQL statement against the connection and returns the number of rows affected
``` C#
public class PersonInsertViewModel
{
    public string Name { get; set; }
    public string Address { get; set; }
}

ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");

var person = new PersonInsertViewModel {Name = "Ahmed", Address = "Madina"};
var rowAffected = simpleAccess.ExecuteNonQuery("INSERT INTO People values (@name, @address);", person);

var rowAffected = simpleAccess.ExecuteNonQuery("UPDATE People SET Name=@name WHERE Id = @id;", new {id = 1, name = "Muhammad"});

```

Using transactions with SimpleAccess
```C#
ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
SqlTransaction transaction = null;
try
{
    using (transaction = simpleAccess.BeginTrasaction())
    {
        var person = new Person() { Name = "Ahmed", Address = "Madina" };

        var newId = simpleAccess.ExecuteScalar<int>(transaction, "INSERT INTO People VALUES (@name, @Address); SELECT SCOPE_IDENTITY();", person);

        simpleAccess.EndTransaction(transaction);
    }
}
catch (Exception)
{
    simpleAccess.EndTransaction(transaction, false);
    throw;
}
```

## SqlRepository

All methods are based on stored procedures with it's related sotred procedure naming convention.

| Methods            | Sp Name | Description |
|--------------------|---------|-------------|
| Get&lt;TEntity&gt; | TEntity_GetById </br> ie. People_GetById | Get TEntity by Id or anyother parameter |
| GetAll&lt;TEntity&gt; | TEntity_GetAll </br> ie. People_GetAll | Get all TEntity object in an IEnumerable&lt;TEntity&gt;. |
| FindSingle&lt;TEntity&gt; | TEntity_Find </br> ie. People_Find | Find a single TEntity object based on where expression. |
| FindAll&lt;TEntity&gt; | TEntity_Find </br> ie. People_Find | Find all TEntity objects based on where expression. |
| Insert&lt;TEntity&gt; | TEntity_Insert </br> ie. People_Insert  | Inserts the given TEntity |
| InsertAll&lt;TEntity&gt; | TEntity_Insert </br> ie. People_Insert  | Inserts all the given entities |
| Update&lt;TEntity&gt; | TEntity_Update </br> ie. People_Update | Updates the given TEntity |
| UpdateAll&lt;TEntity&gt; | TEntity_Update </br> ie. People_Update  | Updates all the given entities |
| Delete&lt;TEntity&gt; | TEntity_Delete </br> ie. People_Delete | Deletes TEntity by the given Id |
| DeleteAll&lt;TEntity&gt; | TEntity_Delete </br> ie. People_Delete | Deletes all the TEntity records by the given Ids |
| SoftDelete&lt;TEntity&gt; | TEntity_MarkDelete </br> ie. People_MarkDelete | Marks  TEntity deleted by the given Id   |

### Using SqlRepository with StoredProcedure

##### People Table
```Sql
CREATE TABLE [dbo].[People](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL,
	[PhoneNumbers] [NVARCHAR](30) NULL,
	[Address] [NVARCHAR](300) NULL,
	[IsDeleted] [BIT] NOT NULL,
	[CreatedBy] [BIGINT] NULL,
	[CreatedOn] [SMALLDATETIME] NULL,
	[ModifiedBy] [BIGINT] NULL,
	[ModifiedOn] [SMALLDATETIME] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

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
##### People_MarkDelete
```Sql
CREATE PROC [dbo].[People_MarkDelete]
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

##### Accessing data

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
            person = repo.FindSingle<Person>(b => b.Id == 1);
            people = repo.Find<Person>(b => b.Address.EndsWith("Munawwarah")
            								&& b.Name == "البيداء"); // EndsWith & StartsWith uses LIKE
            people = repo.Find<Person>(b => b.Address == null);  // Where Address is null

            // Retrive data using SqlRepository.SimpleAccess
            var peopleDeleted = repo.SimpleAccess.ExecuteDynamics("People_GetAllDeleted");
            // while using SqlRepository with StoredProcedures SimpleAccess default command type will be stored procedure

			var peopleInDyanmics = repo.SimpleAccess.ExecuteDynamics("Select * FROM people", CommandType.Text);
			var peopleEnumerable = repo.SimpleAccess.ExecuteEntities<Person>("Select * FROM people", CommandType.Text);

            var totalPeople = repo.SimpleAccess.ExecuteScalar<int>("SELECT COUNT([Id]) FROM people;", CommandType.Text);

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

    	}
    }

}
````

## Support
- Simple Access is written in C# and support .net Managed Code langues (C# and VB.net etc)
- Sql Server 2005 and later
- Oracle 10g and later (in default Simple Access uese Oracle Data Provider for .NET, to use Oracle Data Access Components (ODAC))

## Roadmap
- [x] Separate SimpleCommand and Repositoy
- [x] vitual properties must behave like NotASpParameter marked perperty in Entities drived from StoredProcedureParameters
- [x] Remove StoredProcedureParameters inheritance from Enity Class to make entity more lighter
[Read more...](/Docs/Roadmap.md)
