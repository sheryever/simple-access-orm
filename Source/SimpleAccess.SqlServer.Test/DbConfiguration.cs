using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAccess.SqlServer.Test
{
    public static class DbConfiguration
    {
        public static string DbInitialScript => @"

            ALTER SEQUENCE [dbo].[Seq_Categories] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_Branches] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_Attachments] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_People] RESTART WITH 6 ;  
            ALTER SEQUENCE [dbo].[Seq_Employees] RESTART WITH 17 ;  
            DELETE Categories;
            DELETE Branches;
            DELETE Attachments;
            DELETE People;
            DELETE Employees;

            Insert INTO Categories VALUES(1, 'CATE 1', 'SOME Cate');
            Insert INTO Categories VALUES(2, 'CATE 2', 'SOME Cate');
            Insert INTO Categories VALUES(3, 'CATE 3', 'SOME desc');

            Insert INTO Attachments VALUES(1, 1, 'SOME Attachments');
            Insert INTO Attachments VALUES(2, 2, 'SOME Attachments');
            Insert INTO Attachments VALUES(3, 3, 'SOME Attachments');

            Insert INTO Branches VALUES(1, 1, 'Madina', null,null, null);
            Insert INTO Branches VALUES(2, 1, 'Makkah', null,null, null);
            Insert INTO Branches VALUES(3, 2, 'Karachi', null,null, null);

            Insert INTO People VALUES(1, 'Ahmed', '00000', 'Madina', 4000, 300);
            Insert INTO People VALUES(2, 'Muhammad', '000000', 'Makkah', 5000, null);
            Insert INTO People VALUES(3, 'Karim', '00000', null, 3000, 700);

            Insert INTO Employees VALUES(1, 'Ahmed', '00000', 'Madina', 4000, 300, 1000, 'Sales');
            Insert INTO Employees VALUES(2, 'Muhammad', '000000', 'Makkah', 5000, null, 1000, 'Sales');
            Insert INTO Employees VALUES(3, 'Karim', '00000', null, 3000, 700, null , 'Sales');
            Insert INTO Employees VALUES(4, 'Kamal', '00000', 'Madina', 3900, 300, 300, 'Sales');
            Insert INTO Employees VALUES(5, 'Raheem', '000000', 'Makkah', 6000, null, 100, 'Audit');
            Insert INTO Employees VALUES(6, 'Saleem', '00000', null, 9000, 200, null , 'Audit');
            Insert INTO Employees VALUES(7, 'Kamal', '00000', 'Madina', 18000, 200, 300, 'Purchase');
            Insert INTO Employees VALUES(10, 'Raheem', '000000', 'Makkah', 3000, null, 100, 'Purchase');
            Insert INTO Employees VALUES(11, 'Saleem', '00000', null, 7000, 200, 400 , 'Purchase');
            Insert INTO Employees VALUES(12, 'Raheem2', '000000', 'Makkah', 6000, null, 100, 'Audit');
            Insert INTO Employees VALUES(13, 'Saleem2', '00000', null, 9000, 200, null , 'Audit');
            Insert INTO Employees VALUES(14, 'Kamal3', '00000', 'Madina', 18000, 200, 300, 'Purchase');
            Insert INTO Employees VALUES(15, 'Raheem2', '000000', 'Makkah', 3000, null, 100, 'Purchase');
            Insert INTO Employees VALUES(16, 'Saleem 4', '00000', null, 7000, 200, 400 , 'Purchase');
";

        public static string DbInitialData => @"

            Insert INTO Categories VALUES(1, 'CATE 1', 'SOME Cate');
            Insert INTO Categories VALUES(2, 'CATE 2', 'SOME Cate');
            Insert INTO Categories VALUES(3, 'CATE 3', 'SOME desc');

            Insert INTO Attachments VALUES(1, 1, 'SOME Attachments');
            Insert INTO Attachments VALUES(2, 2, 'SOME Attachments');
            Insert INTO Attachments VALUES(3, 3, 'SOME Attachments');

            Insert INTO Branches VALUES(1, 1, 'Madina', null,null, null);
            Insert INTO Branches VALUES(2, 1, 'Makkah', null,null, null);
            Insert INTO Branches VALUES(3, 2, 'Karachi', null,null, null);

            Insert INTO People VALUES(1, 'Ahmed', '00000');
            Insert INTO People VALUES(2, 'Muhammad', '000000');
            Insert INTO People VALUES(3, 'Shehriyar', '00000');

";

        public static string DbClear => @"

            ALTER SEQUENCE [dbo].[Seq_Categories] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_Branches] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_Attachments] RESTART WITH 6 ;  
		    ALTER SEQUENCE [dbo].[Seq_People] RESTART WITH 6 ;  
            DELETE Categories;
            DELETE Branches;
            DELETE Attachments;
            DELETE People;
";

    }

    public class DbFixtureWithSimpleAccess : IDisposable
    {
        public ISqlSimpleAccess SimpleAccess { get; set; }

        public DbFixtureWithSimpleAccess()
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialData);

        }
        public void Dispose()
        {

            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbClear);

            SimpleAccess.Dispose();
        }
    }

    public class DbFixtureWithSimpleAccess<TRepository> : IDisposable
        where TRepository : class, ISqlSimpleAccess
    {
        public ISqlSimpleAccess SimpleAccess { get; set; }
        public ISqlRepository SqlRepository { get; set; }

        public DbFixtureWithSimpleAccess()
        {
            SimpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            if (typeof(TRepository).Name == "SqlEntityRepository")
            {
                SqlRepository = new SqlEntityRepository(SimpleAccess);
            }
            else
            {
                SqlRepository = new SqlSpRepository(SimpleAccess);

            }
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialData);

        }
        public void Dispose()
        {

            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbClear);

            SimpleAccess.Dispose();
        }
    }

}