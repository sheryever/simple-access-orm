using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServerTestNetCore2.Entities;

namespace SimpleAccess.SqlServerTest
{
    [TestClass]
    public class SimpleAccessAsyncTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }

        [TestInitialize]
        public void SetupSimpleAccess()
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
        }

        [TestMethod]
        public void ExecuteScalarAsyncTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalarAsync<int>("Select Count(*) FROM Category").Result;

            Assert.AreEqual(categoriesCount, 3);
        }


        [TestMethod]

        public void ExecuteScalarAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var rowCount = SimpleAccess.ExecuteScalarAsync<int>(transContext, "Select Count(*) FROM Category").Result;

                Assert.AreEqual(rowCount, 3);

                rowCount = SimpleAccess.ExecuteScalarAsync<int>(transContext, "Select Count(*) FROM [Branches]").Result;

                Assert.AreEqual(rowCount, 2);

                rowCount = SimpleAccess.ExecuteScalarAsync<int>(transContext, "Select Count(*) FROM [Attachments]").Result;


                Assert.AreEqual(rowCount, 5);


                SimpleAccess.EndTransaction(transContext);

            }
        }


        [TestMethod]
        public void ExecuteReaderAsyncTest()
        {
            var categoriesCount = 0;
                
            var reader = SimpleAccess.ExecuteReaderAsync("Select * FROM Category").Result;

            while (reader.Read())
            {
                categoriesCount++;
            }


            Assert.AreEqual(categoriesCount, 3);
        }
        
        [TestMethod]
        public void ExecuteValuesAsyncTest()
        {
            var categoryIds = SimpleAccess.ExecuteValuesAsync<int>("Select Id FROM Category").Result;

            Assert.AreEqual(categoryIds.Count(), 3);
        }


        [TestMethod]

        public void ExecuteValuesAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM Category").Result;

                Assert.AreEqual(values.Count(), 3);

                values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Branches]").Result;

                Assert.AreEqual(values.Count(), 2);

                values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Attachments]").Result;

                Assert.AreEqual(values.Count(), 5);

                SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void ExecuteEntitiesAsyncTest()
        {
            var categoriesCount = SimpleAccess.ExecuteEntitiesAsync<Category>("Select Id, Name, Description FROM Category").Result;

            Assert.AreEqual(categoriesCount.Count(), 3);
        }


        [TestMethod]

        public void ExecuteEntitiesAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var categories = SimpleAccess.ExecuteEntitiesAsync<Category>(transContext, "Select Id, Name, Description FROM Category").Result;

                Assert.AreEqual(categories.Count(), 3);

                var branches = SimpleAccess.ExecuteEntitiesAsync<Branch>(transContext, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

                Assert.AreEqual(branches.Count(), 2);

                var attachments = SimpleAccess.ExecuteEntitiesAsync<Attachment>(transContext, "Select Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

                Assert.AreEqual(attachments.Count(), 5);

                SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void ExecuteEntityAsyncTest()
        {
            var category = SimpleAccess.ExecuteEntityAsync<Category>("Select Top 1 Id, Name, Description FROM Category").Result;

            Assert.IsNotNull(category);
            Assert.AreEqual(category.Id, 2);
        }


        [TestMethod]

        public void ExecuteEntityAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SimpleAccess.ExecuteEntityAsync<Category>(transContext, "Select Top 1 Id, Name, Description FROM Category").Result;

                Assert.IsNotNull(category);
                Assert.AreEqual(category.Id, 2);

                var branch = SimpleAccess.ExecuteEntityAsync<Branch>(transContext, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

                Assert.IsNotNull(branch);
                Assert.AreEqual(branch.Id, 1);

                var attachment = SimpleAccess.ExecuteEntityAsync<Attachment>(transContext, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

                Assert.IsNotNull(attachment);
                Assert.AreEqual(attachment.Id, 5);

                SimpleAccess.EndTransaction(transContext);

            }
        }



        [TestMethod]
        public void ExecuteDynamicAsyncTest()
        {
            var category = SimpleAccess.ExecuteDynamicAsync("Select Top 1 Id, Name, Description FROM Category").Result;

            Assert.IsNotNull(category);
            Assert.AreEqual(category.Id, 2);
        }


        [TestMethod]

        public void ExecuteDynamicAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SimpleAccess.ExecuteDynamicAsync(transContext, "Select Top 1 Id, Name, Description FROM Category").Result;

                Assert.IsNotNull(category);
                Assert.AreEqual(category.Id, 2);

                var branch = SimpleAccess.ExecuteDynamicAsync(transContext, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

                Assert.IsNotNull(branch);
                Assert.AreEqual(branch.Id, 1);

                var attachment = SimpleAccess.ExecuteDynamicAsync(transContext, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

                Assert.IsNotNull(attachment);
                Assert.AreEqual(attachment.Id, 5);

                SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void ExecuteDynamicsAsyncTest()
        {
            var rowAffected = SimpleAccess.ExecuteNonQueryAsync("UPDATE Category SET Description = @description WHERE Id = @id", 
                new { id = 2, description = "Updated description"}).Result;

            Assert.AreEqual(rowAffected, 1);
        }


        [TestMethod]
        public void ExecuteDynamicsAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Category SET Description = @description WHERE Id = @id",
                    new { id = 2, description = "Updated description with transaction" }).Result;

                Assert.AreEqual(rowAffected, 1);

                rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE [Branches] SET [Address2] = @address2 WHERE Id = @id",
                    new { id = 2, address2 = "Updated Address2 with transaction" }).Result;


                Assert.AreEqual(rowAffected, 1);

                rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Attachments SET [OtherName] = @otherName WHERE Id = @id",
                    new { id = 6, otherName = "Updated OtherName with transaction" }).Result;


                Assert.AreEqual(rowAffected, 1);

                SimpleAccess.EndTransaction(transContext);

            }
        }
    }
}
