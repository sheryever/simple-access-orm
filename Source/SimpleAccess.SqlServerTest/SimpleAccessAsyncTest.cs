using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleAccess.Core;
using SimpleAccess.SqlServer;

namespace SimpleAccess.SqlServerTest
{
    [TestClass]
    public class SimpleAccessAsyncTest
    {
        [TestMethod]
        public void ExecuteScalarAsyncTest()
        {
            ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            var categoriesCount = simpleAccess.ExecuteScalarAsync<int>("Select Count(*) FROM Category").Result;

            Assert.AreEqual(categoriesCount, 3);
        }
    }
}
