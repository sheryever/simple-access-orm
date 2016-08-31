using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Repository;

namespace SimpleAccess.SqlServer.ConsoleTest.Entities
{
    public class TestClassFindAndFindAll
    {
        public void Test(string startsWith, string endsWith)
        {
            ISqlRepository repo = new SqlRepository("sqlDefaultConnection2");

            var branches = repo.GetAll<Branche>();
            var branch = repo.FindSingle<Branche>(b => b.Id == 1);
            branches = repo.Find<Branche>(b => b.Address2.EndsWith(endsWith) && b.Name == "البيداء");
            branches = repo.Find<Branche>(b => b.Address2.StartsWith(startsWith));
            branches = repo.Find<Branche>(b => b.Address2 == null);

        }
    }
}
