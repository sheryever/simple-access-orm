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
    public class SqlEntityRepositoryTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }
        private ISqlRepository SqlRepository { get; set; }

        public SqlEntityRepositoryTest()
        {
            SimpleAccess = new SqlSimpleAccess("Data Source=.\\SQLEXPRESS2017;Initial Catalog=SimpleAccessTest;Persist Security Info=True;User ID=sa;Password=Test123;");
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



        [Fact]
        public void GetDynamicPagedListTest()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id");

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelect()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(p => new { p.Id }, 0, 2, "Id");

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }
        [Fact]
        public void GetDynamicPagedListTestWithObjectParameters()
        {
            var id = 1;
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id, FullName", new { id });

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSqlParameters()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id, FullName", new SqlParameter("@id", 1));

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTestWithLike()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }
            , "Where FullName like '%' + @fullName + '%'", 0, 2, "Id", new { fullName = "ah" });

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTest()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }, null, 0, 2, "Id");

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetCountTest()
        {
            var rowCount = SqlRepository.GetCount<Person>();

            Assert.Equal(3, rowCount);

        }

        [Fact]
        public void GetCountTestWithWhere()
        {
            var rowCount = SqlRepository.GetCount<Person>(p => p.Id == 1);

            Assert.Equal(1, rowCount);

        }

        [Fact]
        public void GetCountTestWithSingleColumn()
        {
            var rowCount = SqlRepository.GetCount<Person, string>(select => select.Address);

            Assert.Equal(2, rowCount);

        }

        [Fact]
        public void GetCountTestWithSelect()
        {
            var counts = SqlRepository.GetCount<Branch>(p => new { p.Id, p.CityId });

            Assert.Equal(3, counts.CountOfId);
            Assert.Equal(3, counts.CountOfCityId);
        }

        [Fact]
        public void GetCountTestWithSelectAndWhere()
        {
            var counts = SqlRepository.GetCount<Branch>(p => new { p.Id, p.CityId }, where => where.Id == 1);

            Assert.Equal(1, counts.CountOfId);
            Assert.Equal(1, counts.CountOfCityId);

        }

        [Fact]
        public void GetSumTest()
        {
            var sumOfSalary = SqlRepository.GetSum<Person>(p => p.BasicSalary);

            Assert.Equal(12000, sumOfSalary);

        }

        [Fact]
        public void GetSumTestWithMultiColumn()
        {
            var sum = SqlRepository.GetSum<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(12000, sum.SumOfBasicSalary);
            Assert.Equal(1000, sum.SumOfTransport);
        }


        [Fact]
        public void GetSumTestWithSingleNullableColumn()
        {
            var sumOfTransport = SqlRepository.GetSum<Person>(p => p.Transport);

            Assert.Equal(1000, sumOfTransport);
        }

        [Fact]
        public void GetSumTestWithSelectAndWhere()
        {
            var sumOfSalary = SqlRepository.GetSum<Person>(p => p.BasicSalary, where => where.Id > 1);

            Assert.Equal(8000, sumOfSalary);

        }


        [Fact]
        public void GetMinTest()
        {
            var MinOfSalary = SqlRepository.GetMin<Person>(p => p.BasicSalary);

            Assert.Equal(3000, MinOfSalary);

        }

        [Fact]
        public void GetMinTestWithMultiColumn()
        {
            var Min = SqlRepository.GetMin<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(3000, Min.MinOfBasicSalary);
            Assert.Equal(300, Min.MinOfTransport);
        }


        [Fact]
        public void GetMinTestWithSingleNullableColumn()
        {
            var MinOfTransport = SqlRepository.GetMin<Person>(p => p.Transport);

            Assert.Equal(300, MinOfTransport);
        }

        [Fact]
        public void GetMinTestWithSelectAndWhere()
        {
            var MinOfSalary = SqlRepository.GetMin<Person>(p => p.BasicSalary, where => where.Id > 1);

            Assert.Equal(3000, MinOfSalary);

        }


        [Fact]
        public void GetMaxTest()
        {
            var MaxOfSalary = SqlRepository.GetMax<Person>(p => p.BasicSalary);

            Assert.Equal(5000, MaxOfSalary);

        }

        [Fact]
        public void GetMaxTestWithMultiColumn()
        {
            var Max = SqlRepository.GetMax<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(5000, Max.MaxOfBasicSalary);
            Assert.Equal(700, Max.MaxOfTransport);
        }


        [Fact]
        public void GetMaxTestWithSingleNullableColumn()
        {
            var MaxOfTransport = SqlRepository.GetMax<Person>(p => p.Transport);

            Assert.Equal(700, MaxOfTransport);
        }

        [Fact]
        public void GetMaxTestWithSelectAndWhere()
        {
            var MaxOfSalary = SqlRepository.GetMax<Person>(p => p.BasicSalary, where => where.Id > 2);

            Assert.Equal(3000, MaxOfSalary);

        }


        [Fact]
        public void GetAvgTest()
        {
            var AvgOfSalary = SqlRepository.GetAverage<Person>(p => p.BasicSalary);

            Assert.Equal(4000, AvgOfSalary);

        }

        [Fact]
        public void GetAvgTestWithMultiColumn()
        {
            var Avg = SqlRepository.GetAverage<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(4000, Avg.AvgOfBasicSalary);
            Assert.Equal(500, Avg.AvgOfTransport);
        }


        [Fact]
        public void GetAvgTestWithSingleNullableColumn()
        {
            var AvgOfTransport = SqlRepository.GetAverage<Person>(p => p.Transport);

            Assert.Equal(500, AvgOfTransport);
        }

        [Fact]
        public void GetAvgTestWithSelectAndWhere()
        {
            var AvgOfSalary = SqlRepository.GetAverage<Person>(p => p.BasicSalary, where => where.Id > 1);

            Assert.Equal(4000, AvgOfSalary);

        }

        [Fact]
        public void GetAggregateTest()
        {
            var data = SqlRepository.GetAggregateFirstOrDefault<Employee>(
                countOf: co => co,
                sumOf: so => new { so.BasicSalary, so.Inssurance, so.Transport },
                maxOf: mo => new { mo.BasicSalary, mo.Inssurance, mo.Transport },
                minOf: mi => new { mi.BasicSalary, mi.Inssurance, mi.Transport }
            );
            Assert.True(data != null);

        }


        [Fact]
        public void GetAggregateTestWithWhereAndGroupBy()
        {
            var data = SqlRepository.GetAggregate<Employee>(
                countOf: co => co,
                sumOf: so => new { so.BasicSalary, so.Inssurance, so.Transport },
                maxOf: mo => new { mo.BasicSalary, mo.Inssurance, mo.Transport },
                minOf: mi => new { mi.BasicSalary, mi.Inssurance, mi.Transport },
                where: w => w.Id > 2,
                groupBy: g => new { g.Department }
            );


            Assert.True(data.Any());
        }

        [Fact]
        public void GetAggregateTestWithWhereGroupByHaving()
        {
            var data = SqlRepository.GetAggregate<Employee>(
                countOf: co => co,
                sumOf: so => new { so.BasicSalary, so.Inssurance, so.Transport },
                maxOf: mo => new { mo.BasicSalary, mo.Inssurance, mo.Transport },
                minOf: mi => new { mi.BasicSalary, mi.Inssurance, mi.Transport },
                where: w => w.Id > 2,
                groupBy: g => new { g.Department },
                having: hv => hv.Min(s => s.BasicSalary).GreaterThanEqualTo(2000)
                                .And().Min(s => s.BasicSalary).LessThan(9000)
            );


            Assert.True(data.Any());
        }
    }
}
