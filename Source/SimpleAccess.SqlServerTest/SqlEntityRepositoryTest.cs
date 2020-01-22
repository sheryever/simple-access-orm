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
    public class SqlEntityRepositoryTest
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
        public void InsertTest()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123"
            };
            var rowAffected = SqlRepository.Insert<Person>(person);
            //var rowAffected = SqlRepository.Insert<Person>(person);

            Assert.AreEqual(rowAffected, 1);
        }

        [TestMethod]
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

                Assert.AreEqual(rowAffected, 4);


                SqlRepository.SimpleAccess.EndTransaction(trasaction);

            }
        }

        [TestMethod]
        public void GetAllTest()
        {
            var categories = SqlRepository.GetAll<Category>();

            Assert.AreEqual(categories.Count(), 3);
        }

        [TestMethod]
        public void GetTest()
        {
            var category = SqlRepository.Get<Category>(2);

            Assert.IsNotNull(category);
        }

        [TestMethod]

        public void GetWithTransactionContextTest()
        {
            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var category = SqlRepository.Get<Category>(transaction, 2);

                Assert.IsNotNull(category);

                var branch = SqlRepository.Get<Branch>(transaction, 2);

                Assert.IsNotNull(branch);

                var attachment = SqlRepository.Get<Attachment>(transaction, 3);

                Assert.IsNotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [TestMethod]
        public void FindTest()
        {
            var category = SqlRepository.Find<Category>(c => c.Id == 2);

            Assert.IsNotNull(category);
        }

        [TestMethod]

        public void FindWithTransactionContextTest()
        {
            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var category = SqlRepository.Find<Category>(transaction, c => c.Id == 2);

                Assert.IsNotNull(category);

                var branch = SqlRepository.Find<Branch>(transaction, c => c.Id == 2);

                Assert.IsNotNull(branch);

                var attachment = SqlRepository.Find<Attachment>(transaction, c => c.Id == 2);

                Assert.IsNotNull(attachment);

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }


        [TestMethod]
        public void FindAllTest()
        {
            var categories = SqlRepository.FindAll<Category>(c => c.Description.Contains("cat") );

            Assert.AreEqual(2 , categories.Count());
        }

        [TestMethod]

        public void FindAllWithTransactionContextTest()
        {
            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {
                var categories = SqlRepository.FindAll<Category>(transaction, c => c.Description.Contains("cat"));
                Assert.AreEqual(2, categories.Count());

                var branches = SqlRepository.FindAll<Branch>(transaction, c => c.CityId == 1);
                Assert.AreEqual(2, branches.Count());


                var attachments = SqlRepository.FindAll<Attachment>(transaction, c => c.IncidentId == 3);
                Assert.AreEqual(1, attachments.Count());

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.Update<Person>(person);

            Assert.AreEqual(rowAffected, 1);
        }

        [TestMethod]

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

                Assert.AreEqual(rowAffected, people.Count());


                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            var person = SqlRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SqlRepository.Delete<Person>(person.Id);

            Assert.AreEqual(rowAffected, 1);
        }

        [TestMethod]
        public void DeleteAllWithTransactionContextTest()
        {
            var people = SqlRepository.GetAll<Person>().Select<Person, long>(p => p.Id);

            using (var transaction = SqlRepository.SimpleAccess.BeginTransaction())
            {

                var rowAffected = SqlRepository.DeleteAll<Person>(transaction, people);

                Assert.AreEqual(rowAffected, people.Count());

                SqlRepository.SimpleAccess.EndTransaction(transaction);

            }
        }
    }
}
