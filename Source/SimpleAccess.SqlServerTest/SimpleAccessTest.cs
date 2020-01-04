using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServerTestNetCore2.Entities;

namespace SimpleAccess.SqlServerTest
{
    [TestClass]
    public class SimpleAccessTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }

        [TestInitialize]
        public void SetupSimpleAccess()
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
        }

        [TestMethod]
        public void ExecuteScalarTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalar<int>("Select Count(*) FROM Category");

            Assert.AreEqual(categoriesCount, 3);
        }


        [TestMethod]

        public void ExecuteScalarWithTransactionContextTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM Category");

                Assert.AreEqual(rowCount, 3);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Branches]");

                Assert.AreEqual(rowCount, 2);

                rowCount = SimpleAccess.ExecuteScalar<int>(transaction, "Select Count(*) FROM [Attachments]");


                Assert.AreEqual(rowCount, 5);


                SimpleAccess.EndTransaction(transaction);

            }
        }


        [TestMethod]
        public void ExecuteReaderTest()
        {
            var categoriesCount = 0;
                
            var reader = SimpleAccess.ExecuteReader("Select * FROM Category");

            while (reader.Read())
            {
                categoriesCount++;
            }


            Assert.AreEqual(categoriesCount, 3);
        }
        
        [TestMethod]
        public void ExecuteValuesTest()
        {
            var categoryIds = SimpleAccess.ExecuteValues<int>("Select Id FROM Category");

            Assert.AreEqual(categoryIds.Count(), 3);
        }


        [TestMethod]

        public void ExecuteValuesWithTransactionContextTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var values = SimpleAccess.ExecuteValues<int>(transaction, "Select Id FROM Category");

                Assert.AreEqual(values.Count(), 3);

                values = SimpleAccess.ExecuteValues<int>(transaction, "Select Id FROM [Branches]");

                Assert.AreEqual(values.Count(), 2);

                values = SimpleAccess.ExecuteValues<int>(transaction, "Select Id FROM [Attachments]");

                Assert.AreEqual(values.Count(), 5);

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [TestMethod]
        public void ExecuteEntitiesTest()
        {
            var categoriesCount = SimpleAccess.ExecuteEntities<Category>("Select Id, Name, Description FROM Category");

            Assert.AreEqual(categoriesCount.Count(), 3);
        }


        [TestMethod]

        public void ExecuteEntitiesWithTransactionContextTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var categories = SimpleAccess.ExecuteEntities<Category>(transaction, "Select Id, Name, Description FROM Category");

                Assert.AreEqual(categories.Count(), 3);

                var branches = SimpleAccess.ExecuteEntities<Branch>(transaction, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]");

                Assert.AreEqual(branches.Count(), 2);

                var attachments = SimpleAccess.ExecuteEntities<Attachment>(transaction, "Select Id, [IncidentId], [OtherName] FROM [Attachments]");

                Assert.AreEqual(attachments.Count(), 5);

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [TestMethod]
        public void ExecuteEntityTest()
        {
            var category = SimpleAccess.ExecuteEntity<Category>("Select Top 1 Id, Name, Description FROM Category");

            Assert.IsNotNull(category);
            Assert.AreEqual(category.Id, 2);
        }


        [TestMethod]

        public void ExecuteEntityWithTransactionContextTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var category = SimpleAccess.ExecuteEntity<Category>(transaction, "Select Top 1 Id, Name, Description FROM Category");

                Assert.IsNotNull(category);
                Assert.AreEqual(category.Id, 2);

                var branch = SimpleAccess.ExecuteEntity<Branch>(transaction, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]");

                Assert.IsNotNull(branch);
                Assert.AreEqual(branch.Id, 1);

                var attachment = SimpleAccess.ExecuteEntity<Attachment>(transaction, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]");

                Assert.IsNotNull(attachment);
                Assert.AreEqual(attachment.Id, 5);

                SimpleAccess.EndTransaction(transaction);

            }
        }



        [TestMethod]
        public void ExecuteDynamicTest()
        {
            var category = SimpleAccess.ExecuteDynamic("Select Top 1 Id, Name, Description FROM Category");

            Assert.IsNotNull(category);
            Assert.AreEqual(category.Id, 2);
        }


        [TestMethod]

        public void ExecuteDynamicWithTransactionContextTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var category = SimpleAccess.ExecuteDynamic(transaction, "Select Top 1 Id, Name, Description FROM Category");

                Assert.IsNotNull(category);
                Assert.AreEqual(category.Id, 2);

                var branch = SimpleAccess.ExecuteDynamic(transaction, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]");

                Assert.IsNotNull(branch);
                Assert.AreEqual(branch.Id, 1);

                var attachment = SimpleAccess.ExecuteDynamic(transaction, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]");

                Assert.IsNotNull(attachment);
                Assert.AreEqual(attachment.Id, 5);

                SimpleAccess.EndTransaction(transaction);

            }
        }

        [TestMethod]
        public void ExecuteDynamicsTest()
        {
            var rowAffected = SimpleAccess.ExecuteNonQuery("UPDATE Category SET Description = @description WHERE Id = @id", 
                new { id = 2, description = "Updated description"});

            Assert.AreEqual(rowAffected, 1);
        }


        [TestMethod]
        public void ExecuteDynamicsWithTransactionContextTest()
        {
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                var rowAffected = SimpleAccess.ExecuteNonQuery(transaction, "UPDATE Category SET Description = @description WHERE Id = @id",
                    new { id = 2, description = "Updated description with transaction" });

                Assert.AreEqual(rowAffected, 1);

                rowAffected = SimpleAccess.ExecuteNonQuery(transaction, "UPDATE [Branches] SET [Address2] = @address2 WHERE Id = @id",
                    new { id = 2, address2 = "Updated Address2 with transaction" });


                Assert.AreEqual(rowAffected, 1);

                rowAffected = SimpleAccess.ExecuteNonQuery(transaction, "UPDATE Attachments SET [OtherName] = @otherName WHERE Id = @id",
                    new { id = 6, otherName = "Updated OtherName with transaction" });


                Assert.AreEqual(rowAffected, 1);

                SimpleAccess.EndTransaction(transaction);

            }
        }
    }
}
