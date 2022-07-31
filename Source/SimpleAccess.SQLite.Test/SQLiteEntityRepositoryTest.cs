using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.Core.Extensions;
using SimpleAccess.Core.SqlSyntaxExtensions;
using SimpleAccess.SQLite;
using SimpleAccess.SQLite.TestNetCore2.Entities;
using Xunit;

namespace SimpleAccess.SQLite.Test
{
    public class SQLiteEntityRepositoryTest
    {
        private static ISQLiteSimpleAccess SimpleAccess { get; set; }
        private ISQLiteRepository SQLiteRepository { get; set; }

        public SQLiteEntityRepositoryTest()
        {
            SimpleAccess = new SQLiteSimpleAccess($@"Data Source={Environment.CurrentDirectory}\TempDb.db;Version=3;New=True;");
            SQLiteRepository = new SQLiteEntityRepository(SimpleAccess);
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialScript);
        }

        [Fact]
        public void InsertWithIdentityTest()
        {
            var branch = new Branch
            {
                Name = "New Branch",
                CityId = 2,
                PhoneNumbers = "234234234"
            };
            var rowAffected = SQLiteRepository.Insert<Branch>(branch);
            //var rowAffected = SQLiteRepository.Insert<Person>(person);

            // SQLiteRepository.GetDynamicPagedList<Person>((person1 => new {person.Id, person.FullName}), 0, 10);

            Assert.Equal(1, rowAffected);
        }
        [Fact]
        public void InsertAllAWithTransactionContextTest()
        {
            using (var trasaction = SQLiteRepository.SimpleAccess.BeginTransaction())
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
                var rowAffected = SQLiteRepository.InsertAll<Person>(trasaction, people);

                Assert.Equal(4, rowAffected);


                SQLiteRepository.SimpleAccess.EndTransaction(trasaction);

            }
        }

        [Fact]
        public void GetAllTest()
        {
            var categories = SQLiteRepository.GetAll<Category>();

            Assert.Equal(3, categories.Count());
        }



        [Fact]
        public void GetTest()
        {
            var category = SQLiteRepository.Get<Category>(2);


            Assert.NotNull(category);

            Assert.Equal(2, category.Id);
        }

        [Fact]

        public void GetWithTransactionContextTest()
        {
            using (var transaction = SQLiteRepository.SimpleAccess.BeginTransaction())
            {
                var category = SQLiteRepository.Get<Category>(transaction, 2);

                Assert.NotNull(category);

                var branch = SQLiteRepository.Get<Branch>(transaction, 2);

                Assert.NotNull(branch);

                var attachment = SQLiteRepository.Get<Attachment>(transaction, 3);

                Assert.NotNull(attachment);

                SQLiteRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void FindTest()
        {
            var category = SQLiteRepository.Find<Category>(c => c.Id == 2);

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }

        [Fact]
        public void FindTestWithInClauseFunction()
        {
            var category = SQLiteRepository.Find<Category>(c => c.Id.Contains(2,1));

            Assert.NotNull(category);
            Assert.Equal(1, category.Id);

        }

        [Fact]
        public void FindTestWithInClauseFunction2()
        {
            var categories = SQLiteRepository.FindAll<Category>(c => c.Id.Contains(2, 1));

            Assert.Equal(2, categories.Count());

        }

        [Fact]
        public void FindTestWithEnumFunction()
        {
            var person = SQLiteRepository.Find<Person>(c => c.Gender == Gender.Male);

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestWithNullableEnumFunction()
        {
            var person = SQLiteRepository.Find<Person>(c => c.Gender == (Gender?)Gender.Male);

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestWithEnumDonContainsAndNullValueFunction()
        {
            var person = SQLiteRepository.Find<Person>(c => c.Gender.In(Gender.Male, null));

            Assert.NotNull(person);
            Assert.Equal(1, person.Id);

        }

        [Fact]
        public void FindTestWithLikeClauseUsingContainFunction()
        {
            var category = SQLiteRepository.Find<Category>(c => c.Name.Contains("CATE"));

            Assert.NotNull(category);

        }
        [Fact]
        public void FindTestWithDifferentType()
        {
            long id = 2;
            var category = SQLiteRepository.Find<Category>(c => c.Id == id);

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }
        [Fact]
        public void FindTestWithDifferentNullableType()
        {
            long? id = 2;
            var category = SQLiteRepository.Find<Category>(c => c.Id == id);

            Assert.NotNull(category);
            Assert.Equal(2, category.Id);

        }


        [Fact]
        public void FindTestForIsNullQuery()
        {
            var people = SQLiteRepository.FindAll<Person>(c => c.Address == null);

            Assert.True(people.Any());

        }

        [Fact]
        public void FindTestForIsNullAndOtherValueQuery()
        {
            var people = SQLiteRepository.FindAll<Person>(c => c.Address == null && c.Id == 3);

            Assert.True(people.Any());

        }

        [Fact]
        public void FindTestForIsNotNullQuery()
        {
            var people = SQLiteRepository.FindAll<Person>(c => c.Address != null);

            Assert.True(people.Any());

        }

        [Fact]

        public void FindWithTransactionContextTest()
        {
            using (var transaction = SQLiteRepository.SimpleAccess.BeginTransaction())
            {
                var category = SQLiteRepository.Find<Category>(transaction, c => c.Id == 2);

                Assert.NotNull(category);

                var branch = SQLiteRepository.Find<Branch>(transaction, c => c.Id == 2);

                Assert.NotNull(branch);

                var attachment = SQLiteRepository.Find<Attachment>(transaction, c => c.Id == 2);

                Assert.NotNull(attachment);

                SQLiteRepository.SimpleAccess.EndTransaction(transaction);

            }
        }


        [Fact]
        public void FindAllTest()
        {
            var categories = SQLiteRepository.FindAll<Category>(c => c.Description.Contains("cat"));

            Assert.Equal(2, categories.Count());

        }

        [Fact]

        public void FindAllWithTransactionContextTest()
        {
            using (var transaction = SQLiteRepository.SimpleAccess.BeginTransaction())
            {
                var categories = SQLiteRepository.FindAll<Category>(transaction, c => c.Description.Contains("cat"));
                Assert.Equal(2, categories.Count());

                var branches = SQLiteRepository.FindAll<Branch>(transaction, c => c.CityId == 1);
                Assert.Equal(2, branches.Count());


                var attachments = SQLiteRepository.FindAll<Attachment>(transaction, c => c.IncidentId == 3);
                Assert.Single(attachments);

                SQLiteRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void UpdateTest()
        {
            var person = SQLiteRepository.GetAll<Person>().First();

            person.FullName = "Full Name updated";

            var rowAffected = SQLiteRepository.Update<Person>(person);

            Assert.Equal(1, rowAffected);
        }

        [Fact]

        public void UpdateAllWithTransactionContextTest()
        {
            var people = SQLiteRepository.GetAll<Person>();

            using (var transaction = SQLiteRepository.SimpleAccess.BeginTransaction())
            {

                foreach (var person in people)
                {
                    person.FullName = person.FullName + 1;
                }


                var rowAffected = SQLiteRepository.UpdateAll<Person>(transaction, people);

                Assert.Equal(rowAffected, people.Count());


                SQLiteRepository.SimpleAccess.EndTransaction(transaction);

            }
        }

        [Fact]
        public void DeleteTest()
        {
            var person = SQLiteRepository.GetAll<Person>().First();


            var rowAffected = SQLiteRepository.Delete<Person>(person.Id);

            Assert.Equal(1, rowAffected);
        }

        [Fact]
        public void DeleteAllWithTransactionContextTest()
        {
            var people = SQLiteRepository.GetAll<Person>().Select<Person, long>(p => p.Id);

            using (var transaction = SQLiteRepository.SimpleAccess.BeginTransaction())
            {

                var rowAffected = SQLiteRepository.DeleteAll<Person>(transaction, people);

                Assert.Equal(rowAffected, people.Count());

                SQLiteRepository.SimpleAccess.EndTransaction(transaction);

            }
        }



        [Fact]
        public void GetDynamicPagedListTest()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelect()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(p => new { p.Id }, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelectDistinct()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(true, p => new { p.Id }, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelectAndWhereClause()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(p => new { p.Id }, "WHERE ID > 1", 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSelectDistinctAndWhereClause()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(true, p => new { p.Id }
                                , "WHERE ID > @id", 0, 2, "Id", false, new SQLiteParameter( "@id", 1 ));

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(2, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithObjectParameters()
        {
            var id = 1;
            var people = SQLiteRepository.GetDynamicPagedList<Person>(0, 2, "Id, FullName", new { id }, true);

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetDynamicPagedListTestWithSqlParameters()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(0, 2, "Id, FullName", false, new SQLiteParameter("@id", 1));

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTestWithSelectDistinct()
        {
            var people = SQLiteRepository.GetEntitiesPagedList<Person>(true, p => new { p.Id }, null, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTestWithLike()
        {
            var people = SQLiteRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }
            , "Where FullName like '%' + @fullName + '%'", 0, 2, "Id", new { fullName = "ah" }, true);

            Assert.Equal(1, people.Data.Count());
            Assert.Equal(1, people.TotalRows);
        }

        [Fact]
        public void GetEntitiesPagedListTest()
        {
            var people = SQLiteRepository.GetEntitiesPagedList<Person>(p => new { p.Id, p.FullName }, null, 0, 2, "Id", false);

            Assert.Equal(2, people.Data.Count());
            Assert.Equal(3, people.TotalRows);
        }

        [Fact]
        public void IsExistTest()
        {
            var result = SQLiteRepository.IsExist<Person>();

            Assert.True(result);

        }

        [Fact]
        public void IsExistTestTestWithSelector()
        {
            var result = SQLiteRepository.IsExist<Person>(p => p.Id);

            Assert.True(result);

        }

        [Fact]
        public void IsExistTestTestWithWhereTrue()
        {
            var result = SQLiteRepository.IsExist<Person>(p => p.Id == 1);

            Assert.True(result);
        }

        [Fact]
        public void IsExistTestTestWithWhereFalse()
        {
            var result = SQLiteRepository.IsExist<Person>(p => p.Id > 100);

            Assert.False(result);
        }


        [Fact]
        public void GetCountTest()
        {
            var rowCount = SQLiteRepository.GetCount<Person>();

            Assert.Equal(3, rowCount);

        }

        [Fact]
        public void GetCountTestWithWhere()
        {
            var rowCount = SQLiteRepository.GetCount<Person>(p => p.Id == 1);

            Assert.Equal(1, rowCount);

        }

        [Fact]
        public void GetCountTestWithSingleColumn()
        {
            var rowCount = SQLiteRepository.GetCount<Person, string>(select => select.Address);

            Assert.Equal(2, rowCount);

        }

        [Fact]
        public void GetCountTestWithSelect()
        {
            var counts = SQLiteRepository.GetCount<Branch>(p => new { p.Id, p.CityId });

            Assert.Equal(3, counts.CountOfId);
            Assert.Equal(3, counts.CountOfCityId);
        }

        [Fact]
        public void GetCountTestWithSelectAndWhere()
        {
            var counts = SQLiteRepository.GetCount<Branch>(p => new { p.Id, p.CityId }, where => where.Id == 1);

            Assert.Equal(1, counts.CountOfId);
            Assert.Equal(1, counts.CountOfCityId);

        }



        [Fact]
        public void GetSumTest()
        {
            var sumOfSalary = SQLiteRepository.GetSum<Person>(p => p.BasicSalary);

            Assert.Equal(12000, sumOfSalary);

        }

        [Fact]
        public void GetSumTestWithMultiColumn()
        {
            var sum = SQLiteRepository.GetSum<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(12000, sum.SumOfBasicSalary);
            Assert.Equal(1000, sum.SumOfTransport);
        }


        [Fact]
        public void GetSumTestWithSingleNullableColumn()
        {
            var sumOfTransport = SQLiteRepository.GetSum<Person>(p => p.Transport);

            Assert.Equal(1000, sumOfTransport);
        }

        [Fact]
        public void GetSumTestWithSelectAndWhere()
        {
            var sumOfSalary = SQLiteRepository.GetSum<Person>(p => p.BasicSalary, where => where.Id > 1);

            Assert.Equal(8000, sumOfSalary);

        }


        [Fact]
        public void GetMinTest()
        {
            var MinOfSalary = SQLiteRepository.GetMin<Person>(p => p.BasicSalary);

            Assert.Equal(3000, MinOfSalary);

        }

        [Fact]
        public void GetMinTestWithMultiColumn()
        {
            var Min = SQLiteRepository.GetMin<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(3000, Min.MinOfBasicSalary);
            Assert.Equal(300, Min.MinOfTransport);
        }


        [Fact]
        public void GetMinTestWithSingleNullableColumn()
        {
            var MinOfTransport = SQLiteRepository.GetMin<Person>(p => p.Transport);

            Assert.Equal(300, MinOfTransport);
        }

        [Fact]
        public void GetMinTestWithSelectAndWhere()
        {
            var MinOfSalary = SQLiteRepository.GetMin<Person>(p => p.BasicSalary, where => where.Id > 1);

            Assert.Equal(3000, MinOfSalary);

        }


        [Fact]
        public void GetMaxTest()
        {
            var MaxOfSalary = SQLiteRepository.GetMax<Person>(p => p.BasicSalary);

            Assert.Equal(5000, MaxOfSalary);

        }

        [Fact]
        public void GetMaxTestWithMultiColumn()
        {
            var Max = SQLiteRepository.GetMax<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(5000, Max.MaxOfBasicSalary);
            Assert.Equal(700, Max.MaxOfTransport);
        }


        [Fact]
        public void GetMaxTestWithSingleNullableColumn()
        {
            var MaxOfTransport = SQLiteRepository.GetMax<Person>(p => p.Transport);

            Assert.Equal(700, MaxOfTransport);
        }

        [Fact]
        public void GetMaxTestWithSelectAndWhere()
        {
            var MaxOfSalary = SQLiteRepository.GetMax<Person>(p => p.BasicSalary, where => where.Id > 2);

            Assert.Equal(3000, MaxOfSalary);

        }


        [Fact]
        public void GetAvgTest()
        {
            var AvgOfSalary = SQLiteRepository.GetAverage<Person>(p => p.BasicSalary);

            Assert.Equal(4000, AvgOfSalary);

        }

        [Fact]
        public void GetAvgTestWithMultiColumn()
        {
            var Avg = SQLiteRepository.GetAverage<Person>(p => new { p.BasicSalary, p.Transport });

            Assert.Equal(4000, Avg.AvgOfBasicSalary);
            Assert.Equal(500, Avg.AvgOfTransport);
        }


        [Fact]
        public void GetAvgTestWithSingleNullableColumn()
        {
            var AvgOfTransport = SQLiteRepository.GetAverage<Person>(p => p.Transport);

            Assert.Equal(500, AvgOfTransport);
        }

        [Fact]
        public void GetAvgTestWithSelectAndWhere()
        {
            var AvgOfSalary = SQLiteRepository.GetAverage<Person>(p => p.BasicSalary, where => where.Id > 1);

            Assert.Equal(4000, AvgOfSalary);

        }

        [Fact]
        public void InClauseWithArrayVariableTest()
        {
            var ids = new[] { 1, 2 };
            var found = SQLiteRepository.FindAll<Person>(p => p.Id.In(ids));

            Assert.True(found.Any());
        }

        [Fact]
        public void InClauseWithArrayValuesTest()
        {
            var found = SQLiteRepository.FindAll<Person>(p => p.Id.In(new[] { 1, 2 }));

            Assert.True(found.Any());
        }

        [Fact]
        public void WhereClauseWithDirectTrueBoolPropertyTest()
        {
            var data = SQLiteRepository.FindAll<Employee>(e => e.IsOnDuty);

            Assert.Equal(10, data.Count());
        }


        [Fact]
        public void WhereClauseWithDirectFalseBoolPropertyTest()
        {
            var data = SQLiteRepository.FindAll<Employee>(e => !e.IsOnDuty);

            Assert.Equal(10, data.Count());
        }

        [Fact]
        public void InClauseExpressionTest()
        {
            var found = SQLiteRepository.FindAll<Person>(p => p.Id.In<Person, int>(pr => pr.Id));

            Assert.True(found.Any());

        }

        [Fact]
        public void InClauseWithSubWhereExpressionTest()
        {
            var found = SQLiteRepository.FindAll<Person>(p => p.Id.In<Category, int>(pr => pr.Id, pw => pw.Id < 10));
            Assert.True(found.Any());

        }
        [Fact]
        public void TestGetDynamicPage()
        {
            var people = SQLiteRepository.GetDynamicPagedList<Person>(e => new { e.Id, e.FullName }, 0, 1000, "FullName", false);
            Assert.Equal(3, people.Data.Count());
        }

        //[Fact]
        //public void GetAggregateTest()
        //{
        //    var data = SQLiteRepository.GetAggregate<Employee>(
        //        aggregator: (ag, e) => new { SumOfBasicSalary = ag.Sum(e.BasicSalary), MaxOfBasicSalary = ag.Max(e.BasicSalary) }
        //    );
        //    Assert.True(data != null);

        //}
        [Fact]
        public void GetAggregateTestWithWheres ()
        {
            var data = SQLiteRepository.GetAggregate <Employee>(
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
            var data = SQLiteRepository.GetAggregateWithGroupBy<Employee>(
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
            var data = SQLiteRepository.GetAggregateWithGroupBy<Employee>(
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


        [Fact]
        public void CheckCultureValueOfDecimal()
        {
            var currentCulture = CultureInfo.CurrentCulture;

            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
            decimal? value = 3.00M;
            object objValue = (object)value;
            var data = SQLiteRepository.Find<Employee>(e => e.Id == 1 &&  e.Transport > value);

            Assert.NotNull(data);
            CultureInfo.CurrentCulture = currentCulture;
        }


    }
}
