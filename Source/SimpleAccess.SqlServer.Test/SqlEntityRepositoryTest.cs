using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
#if NETFULL
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif
using SimpleAccess.Core;
using SimpleAccess.Core.Extensions;
using SimpleAccess.Core.SqlSyntaxExtensions;
using SimpleAccess.SqlServer;
using SimpleAccess.SqlServer.TestNetCore2.Entities;
using Xunit;
using System;

namespace SimpleAccess.SqlServer.Test
{
    public class SqlEntityRepositoryTest
    {
        private ISqlSimpleAccess SimpleAccess { get; set; }
        private ISqlRepository SqlRepository { get; set; }

        public SqlEntityRepositoryTest()
        {
            SimpleAccess = new SqlSimpleAccess("Data Source=.\\SQLEXPRESS2017;Initial Catalog=SimpleAccessTest;Persist Security Info=True;User ID=sa;Password=Test123;TrustServerCertificate=True;");
            SqlRepository = new SqlEntityRepository(SimpleAccess);
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }


        [Fact]
        public void InsertWithSequenceTest()
        {
            var person = new Person
            {
                FullName = "Muhammad Abdul Rehman Khan",
                Phone = "1112182123",
                DOB = DateTime.Now.AddYears(-20)
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
        public void InsertWithDatabaseUniqueIdentifierTest()
        {
            var product = new ProductDatabaseUniqueIdentifier
            {
                Name = "Product ProductDatabaseUniqueIdentifier",
            };
            var rowAffected = SqlRepository.Insert<ProductDatabaseUniqueIdentifier>(product);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void InsertWithClientUniqueIdentifierTest()
        {
            var product = new ProductCleintGuid
            {
                Name = "Product ProductCleintGuid",
            };
            var rowAffected = SqlRepository.Insert<ProductCleintGuid>(product);

            Assert.Equal(1, rowAffected);
        }


        [Fact]
        public void InsertWithUserDefineIdentifierTest()
        {
            var product = new ProductUserDefine
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Product ProductUserDefine",
            };
            var rowAffected = SqlRepository.Insert<ProductUserDefine>(product);

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
                        Phone = "1112182123",
                        DOB = DateTime.Now.AddYears(-20)
                    },
                    new Person
                    {
                        FullName = "Muhammad Sharjeel",
                        Phone = "0599065644",
                        DOB = DateTime.Now.AddYears(-20)
                    },
                    new Person
                    {
                        FullName = "Muhammad Affan",
                        Phone = "1112182123",
                        DOB = DateTime.Now.AddYears(-20)
                    },
                    new Person
                    {
                        FullName = "Muhammad Usman",
                        Phone = "1112182123",
                        DOB = DateTime.Now.AddYears(-20)
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
                Assert.True(people.Any());

            }

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
        public void FindTestNullableBoolTrue()
        {
            var person = SqlRepository.Find<Person>(c => c.Alive == true);

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestNullableBoolNull()
        {
            var person = SqlRepository.Find<Person>(c => c.Alive == null);

            Assert.NotNull(person);
            Assert.Equal(3, person.Id);

        }

        [Fact]
        public void FindTestWithInClauseFunction()
        {
            var category = SqlRepository.Find<Category>(c => c.Id.Contains(2,1));

            Assert.NotNull(category);
            Assert.Equal(1, category.Id);

        }

        [Fact]
        public void FindTestWithInClauseFunction2()
        {
            var categories = SqlRepository.FindAll<Category>(c => c.Id.Contains(2, 1));

            Assert.Equal(2, categories.Count());

        }

        [Fact]
        public void FindTestWithEnumFunction()
        {
            var person = SqlRepository.Find<Person>(c => c.Gender == Gender.Male);

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestWithNullableEnumFunction()
        {
            var person = SqlRepository.Find<Person>(c => c.Gender == (Gender?)Gender.Male);

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestWithEnumDonContainsAndNullValueFunction()
        {
            var person = SqlRepository.Find<Person>(c => c.Gender.In(Gender.Male, null));

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestWithLikeClauseUsingContainFunction()
        {
            var category = SqlRepository.Find<Category>(c => c.Name.Contains("CATE"));

            Assert.NotNull(category);

        }
        [Fact]
        public void FindTestWithDifferentType()
        {
            long id = 2;
            var category = SqlRepository.Find<Category>(c => c.Id == id);

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }
        [Fact]
        public void FindTestWithDifferentNullableType()
        {
            long? id = 2;
            var category = SqlRepository.Find<Category>(c => c.Id == id);

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }


        [Fact]
        public void FindTestForIsNullQuery()
        {
            var people = SqlRepository.FindAll<Person>(c => c.Address == null);

            Assert.True(people.Any());

        }

        [Fact]
        public void FindTestForIsNullAndOtherValueQuery()
        {
            var people = SqlRepository.FindAll<Person>(c => c.Address == null && c.Id == 3);

            Assert.True(people.Any());

        }

        [Fact]
        public void FindTestForIsNotNullQuery()
        {
            var people = SqlRepository.FindAll<Person>(c => c.Address != null);

            Assert.True(people.Any());

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
                Assert.Single(attachments);

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
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelect()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(p => new { p.Id }, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelectDistinct()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(true, p => new { p.Id }, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelectAndWhereClause()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(p => new { p.Id }, "WHERE ID > 1", 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelectDistinctAndWhereClause()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(true, p => new { p.Id }
                                , "WHERE ID > @id", 0, 2, "Id", false, new SqlParameter( "@id", 1 ));

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithObjectParameters()
        {
            var id = 1;
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id, FullName", new { id }, true);

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSqlParameters()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(0, 2, "Id, FullName", false, new SqlParameter("@id", 1));

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTestWithSelectDistinct()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(true, p => new { p.Id }, null, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTestWithLike()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }
            , "Where FullName like '%' + @fullName + '%'", 0, 2, "Id", new { fullName = "ah" }, true);

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTest()
        {
            var people = SqlRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }, null, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void IsExistTest()
        {
            var result = SqlRepository.IsExist<Person>();

            Assert.True(result);

        }

        [Fact]
        public void IsExistTestTestWithSelector()
        {
            var result = SqlRepository.IsExist<Person>(p => p.Id);

            Assert.True(result);

        }

        [Fact]
        public void IsExistTestTestWithWhereTrue()
        {
            var result = SqlRepository.IsExist<Person>(p => p.Id == 1);

            Assert.True(result);
        }

        [Fact]
        public void IsExistTestTestWithWhereFalse()
        {
            var result = SqlRepository.IsExist<Person>(p => p.Id > 100);

            Assert.False(result);
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
        public void GetCountTestWithWhereAndClassProperty()
        {
            var person = new Person() { Id = 1 };

            var rowCount = SqlRepository.GetCount<Person>(p => p.Id == person.Id);

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
        public void GetMaxWithDateTimeTest()
        {
            var lastDob = SqlRepository.GetMax<Person>(p => new { p.DOB });

            Assert.NotNull(lastDob.MaxOfDOB);

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
        public void InClauseWithArrayVariableTest()
        {
            var ids = new[] { 1, 2 };
            var found = SqlRepository.FindAll<Person>(p => p.Id.In(ids));

            Assert.True(found.Any());
        }

        [Fact]
        public void InClauseWithArrayValuesTest()
        {
            var found = SqlRepository.FindAll<Person>(p => p.Id.In(new[] { 1, 2 }));

            Assert.True(found.Any());
        }

        [Fact]
        public void WhereClauseWithDirectTrueBoolPropertyTest()
        {
            var data = SqlRepository.FindAll<Employee>(e => e.IsOnDuty);

            Assert.Equal(10, data.Count());
        }


        [Fact]
        public void WhereClauseWithDirectFalseBoolPropertyTest()
        {
            var data = SqlRepository.FindAll<Employee>(e => e.IsOnDuty == false);

            Assert.Equal(4, data.Count());
        }

        [Fact]
        public void InClauseExpressionTest()
        {
            var found = SqlRepository.FindAll<Person>(p => p.Id.In<Person, int>(pr => pr.Id));

            Assert.True(found.Any());

        }

        [Fact]
        public void InClauseWithSubWhereExpressionTest()
        {
            var found = SqlRepository.FindAll<Person>(p => p.Id.In<Category, int>(
                                    selct => selct.Id, categoryWhere => categoryWhere.Id < 10));
            Assert.True(found.Any());

        }
    
        [Fact]
        public void TestGetDynamicPage()
        {
            var people = SqlRepository.GetDynamicPagedList<Person>(e => new { e.Id, e.FullName }, 0, 1000, "FullName", false);
            Assert.Equal(3, people.Data.Count());
        }

        //[Fact]
        //public void GetAggregateTest()
        //{
        //    var data = SqlRepository.GetAggregate<Employee>(
        //        aggregator: (ag, e) => new { SumOfBasicSalary = ag.Sum(e.BasicSalary), MaxOfBasicSalary = ag.Max(e.BasicSalary) }
        //    );
        //    Assert.True(data != null);

        //}
        [Fact]
        public void GetAggregateTestWithWheres ()
        {
            var data = SqlRepository.GetAggregate <Employee>(
                aggregator: (ag, e) => new
                {
                    SumOfBasicSalary = ag.Sum(e.BasicSalary),
                    MaxOfBasicSalary = ag.Max(e.BasicSalary),
                    MinOfBasicSalary = ag.Min(e.BasicSalary),
                    AvgOfBasicSalary = ag.Average(e.BasicSalary)
                },
                where: w => w.Id > 2  
             );  
             
            Assert.NotNull(data);
        }

        [Fact]
        public void GetAggregateTestWithWhereAndGroupBy()
        {
            var data = SqlRepository.GetAggregateWithGroupBy<Employee>(
                aggregator: (ag, e) => new
                {
                    SumOfBasicSalary = ag.Sum(e.BasicSalary),
                    MaxOfBasicSalary = ag.Max(e.BasicSalary),
                    MinOfBasicSalary = ag.Min(e.BasicSalary),
                    AvgOfBasicSalary = ag.Average(e.BasicSalary)
                },
                where: w => w.Id > 2,
                groupBy: g => new { g.Department }
            );

            Assert.True(data.Any());
        }

        [Fact]
        public void GetAggregateTestWithWhereGroupByHaving()
        {
            var data = SqlRepository.GetAggregateWithGroupBy<Employee>(
                    aggregator: (ag, p) => new
                    {
                        Count = ag.Count(p),
                        SumOfBasicSalary = ag.Sum(p.BasicSalary),
                        SumOfInssurance = ag.Sum(p.Inssurance),
                        SumOfTransport = ag.Sum(p.Transport)
        ,
                        MaxOfBasicSalary = ag.Max(p.BasicSalary),
                        MaxOfInssurance = ag.Max(p.Inssurance),
                        MaxOfTransport = ag.Max(p.Transport),
                        MinOfBasicSalary = ag.Min(p.BasicSalary),
                        MinOfInssurance = ag.Min(p.Inssurance),
                        MinOfTransport = ag.Min(p.Transport)
                    },
                where: w => w.Id > 2,
                groupBy: g => new { g.Department },
                having: (hv, s) => hv.Min(s.BasicSalary) >= 2000
                                  && hv.Max(s.BasicSalary) < 9000
            );

            Assert.True(data.Any());
            Assert.Single(data);

        }

#if !NETFULL

        [Fact]
        public void CheckCultureValueOfDecimal()
        {
            var currentCulture = CultureInfo.CurrentCulture;

            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
            decimal? value = 3.00M;
            object objValue = (object)value;
            var data = SqlRepository.Find<Employee>(e => e.Id == 1 &&  e.Transport > value);

            Assert.NotNull(data);
            CultureInfo.CurrentCulture = currentCulture;
        }
#endif

    }
}
