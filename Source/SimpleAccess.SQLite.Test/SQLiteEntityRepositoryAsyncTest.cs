using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.SQLite;
using SimpleAccess.SQLite.TestNetCore2.Entities;
using Xunit;

namespace SimpleAccess.SQLite.Test
{
     public class SQLiteEntityRepositoryAsyncTest
    {
        private static ISQLiteSimpleAccess SimpleAccess { get; set; }
        private ISQLiteRepository SQLiteRepository{ get; set; }


        public SQLiteEntityRepositoryAsyncTest()
        {
            SimpleAccess = new SQLiteSimpleAccess($@"Data Source={Environment.CurrentDirectory}\TempDb.db;Version=3;New=True;");
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
            var rowAffected = SQLiteRepository.InsertAsync<Person>(person).Result;
            //var rowAffected = SQLiteRepository.Insert<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void InsertAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SQLiteRepository.SimpleAccess.BeginTransactionAsync().Result)
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
                var rowAffected = SQLiteRepository.InsertAllAsync<Person>(transContext, people).Result;

                Assert.Equal(4, rowAffected);


                SQLiteRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void GetAllAsyncTest()
        {
            var categories = SQLiteRepository.GetAllAsync<Category>().Result;

            Assert.Equal(3, categories.Count());
        }

        [Fact]
        public void GetAsyncTest()
        {
            var category = SQLiteRepository.GetAsync<Category>(2).Result;

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }

        [Fact]

        public void GetAsyncWithTransactionContextTest()
        {
            using (var transContext = SQLiteRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SQLiteRepository.GetAsync<Category>(transContext, 2).Result;

                Assert.NotNull(category);

                var branch = SQLiteRepository.GetAsync<Branch>(transContext, 2).Result;

                Assert.NotNull(branch);

                var attachment = SQLiteRepository.GetAsync<Attachment>(transContext, 3).Result;

                Assert.NotNull(attachment);

                SQLiteRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void FindAsyncTest()
        {
            var category = SQLiteRepository.FindAsync<Category>(c => c.Id == 2).Result;

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }

        [Fact]

        public void FindAsyncWithTransactionContextTest()
        {
            using (var transContext = SQLiteRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SQLiteRepository.FindAsync<Category>(transContext, c => c.Id == 2).Result;

                Assert.NotNull(category);

                var branch = SQLiteRepository.FindAsync<Branch>(transContext, c => c.Id == 2).Result;

                Assert.NotNull(branch);

                var attachment = SQLiteRepository.FindAsync<Attachment>(transContext, c => c.Id == 2).Result;

                Assert.NotNull(attachment);

                SQLiteRepository.SimpleAccess.EndTransaction(transContext);

            }
        }


        [Fact]
        public void FindAllAsyncTest()
        {
            var categories = SQLiteRepository.FindAllAsync<Category>(c => c.Description.Contains("cat") ).Result;

            Assert.Equal(2, categories.Count());
        }

        [Fact]

        public void FindAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SQLiteRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var categories = SQLiteRepository.FindAllAsync<Category>(transContext, c => c.Description.Contains("cat")).Result;
                Assert.Equal(2, categories.Count());

                var branches = SQLiteRepository.FindAllAsync<Branch>(transContext, c => c.CityId == 1).Result;
                Assert.Equal(2, branches.Count());


                var attachments = SQLiteRepository.FindAllAsync<Attachment>(transContext, c => c.IncidentId == 3).Result;
                Assert.Equal(1, attachments.Count());

                SQLiteRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void UpdateAsyncTest()
        {
            var person = SQLiteRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SQLiteRepository.UpdateAsync<Person>(person).Result;

            Assert.Equal(1, rowAffected);
        }

        [Fact]

        public void UpdateAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SQLiteRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var people = SQLiteRepository.GetAll<Person>();

                foreach (var person in people)
                {
                    person.FullName = person.FullName + 1;
                }


                var rowAffected = SQLiteRepository.UpdateAllAsync<Person>(people).Result;

                Assert.Equal(rowAffected, people.Count());


                SQLiteRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void DeleteAsyncTest()
        {
            var person = SQLiteRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SQLiteRepository.DeleteAsync<Person>(person.Id).Result;

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void DeleteAllAsyncWithTransactionContextTest()
        {
            using (var transContext = SQLiteRepository.SimpleAccess.BeginTransactionAsync().Result)
            {
                var people = SQLiteRepository.GetAll<Person>().Select<Person, long>(p => p.Id);

                var rowAffected = SQLiteRepository.DeleteAllAsync<Person>(people).Result;

                Assert.Equal(rowAffected, people.Count());


                SQLiteRepository.SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void GetDynamicPagedListAsyncTestWithSelectAndWhereClause()
        {
            var people = SQLiteRepository.GetDynamicPagedListAsync<Person>(p => new { p.Id }, "WHERE ID > 1", 0, 2, "Id", false).Result;

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListAsyncTestWithSelectDistinctAndWhereClause()
        {
            var people = SQLiteRepository.GetDynamicPagedListAsync<Person>(true, p => new { p.Id }
                                , "WHERE ID > @id", 0, 2, "Id", false, new SQLiteParameter("@id", 1)).Result;

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
