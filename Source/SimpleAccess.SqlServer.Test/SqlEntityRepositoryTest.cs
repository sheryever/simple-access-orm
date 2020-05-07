using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServer.TestNetCore2.Entities;
using Xunit;

namespace SimpleAccess.SqlServer.Test
{
    public class SqlEntityRepositoryTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }
        private ISqlRepository SqlRepository { get; set; }

        public SqlEntityRepositoryTest()
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            SqlRepository = new SqlEntityRepository(SimpleAccess);
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }


        [Fact]
        public void InsertWithSequenceTest()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };
            var rowAffected = SqlRepository.Insert<Person>(person);
            //var rowAffected = SqlRepository.Insert<Person>(person);

            // SqlRepository.GetDynamicPagedList<Person>((person1 => new {person.Id, person.FullName}), 0, 10);

            Assert.Equal(1, rowAffected);
        }
        [Fact]
        public void InsertWithIdentityTest()
        {
            var branch = new Branch2
            {
                Name = "New Branch",
                CityId = 2,
                PhoneNumbers = "234234234"
            };
            var rowAffected = SqlRepository.Insert<Branch2>(branch);
            //var rowAffected = SqlRepository.Insert<Person>(person);

            // SqlRepository.GetDynamicPagedList<Person>((person1 => new {person.Id, person.FullName}), 0, 10);

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
        public void GetDynamicPagedListTest()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id");

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTest()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }, null, 0, 2, "Id");

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetTest()
        {
            var category = SqlRepository.Get<Category>(2);
            

            Assert.NotNull(category);

            Assert.Equal(2, category.Id);
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
            Assert.Equal(2, category.Id);

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

        public void UpdateAllWithTransactionContextTest()
        {
            var people = SqlRepository.GetAll<Person>();

            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {

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
            var people = SqlRepository.GetAll<Person>().Select<Person, long>(p => p.Id);

            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {

                var rowAffected = SqlRepository.DeleteAll<Person>(transaction, people);

                Assert.Equal(rowAffected, people.Count());

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }
    }
}
