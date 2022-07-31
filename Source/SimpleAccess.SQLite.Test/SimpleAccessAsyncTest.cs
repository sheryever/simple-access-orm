using System;
using System.Data;
using System.Linq;
using SimpleAccess.Core;
using SimpleAccess.SQLite;
using SimpleAccess.SQLite.TestNetCore2.Entities;
using Xunit;
using static Xunit.Assert;

namespace SimpleAccess.SQLite.Test
{
    public class SimpleAccessAsyncTest
    {
        private static ISQLiteSimpleAccess SimpleAccess { get; set; }

        public SimpleAccessAsyncTest()
        {
            SimpleAccess = new SQLiteSimpleAccess($@"Data Source={Environment.CurrentDirectory}\TempDb.db;Version=3;New=True;");
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }

        [Fact]
        public void ExecuteScalarAsyncTest()
        {
            var categoriesCount = SimpleAccess.ExecuteScalarAsync<int>("Select Count(*) FROM Categories").Result;

            Equal(3, categoriesCount);
        }


        [Fact]

        public void ExecuteScalarAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var rowCount = SimpleAccess.ExecuteScalarAsync<int>(transContext, "Select Count(*) FROM Categories").Result;

                Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalarAsync<int>(transContext, "Select Count(*) FROM [Branches]").Result;

                Equal(3, rowCount);

                rowCount = SimpleAccess.ExecuteScalarAsync<int>(transContext, "Select Count(*) FROM [Attachments]").Result;


                Equal(3, rowCount);


                SimpleAccess.EndTransaction(transContext);

            }
        }


        [Fact]
        public void ExecuteReaderAsyncTest()
        {
            var categoriesCount = 0;
                
            var reader = SimpleAccess.ExecuteReaderAsync("Select * FROM Categories").Result;

            while (reader.Read())
            {
                categoriesCount++;
            }


            Equal(3, categoriesCount);
        }
        
        [Fact]
        public void ExecuteValuesAsyncTest()
        {
            var categoryIds = SimpleAccess.ExecuteValuesAsync<int>("Select Id FROM Categories").Result;

            Equal(3, categoryIds.Count());
        }

        [Fact]
        public void ExecuteValuesAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM Categories").Result;

                Equal(3, values.Count());

                values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Branches]").Result;

                Equal(3, values.Count());

                values = SimpleAccess.ExecuteValuesAsync<int>(transContext, "Select Id FROM [Attachments]").Result;

                Equal(3, values.Count());

                SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void ExecuteEntitiesAsyncTest()
        {
            var categoriesCount = SimpleAccess.ExecuteEntitiesAsync<Category>("Select Id, Name, Description FROM Categories").Result;

            Equal(3, categoriesCount.Count());
        }


        [Fact]

        public void ExecuteEntitiesAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var categories = SimpleAccess.ExecuteEntitiesAsync<Category>(transContext, "Select Id, Name, Description FROM Categories").Result;

                Equal(3, categories.Count());

                var branches = SimpleAccess.ExecuteEntitiesAsync<Branch>(transContext, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches]").Result;

                Equal(3, branches.Count());

                var attachments = SimpleAccess.ExecuteEntitiesAsync<Attachment>(transContext, "Select Id, [IncidentId], [OtherName] FROM [Attachments]").Result;

                Equal(3, attachments.Count());

                SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void ExecuteEntityAsyncTest()
        {
            var category = SimpleAccess.ExecuteEntityAsync<Category>("Select Id, Name, Description FROM Categories LIMIT 1").Result;

            NotNull(category);
            Equal(1, category.Id);
        }


        [Fact]

        public void ExecuteEntityAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SimpleAccess.ExecuteEntityAsync<Category>(transContext, "Select Id, Name, Description FROM Categories LIMIT 1").Result;

                NotNull(category);
                Equal(1, category.Id);

                var branch = SimpleAccess.ExecuteEntityAsync<Branch>(transContext, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches] LIMIT 1").Result;

                NotNull(branch);
                Equal(1, branch.Id);

                var attachment = SimpleAccess.ExecuteEntityAsync<Attachment>(transContext, "Select Id, [IncidentId], [OtherName] FROM [Attachments] LIMIT 1").Result;

                NotNull(attachment);
                Equal(1, attachment.Id);

                SimpleAccess.EndTransaction(transContext);

            }
        }



        [Fact]
        public void ExecuteDynamicAsyncTest()
        {
            var category = SimpleAccess.ExecuteDynamicAsync("Select Id, Name, Description FROM Categories LIMIT 1").Result;

            NotNull(category);
            Equal(1, category.Id);
        }


        [Fact]

        public void ExecuteDynamicAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var category = SimpleAccess.ExecuteDynamicAsync(transContext, "Select Id, Name, Description FROM Categories LIMIT 1").Result;

                NotNull(category);
                Assert.Equal(category.Id, 1);

                var branch = SimpleAccess.ExecuteDynamicAsync(transContext, "Select Id, CityId, Name, [PhoneNumbers], [Address], [Address2] FROM [Branches] LIMIT 1").Result;

                NotNull(branch);
                Assert.Equal(branch.Id, 1);

                var attachment = SimpleAccess.ExecuteDynamicAsync(transContext, "Select Id, [IncidentId], [OtherName] FROM [Attachments] LIMIT 1").Result;

                NotNull(attachment);
                Assert.Equal(attachment.Id, 1);

                SimpleAccess.EndTransaction(transContext);

            }
        }

        [Fact]
        public void ExecuteDynamicsAsyncTest()
        {
            var rowAffected = SimpleAccess.ExecuteNonQueryAsync("UPDATE Categories SET Description = @description WHERE Id = @id", 
                new { id = 2, description = "Updated description"}).Result;

            Equal(1, rowAffected);
        }


        [Fact]
        public void ExecuteDynamicsAsyncWithTransactionContextTest()
        {
            using (var transContext = SimpleAccess.BeginTransactionAsync().Result)
            {
                var rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Categories SET Description = @description WHERE Id = @id",
                    new { id = 1, description = "Updated description with transaction" }).Result;

                Equal(1, rowAffected);

                rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE [Branches] SET [Address2] = @address2 WHERE Id = @id",
                    new { id = 1, address2 = "Updated Address2 with transaction" }).Result;


                Equal(1, rowAffected);

                rowAffected = SimpleAccess.ExecuteNonQueryAsync(transContext, "UPDATE Attachments SET [OtherName] = @otherName WHERE Id = @id",
                    new { id = 1, otherName = "Updated OtherName with transaction" }).Result;


                Equal(1, rowAffected);

                SimpleAccess.EndTransaction(transContext);

            }
        }
    }
}
