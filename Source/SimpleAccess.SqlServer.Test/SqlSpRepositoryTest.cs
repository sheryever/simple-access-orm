using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
 using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServer.TestNetCore2.Entities;
using Xunit;

namespace SimpleAccess.SqlServer.Test
{
     public class SqlSpRepositoryTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }
        private ISqlRepository SqlRepository { get; set; }

         public SqlSpRepositoryTest( )
        {
            SimpleAccess = new SqlSimpleAccess("Data Source=.\\SQLEXPRESS2017;Initial Catalog=SimpleAccessTest;Persist Security Info=True;User ID=sa;Password=Test123;");
            SqlRepository = new SqlSpRepository(SimpleAccess);
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }

        [Fact]
        public void InsertTest1()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };
            var rowAffected = SqlRepository.Insert<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void InsertWithTransactionTest()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };

            var rowAffected = 0;
            using (var transaction = SimpleAccess.BeginTransaction())
            {
                rowAffected = SqlRepository.Insert<Person>(transaction, person);

            }

            Assert.Equal(1, rowAffected);
        }


        [Fact]
        public void InsertAnonymousObjectTest()
        {
            var person = new
            {
                Id = 0,
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };
            var rowAffected = SqlRepository.Insert<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void InsertSqlParametersTest()
        {
            var person = new SqlParameter[]
            {
                new SqlParameter("Id", (object)0){ Direction = ParameterDirection.InputOutput},
                new SqlParameter( "FullName", "Muhammad Abdul Rehman Khan"),
                new SqlParameter( "Phone" , "1112182123")
            };

            var rowAffected = SqlRepository.Insert<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void InsertAllAWithTransactionContextTest()
        {
            using (var trasaction = SqlRepository.SimpleAccess.BeginTransaction())
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
                var rowAffected = SqlRepository.InsertAll<Person>(trasaction, people);

                Assert.Equal(4, rowAffected);


                SqlRepository.SimpleAccess.EndTransaction(trasaction);

            }
        }

        [Fact]
        public void GetAllTest()
        {
            var categories = SqlRepository.GetAll<Category>();

            Assert.Equal(3, categories.Count());
        }

        [Fact]
        public void GetAllWithTransactionTest()
        {
            using (var trasaction = SqlRepository.SimpleAccess.BeginTransaction())
            {

                var categories = SqlRepository.GetAll<Category>(trasaction);
                Assert.Equal(3, categories.Count());

            }

        }

        [Fact]
        public void MultipleGetAllWithTransactionTest()
        {
            using (var trasaction = SqlRepository.SimpleAccess.BeginTransaction())
            {

                var categories = SqlRepository.GetAll<Category>(trasaction);
                var people = SqlRepository.GetAll<Person>(trasaction);
                Assert.Equal(3, categories.Count());
                Assert.True (people.Any());

            }

        }
        [Fact]
        public void GetDynamicPagedListTest()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListWithSelectTest()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(p => new {p.Id, p.FullName}, null , 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        //[Fact]
        //public void GetAllWithTransactionTest()
        //{
        //    using (var trasaction = SqlRepository.SimpleAccess.BeginTransaction())
        //    {

        //        var categories = SqlRepository.GetAll<Category>(trasaction);
        //        Assert.Equal(categories.Count(), 3);

        //    }

        //}

        [Fact]
        public void GetTest()
        {
            var category = SqlRepository.Get<Category>(2);

            Assert.NotNull(category);
        }

        [Fact]

        public void GetWithTransactionContextTest()
        {
            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var category = SqlRepository.Get<Category>(transaction, 2);

                Assert.NotNull(category);

                var branch = SqlRepository.Get<Branch>(transaction, 2);

                Assert.NotNull(branch);

                var attachment = SqlRepository.Get<Attachment>(transaction, 3);

                Assert.NotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void FindTest()
        {
            var category = SqlRepository.Find<Category>(c => c.Id == 2);

            Assert.NotNull(category);
        }

        [Fact]

        public void FindWithTransactionContextTest()
        {
            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var category = SqlRepository.Find<Category>(transaction, c => c.Id == 2);

                Assert.NotNull(category);

                var branch = SqlRepository.Find<Branch>(transaction, c => c.Id == 2);

                Assert.NotNull(branch);

                var attachment = SqlRepository.Find<Attachment>(transaction, c => c.Id == 2);

                Assert.NotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }


        [Fact]
        public void FindAllTest()
        {
            var categories = SqlRepository.FindAll<Category>(c => c.Description.Contains("cat"));

            Assert.Equal(2, categories.Count());
        }

        [Fact]

        public void FindAllWithTransactionContextTest()
        {
            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var categories = SqlRepository.FindAll<Category>(transaction, c => c.Description.Contains("cat"));
                Assert.Equal(2, categories.Count());

                var branches = SqlRepository.FindAll<Branch>(transaction, c => c.CityId == 1);
                Assert.Equal(2, branches.Count());


                var attachments = SqlRepository.FindAll<Attachment>(transaction, c => c.IncidentId == 3);
                Assert.Equal(1, attachments.Count());

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void UpdateTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.Update<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]

        public void UpdateAllTest()
        {
            var people = SqlRepository.GetAll<Person>();


            foreach (var person in people)
            {
                person.FullName = person.FullName + 1;
            }


            var rowAffected = SqlRepository.UpdateAll<Person>(people);

            Assert.Equal(rowAffected, people.Count());



        }

        [Fact]

        public void UpdateAllWithTransactionTest()
        {

            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var people = SqlRepository.GetAll<Person>(transaction);

                foreach (var person in people)
                {
                    person.FullName = person.FullName + 1;
                }


                var rowAffected = SqlRepository.UpdateAll<Person>(transaction, people);

                Assert.Equal(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void DeleteTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.Delete<Person>(person.Id);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void DeleteAllWithTransactionContextTest()
        {

            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var people = SqlRepository.GetAll<Person>(transaction).Select<Person, long>(p => p.Id);

                var rowAffected = SqlRepository.DeleteAll<Person>(transaction, people);

                Assert.Equal(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }
    }
}
