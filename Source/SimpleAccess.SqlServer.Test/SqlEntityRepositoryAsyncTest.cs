using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServer.TestNetCore2.Entities;
using Xunit;

namespace SimpleAccess.SqlServer.Test
{
     public class SqlEntityRepositoryAsyncTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }
        private ISqlRepository SqlRepository{ get; set; }

         public SqlEntityRepositoryAsyncTest()
        {
            SimpleAccess = new SqlSimpleAccess("Data Source=.\\SQLEXPRESS2017;Initial Catalog=SimpleAccessTest;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Test123;");
            SqlRepository = new SqlEntityRepository(SimpleAccess);
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }

        [Fact]
        public void InsertAsyncTest()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };
            var rowAffected = SqlRepository.InsertAsync<Person>(person).Result;
            //var rowAffected = SqlRepository.Insert<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
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

                Assert.Equal(4, rowAffected);


                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void GetAllAsyncTest()
        {
            var categories = SqlRepository.GetAllAsync<Category>().Result;

            Assert.Equal(3, categories.Count());
        }

        [Fact]
        public async void GetAllAsyncWithTransactionTest()
        {
            using (var trasaction = await SqlRepository.SimpleAccess.BeginTransactionAsync())
            {

                var categories = SqlRepository.GetAllAsync<Category>(trasaction);
                Assert.Equal(3, categories.Result.Count());

            }

        }

        [Fact]
        public async void MultipleGetAllAsyncWithTransactionTest()
        {
            using (var trasaction = await SqlRepository.SimpleAccess.BeginTransactionAsync())
            {

                var categories = SqlRepository.GetAllAsync<Category>(trasaction);
                var people = SqlRepository.GetAllAsync<Person>(trasaction);
                await Task.WhenAll(categories, people);
                Assert.Equal(3, categories.Result.Count());
                Assert.True(people.Result.Any());

            }

        }

        [Fact]
        public void GetAsyncTest()
        {
            var category = SqlRepository.GetAsync<Category>(2).Result;

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }

        [Fact]

        public void GetAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SqlRepository.GetAsync<Category>(transContext, 2).Result;

                Assert.NotNull(category);

                var branch = SqlRepository.GetAsync<Branch>(transContext, 2).Result;

                Assert.NotNull(branch);

                var attachment = SqlRepository.GetAsync<Attachment>(transContext, 3).Result;

                Assert.NotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void FindAsyncTest()
        {
            var category = SqlRepository.FindAsync<Category>(c => c.Id == 2).Result;

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }

        [Fact]

        public void FindAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SqlRepository.FindAsync<Category>(transContext, c => c.Id == 2).Result;

                Assert.NotNull(category);

                var branch = SqlRepository.FindAsync<Branch>(transContext, c => c.Id == 2).Result;

                Assert.NotNull(branch);

                var attachment = SqlRepository.FindAsync<Attachment>(transContext, c => c.Id == 2).Result;

                Assert.NotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }


        [Fact]
        public void FindAllAsyncTest()
        {
            var categories = SqlRepository.FindAllAsync<Category>(c => c.Description.Contains("cat") ).Result;

            Assert.Equal(2, categories.Count());
        }

        [Fact]

        public void FindAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var categories = SqlRepository.FindAllAsync<Category>(transContext, c => c.Description.Contains("cat")).Result;
                Assert.Equal(2, categories.Count());

                var branches = SqlRepository.FindAllAsync<Branch>(transContext, c => c.CityId == 1).Result;
                Assert.Equal(2, branches.Count());


                var attachments = SqlRepository.FindAllAsync<Attachment>(transContext, c => c.IncidentId == 3).Result;
                Assert.Equal(1, attachments.Count());

                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void UpdateAsyncTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.UpdateAsync<Person>(person).Result;

            Assert.Equal(1, rowAffected);
        }

        [Fact]

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

                Assert.Equal(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void DeleteAsyncTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.DeleteAsync<Person>(person.Id).Result;

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void DeleteAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SqlRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var people = SqlRepository.GetAll<Person>().Select<Person, long>(p => p.Id);

                var rowAffected = SqlRepository.DeleteAllAsync<Person>(people).Result;

                Assert.Equal(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void GetDynamicPagedListAsyncTestWithSelectAndWhereClause()
        {
            var people = SqlRepository.GetDynamicPagedListAsync<Person>(p => new { p.Id }, "WHERE ID > 1", 0, 2, "Id", false).Result;

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListAsyncTestWithSelectDistinctAndWhereClause()
        {
            var people = SqlRepository.GetDynamicPagedListAsync<Person>(true, p => new { p.Id }
                                , "WHERE ID > @id", 0, 2, "Id", false, new SqlParameter("@id", 1)).Result;

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        //[Fact]
        //public void ExecuteReaderAsyncTest()
        //{
        //    var categoriesCount = 0;

        //    var reader = SimpleAccess.ExecuteReaderAsync("Select * FROM Categories").Result;

        //    while (reader.Read())
        //    {
        //        categoriesCount++;
        //    }


        //    Assert.Equal(categoriesCount, 3);
        //}

        //[Fact]
        //public void ExecuteValuesAsyncTest()
        //{
        //    var categoryIds = SimpleAccess.ExecuteValuesAsync<int>("Select Id FROM Categories").Result;

        //    Assert.Equal(categoryIds.Count(), 3);
        //}


        //[Fact]

        //public void ExecuteValuesAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM Categories").Result;

        //        Assert.Equal(values.Count(), 3);

        //        values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Branches]").Result;

        //        Assert.Equal(values.Count(), 2);

        //        values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Attachments]").Result;

        //        Assert.Equal(values.Count(), 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}

        //[Fact]
        //public void ExecuteEntitiesAsyncTest()
        //{
        //    var categoriesCount = SimpleAccess.ExecuteEntitiesAsync<Category>("Select Id, Name, Description FROM Categories").Result;

        //    Assert.Equal(categoriesCount.Count(), 3);
        //}


        //[Fact]

        //public void ExecuteEntitiesAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var categories = SimpleAccess.ExecuteEntitiesAsync<Category>(transContext, "Select Id, Name, Description FROM Categories").Result;

        //        Assert.Equal(categories.Count(), 3);

        //        var branches = SimpleAccess.ExecuteEntitiesAsync<Branch>(transContext, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

        //        Assert.Equal(branches.Count(), 2);

        //        var attachments = SimpleAccess.ExecuteEntitiesAsync<Attachment>(transContext, "Select Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

        //        Assert.Equal(attachments.Count(), 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}

        //[Fact]
        //public void ExecuteEntityAsyncTest()
        //{
        //    var category = SimpleAccess.ExecuteEntityAsync<Category>("Select Top 1 Id, Name, Description FROM Categories").Result;

        //    Assert.NotNull(category);
        //    Assert.Equal(category.Id, 2);
        //}


        //[Fact]

        //public void ExecuteEntityAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var category = SimpleAccess.ExecuteEntityAsync<Category>(transContext, "Select Top 1 Id, Name, Description FROM Categories").Result;

        //        Assert.NotNull(category);
        //        Assert.Equal(category.Id, 2);

        //        var branch = SimpleAccess.ExecuteEntityAsync<Branch>(transContext, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

        //        Assert.NotNull(branch);
        //        Assert.Equal(branch.Id, 1);

        //        var attachment = SimpleAccess.ExecuteEntityAsync<Attachment>(transContext, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

        //        Assert.NotNull(attachment);
        //        Assert.Equal(attachment.Id, 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}



        //[Fact]
        //public void ExecuteDynamicAsyncTest()
        //{
        //    var category = SimpleAccess.ExecuteDynamicAsync("Select Top 1 Id, Name, Description FROM Categories").Result;

        //    Assert.NotNull(category);
        //    Assert.Equal(category.Id, 2);
        //}


        //[Fact]

        //public void ExecuteDynamicAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var category = SimpleAccess.ExecuteDynamicAsync(transContext, "Select Top 1 Id, Name, Description FROM Categories").Result;

        //        Assert.NotNull(category);
        //        Assert.Equal(category.Id, 2);

        //        var branch = SimpleAccess.ExecuteDynamicAsync(transContext, "Select TOP 1 Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

        //        Assert.NotNull(branch);
        //        Assert.Equal(branch.Id, 1);

        //        var attachment = SimpleAccess.ExecuteDynamicAsync(transContext, "Select TOP 1 Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

        //        Assert.NotNull(attachment);
        //        Assert.Equal(attachment.Id, 5);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}

        //[Fact]
        //public void ExecuteDynamicsAsyncTest()
        //{
        //    var rowAffected = SimpleAccess.ExecuteNonQueryAsync("UPDATE Categories SET Description = @description WHERE Id = @id", 
        //        new { id = 2, description = "Updated description"}).Result;

        //    Assert.Equal(rowAffected, 1);
        //}


        //[Fact]
        //public void ExecuteDynamicsAsyncWithTransactionContextTest()
        //{
        //    using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
        //    {
        //        var rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Categories SET Description = @description WHERE Id = @id",
        //            new { id = 2, description = "Updated description with transaction" }).Result;

        //        Assert.Equal(rowAffected, 1);

        //        rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE [Branches] SET [Address2] = @address2 WHERE Id = @id",
        //            new { id = 2, address2 = "Updated Address2 with transaction" }).Result;


        //        Assert.Equal(rowAffected, 1);

        //        rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Attachments SET [OtherName] = @otherName WHERE Id = @id",
        //            new { id = 6, otherName = "Updated OtherName with transaction" }).Result;


        //        Assert.Equal(rowAffected, 1);

        //        SimpleAccess.EndTransaction(transContext);

        //    }
        //}
    }
}
