using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServerTestNetCore2.Entities;

namespace SimpleAccess.SqlServerTest
{
    [TestClass]
    public class SqlEntityRepositoryAsyncTest
    {
        private static ISqlSimpleAccess SimpleAccess { get; set; }
        private static ISqlRepository SqlRepository{ get; set; }

        [ClassInitialize]
        public static void SetupSimpleAccess(TestContext context)
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            SqlRepository = new SqlEntityRepository(SimpleAccess);
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }

        [TestMethod]
        public void InsertAsyncTest()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };
            var rowAffected = SqlRepository.InsertAsync<Person>(person).Result;
            //var rowAffected = SqlRepository.Insert<Person>(person);

            Assert.AreEqual(rowAffected, 1);
        }

        [TestMethod]
        public void InsertAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var people = new[] {
                    new Person
                    {
                        FullName = "Muhammad Abdul Rehman Khan",
                        Phone = "1112182123"
                    },
                    new Person
                    {
                        FullName = "Muhammad Sharjeel",
                        Phone = "0599065644"
                    },
                    new Person
                    {
                        FullName = "Muhammad Affan",
                        Phone = "1112182123"
                    },
                    new Person
                    {
                        FullName = "Muhammad Usman",
                        Phone = "1112182123"
                    },
                };
                var rowAffected = SqlRepository.InsertAllAsync<Person>(transContext, people).Result;

                Assert.AreEqual(rowAffected, 4);


                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void GetAllAsyncTest()
        {
            var categories = SqlRepository.GetAllAsync<Category>().Result;

            Assert.AreEqual(categories.Count(), 3);
        }

        [TestMethod]
        public void GetAsyncTest()
        {
            var category = SqlRepository.GetAsync<Category>(2).Result;

            Assert.IsNotNull(category);
        }

        [TestMethod]

        public void GetAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SqlRepository.GetAsync<Category>(transContext, 2).Result;

                Assert.IsNotNull(category);

                var branch = SqlRepository.GetAsync<Branch>(transContext, 2).Result;

                Assert.IsNotNull(branch);

                var attachment = SqlRepository.GetAsync<Attachment>(transContext, 3).Result;

                Assert.IsNotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void FindAsyncTest()
        {
            var category = SqlRepository.FindAsync<Category>(c => c.Id == 2).Result;

            Assert.IsNotNull(category);
        }

        [TestMethod]

        public void FindAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SqlRepository.FindAsync<Category>(transContext, c => c.Id == 2).Result;

                Assert.IsNotNull(category);

                var branch = SqlRepository.FindAsync<Branch>(transContext, c => c.Id == 2).Result;

                Assert.IsNotNull(branch);

                var attachment = SqlRepository.FindAsync<Attachment>(transContext, c => c.Id == 2).Result;

                Assert.IsNotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }


        [TestMethod]
        public void FindAllAsyncTest()
        {
            var categories = SqlRepository.FindAllAsync<Category>(c => c.Description.Contains("cat") ).Result;

            Assert.AreEqual(categories.Count(), 2);
        }

        [TestMethod]

        public void FindAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var categories = SqlRepository.FindAllAsync<Category>(transContext, c => c.Description.Contains("cat")).Result;
                Assert.AreEqual(2, categories.Count());

                var branches = SqlRepository.FindAllAsync<Branch>(transContext, c => c.CityId == 1).Result;
                Assert.AreEqual(2, branches.Count());


                var attachments = SqlRepository.FindAllAsync<Attachment>(transContext, c => c.IncidentId == 3).Result;
                Assert.AreEqual(1, attachments.Count());

                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void UpdateAsyncTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.UpdateAsync<Person>(person).Result;

            Assert.AreEqual(rowAffected, 1);
        }

        [TestMethod]

        public void UpdateAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var people = SqlRepository.GetAll<Person>();

                foreach (var person in people)
                {
                    person.FullName = person.FullName + 1;
                }


                var rowAffected = SqlRepository.UpdateAllAsync<Person>(people).Result;

                Assert.AreEqual(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [TestMethod]
        public void DeleteAsyncTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.DeleteAsync<Person>(person.Id).Result;

            Assert.AreEqual(rowAffected, 1);
        }

        [TestMethod]
        public void DeleteAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var people = SqlRepository.GetAll<Person>().Select<Person, long>(p => p.Id);

                var rowAffected = SqlRepository.DeleteAllAsync<Person>(people).Result;

                Assert.AreEqual(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        //[TestMethod]
        //public void ExecuteReaderAsyncTest()
        //{
        //    var categoriesCount = 0;

        //    var reader = SimpleAccess.ExecuteReaderAsync("Select * FROM Categories").Result;

        //    while (reader.Read())
        //    {
        //        categoriesCount++;
        //    }


        //    Assert.AreEqual(categoriesCount, 3);
        //}

        //[TestMethod]
        //public void ExecuteValuesAsyncTest()
        //{
        //    var categoryIds = SimpleAccess.ExecuteValuesAsync<int>("Select Id FROM Categories").Result;

        //    Assert.AreEqual(categoryIds.Count(), 3);
        //}


        //[TestMethod]

        //public void ExecuteValuesAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM Categories").Result;

        //        Assert.AreEqual(values.Count(), 3);

        //        values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Branches]").Result;

        //        Assert.AreEqual(values.Count(), 2);

        //        values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Attachments]").Result;

        //        Assert.AreEqual(values.Count(), 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}

        //[TestMethod]
        //public void ExecuteEntitiesAsyncTest()
        //{
        //    var categoriesCount = SimpleAccess.ExecuteEntitiesAsync<Category>("Select Id, Name, Description FROM Categories").Result;

        //    Assert.AreEqual(categoriesCount.Count(), 3);
        //}


        //[TestMethod]

        //public void ExecuteEntitiesAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var categories = SimpleAccess.ExecuteEntitiesAsync<Category>(transContext, "Select Id, Name, Description FROM Categories").Result;

        //        Assert.AreEqual(categories.Count(), 3);

        //        var branches = SimpleAccess.ExecuteEntitiesAsync<Branch>(transContext, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

        //        Assert.AreEqual(branches.Count(), 2);

        //        var attachments = SimpleAccess.ExecuteEntitiesAsync<Attachment>(transContext, "Select Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

        //        Assert.AreEqual(attachments.Count(), 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}

        //[TestMethod]
        //public void ExecuteEntityAsyncTest()
        //{
        //    var category = SimpleAccess.ExecuteEntityAsync<Category>("Select Top 1 Id, Name, Description FROM Categories").Result;

        //    Assert.IsNotNull(category);
        //    Assert.AreEqual(category.Id, 2);
        //}


        //[TestMethod]

        //public void ExecuteEntityAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var category = SimpleAccess.ExecuteEntityAsync<Category>(transContext, "Select Top 1 Id, Name, Description FROM Categories").Result;

        //        Assert.IsNotNull(category);
        //        Assert.AreEqual(category.Id, 2);

        //        var branch = SimpleAccess.ExecuteEntityAsync<Branch>(transContext, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

        //        Assert.IsNotNull(branch);
        //        Assert.AreEqual(branch.Id, 1);

        //        var attachment = SimpleAccess.ExecuteEntityAsync<Attachment>(transContext, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

        //        Assert.IsNotNull(attachment);
        //        Assert.AreEqual(attachment.Id, 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}



        //[TestMethod]
        //public void ExecuteDynamicAsyncTest()
        //{
        //    var category = SimpleAccess.ExecuteDynamicAsync("Select Top 1 Id, Name, Description FROM Categories").Result;

        //    Assert.IsNotNull(category);
        //    Assert.AreEqual(category.Id, 2);
        //}


        //[TestMethod]

        //public void ExecuteDynamicAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var category = SimpleAccess.ExecuteDynamicAsync(transContext, "Select Top 1 Id, Name, Description FROM Categories").Result;

        //        Assert.IsNotNull(category);
        //        Assert.AreEqual(category.Id, 2);

        //        var branch = SimpleAccess.ExecuteDynamicAsync(transContext, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

        //        Assert.IsNotNull(branch);
        //        Assert.AreEqual(branch.Id, 1);

        //        var attachment = SimpleAccess.ExecuteDynamicAsync(transContext, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

        //        Assert.IsNotNull(attachment);
        //        Assert.AreEqual(attachment.Id, 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}

        //[TestMethod]
        //public void ExecuteDynamicsAsyncTest()
        //{
        //    var rowAffected = SimpleAccess.ExecuteNonQueryAsync("UPDATE Categories SET Description = @description WHERE Id = @id", 
        //        new { id = 2, description = "Updated description"}).Result;

        //    Assert.AreEqual(rowAffected, 1);
        //}


        //[TestMethod]
        //public void ExecuteDynamicsAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Categories SET Description = @description WHERE Id = @id",
        //            new { id = 2, description = "Updated description with transaction" }).Result;

        //        Assert.AreEqual(rowAffected, 1);

        //        rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE [Branches] SET [Address2] = @address2 WHERE Id = @id",
        //            new { id = 2, address2 = "Updated Address2 with transaction" }).Result;


        //        Assert.AreEqual(rowAffected, 1);

        //        rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Attachments SET [OtherName] = @otherName WHERE Id = @id",
        //            new { id = 6, otherName = "Updated OtherName with transaction" }).Result;


        //        Assert.AreEqual(rowAffected, 1);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}
    }
}
