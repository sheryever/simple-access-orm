using System;
using System.Data;
using System.Linq;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServer.TestNetCore2.Entities;
using Xunit;

namespace SimpleAccess.SqlServer.Test
{
     public class SimpleAccessTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }
 
        public SimpleAccessTest()
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }

        [Fact]
        public void ExecuteScalarTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalar<int>("Select Count(*) FROM Categories");

            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public void ExecuteScalarWithObjectParamTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalar<int>("Select Count(*) FROM Categories WHERE Id > @id", new { Id = 0 });

            Assert.Equal(3, categoriesCount);
        }
        [Fact]
        public void ExecuteScalarWithCommandTypeAndObjectParamTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalar<int>("Select Count(*) FROM Categories WHERE Id > @id"
                , CommandType.Text, new {id = 0});

            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public void ExecuteScalarWithSqlParameterTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalar<int>("Select Count(*) FROM Categories WHERE Id > @id", 0.ToDataParam("Id"));

            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public void ExecuteScalarWithCommandTypeAndSqlParameterTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalar<int>("Select Count(*) FROM Categories WHERE Id > @id"
                , CommandType.Text,  0.ToDataParam("Id"));

            Assert.Equal(3, categoriesCount);
        }

        [Fact]
        public void ExecuteScalarWithTransactionTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM Categories");

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Branches]");

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Attachments]");


                Assert.Equal(3, rowCount);
                try
                {
                    var a = new
                        Attachment2
                        {OtherName = "TETET"};
                    rowCount = SimpleAccess.ExecuteScalar<int>(transaction,
                        "INSERT INTO [Attachments2] VALUES (@IncidentId, @OtherName, @ShipDate); SELECT SCOPE_IDENTITY() ", a);

                }
                catch (Exception ex)
                {
                    Assert.Equal(3, rowCount);

                }

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteScalarWithTransactionAndObjectParamTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM Categories WHERE Id > @id", 0.ToDataParam("Id"));

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Branches] WHERE Id > @id", 0.ToDataParam("Id"));

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Attachments] WHERE Id > @id", 0.ToDataParam("Id"));


                Assert.Equal(3, rowCount);


                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteScalarWithTransaction_CommandTypeAndObjectParamTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {

                var rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM Categories WHERE Id > @id"
                    , CommandType.Text, new {id =0});

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Branches] WHERE Id > @id"
                    , CommandType.Text, new { id = 0 });

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Attachments] WHERE Id > @id"
                    , CommandType.Text, new { id = 0 });


                Assert.Equal(3, rowCount);


                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteScalarWithTransactionAndParametersTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM Categories WHERE Id > @id", 0.ToDataParam("Id"));

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Branches] WHERE Id > @id", 0.ToDataParam("Id"));

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Attachments] WHERE Id > @id", 0.ToDataParam("Id"));


                Assert.Equal(3, rowCount);


                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteScalarWithTransaction_CommandTypeAndParametersTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM Categories WHERE Id > @id", CommandType.Text, 0.ToDataParam("Id"));

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Branches] WHERE Id > @id", CommandType.Text, 0.ToDataParam("Id"));

                Assert.Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Attachments] WHERE Id > @id", CommandType.Text, 0.ToDataParam("Id"));


                Assert.Equal(3, rowCount);


                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteReaderTest()
        {
            var categoriesCount = 0;

            using (var reader = SimpleAccess.ExecuteReader("Select * FROM Categories"))
            {
                while (reader.Read())
                {
                    categoriesCount++;
                }

            }

            Assert.Equal(3, categoriesCount);
        }
        
        [Fact]
        public void ExecuteValuesTest()
        {
            var categoryIds = SimpleAccess.ExecuteValues<int>("Select Id FROM Categories");

            Assert.Equal(3, categoryIds.Count());
        }

        [Fact]
        public void ExecuteValuesWithTransactionTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var values = SimpleAccess.ExecuteValues<int>(transaction, "Select Id FROM Categories");

                Assert.Equal(3, values.Count());

                values = SimpleAccess.ExecuteValues<int>(transaction, "Select Id FROM [Branches]");

                Assert.Equal(3, values.Count());

                values = SimpleAccess.ExecuteValues<int>(transaction, "Select Id FROM [Attachments]");

                Assert.Equal(3, values.Count());

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteEntitiesTest()
        {
            var categoriesCount = SimpleAccess.ExecuteEntities<Category>("Select Id, Name, Description FROM Categories");

            Assert.Equal(3, categoriesCount.Count());
        }


        [Fact]

        public void ExecuteEntitiesWithTransactionTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var categories = SimpleAccess.ExecuteEntities<Category>(transaction, "Select Id, Name, Description FROM Categories");

                Assert.Equal(3, categories.Count());

                var branches = SimpleAccess.ExecuteEntities<Branch>(transaction, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]");

                Assert.Equal(3, branches.Count());

                var attachments = SimpleAccess.ExecuteEntities<Attachment>(transaction, "Select Id, [IncidentId], [OtherName] FROM [Attachments]");

                Assert.Equal(3, attachments.Count());

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteEntityTest()
        {
            var category = SimpleAccess.ExecuteEntity<Category>("Select Top 1 Id, Name, Description FROM Categories");

            Assert.NotNull(category);
            Assert.Equal(1, category.Id);
        }


        [Fact]

        public void ExecuteEntityWithTransactionTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var category = SimpleAccess.ExecuteEntity<Category>(transaction, "Select Top 1 Id, Name, Description FROM Categories");

                Assert.NotNull(category);
                Assert.Equal(1, category.Id);

                var branch = SimpleAccess.ExecuteEntity<Branch>(transaction, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]");

                Assert.NotNull(branch);
                Assert.Equal(1, branch.Id);

                var attachment = SimpleAccess.ExecuteEntity<Attachment>(transaction, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]");

                Assert.NotNull(attachment);
                Assert.Equal(1, attachment.Id);

                SimpleAccess.EndTransaction(transaction);

            }
        }



        [Fact]
        public void ExecuteDynamicTest()
        {
            var category = SimpleAccess.ExecuteDynamic("Select Top 1 Id, Name, Description FROM Categories");

            Assert.NotNull(category);
            Assert.Equal(category.Id, 1);
        }


        [Fact]

        public void ExecuteDynamicWithTransactionTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var category = SimpleAccess.ExecuteDynamic(transaction, "Select Top 1 Id, Name, Description FROM Categories");

                Assert.NotNull(category);
                Assert.Equal(category.Id, 1);

                var branch = SimpleAccess.ExecuteDynamic(transaction, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]");

                Assert.NotNull(branch);
                Assert.Equal(branch.Id, 1);

                var attachment = SimpleAccess.ExecuteDynamic(transaction, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]");

                Assert.NotNull(attachment);
                Assert.Equal(attachment.Id, 1);

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void ExecuteDynamicsTest()
        {
            var rowAffected = SimpleAccess.ExecuteNonQuery("UPDATE Categories SET Description = @description WHERE Id = @id", 
                new { id = 2, description = "Updated description"});

            Assert.Equal(1, rowAffected);
        }


        [Fact]
        public void ExecuteDynamicsWithTransactionTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowAffected = SimpleAccess.ExecuteNonQuery(transaction, "UPDATE Categories SET Description = @description WHERE Id = @id",
                    new { id = 2, description = "Updated description with transaction" });

                Assert.Equal(1, rowAffected);

                rowAffected = SimpleAccess.ExecuteNonQuery(transaction, "UPDATE [Branches] SET [Address2] = @address2 WHERE Id = @id",
                    new { id = 2, address2 = "Updated Address2 with transaction" });


                Assert.Equal(1, rowAffected);

                rowAffected = SimpleAccess.ExecuteNonQuery(transaction, "UPDATE Attachments SET [OtherName] = @otherName WHERE Id = @id",
                    new { id = 2, otherName = "Updated OtherName with transaction" });


                Assert.Equal(1, rowAffected);

                SimpleAccess.EndTransaction(transaction);

            }
        }
    }
}
