using SimpleAccess.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccessNetCore.SqlServer.ConsoleTest.Entities
{
    public class TestClassFindAndFindAll
    {
        public void Test(string startsWith, string endsWith)
        {
            ISqlRepository repo = new SqlRepository("sqlDefaultConnection");

            var branches = repo.GetAll<Branch>();
            var branch = repo.FindAll<Branch>();
             branch = repo.FindAll<Branch>(b => b.Id == 1);
            branches = repo.FindAll<Branch>(b => b.Address2.EndsWith(endsWith) && b.Name == "البيداء");
            branches = repo.FindAll<Branch>(b => b.Address2.StartsWith(startsWith));
            branches = repo.FindAll<Branch>(b => b.Address2 == null);

        }
    }
}
