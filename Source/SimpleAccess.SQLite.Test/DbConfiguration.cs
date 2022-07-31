using System;
using System.Collections.Generic;
using System.Text;
using SimpleAccess.SQLite;

namespace SimpleAccess.SQLite.Test
{
    public static class DbConfiguration
    {
        public static string DbInitialScript => @"

            drop table if exists Categories;
            drop table if exists Attachments;
            drop table if exists Branches;
            drop table if exists People;
            drop table if exists Employees;


            CREATE TABLE Categories (
                Id          INT        PRIMARY KEY,
                Name        TEXT (50),
                Description TEXT (500) 
            );

            CREATE TABLE Attachments (
                Id          INT        PRIMARY KEY,
                IncidentId          INT,
                OtherName        TEXT (50)
            );

            CREATE TABLE Branches (
                Id          INT        PRIMARY KEY,
                CityId          INT,
                Name            TEXT (50),
                PhoneNumbers    TEXT (50),
                Address         TEXT (50),
                Address2        TEXT (50)
            );


            CREATE TABLE People (
                Id              INT        PRIMARY KEY,
                FullName            TEXT (200),
                Gender          INT,
                Phone         TEXT (40),
                Address        TEXT (200),
                BasicSalary        DECIMAL (10, 2),
                Transport        INT
            );


            CREATE TABLE Employees (
                Id              INT        PRIMARY KEY,
                FullName            TEXT (200),
                Phone         TEXT (40),
                Address        TEXT (200),
                BasicSalary        DECIMAL (10, 2),
                Transport        INT,
                Inssurance      DECIMAL (10, 2),
                 IsOnDuty    BOOLEAN,
                Department      TEXT (50)
            );

            Insert INTO Categories VALUES(1, 'CATE 1', 'SOME Cate');
            Insert INTO Categories VALUES(2, 'CATE 2', 'SOME Cate');
            Insert INTO Categories VALUES(3, 'CATE 3', 'SOME desc');

            Insert INTO Attachments VALUES(1, 1, 'SOME Attachments');
            Insert INTO Attachments VALUES(2, 2, 'SOME Attachments');
            Insert INTO Attachments VALUES(3, 3, 'SOME Attachments');

            Insert INTO Branches VALUES(1, 1, 'Madina', null,null, null);
            Insert INTO Branches VALUES(2, 1, 'Makkah', null,null, null);
            Insert INTO Branches VALUES(3, 2, 'Karachi', null,null, null);

            Insert INTO People VALUES(1, 'Ahmed', 1, '00000', 'Madina', 4000, 300);
            Insert INTO People VALUES(2, 'Muhammad', 1, '000000', 'Makkah', 5000, null);
            Insert INTO People VALUES(3, 'Karim', 1, '00000', null, 3000, 700);

            Insert INTO Employees VALUES(1, 'Ahmed', '00000', 'Madina', 4000, 300, 1000, 1, 'Sales');
            Insert INTO Employees VALUES(2, 'Muhammad', '000000', 'Makkah', 5000, null, 1000, 1,'Sales');
            Insert INTO Employees VALUES(3, 'Karim', '00000', null, 3000, 700, null , 1,'Sales');
            Insert INTO Employees VALUES(4, 'Kamal', '00000', 'Madina', 3900, 300, 300, 0,'Sales');
            Insert INTO Employees VALUES(5, 'Raheem', '000000', 'Makkah', 6000, null, 100, 0,'Audit');
            Insert INTO Employees VALUES(6, 'Saleem', '00000', null, 9000, 200, null , 1,'Audit');
            Insert INTO Employees VALUES(7, 'Kamal', '00000', 'Madina', 18000, 200, 300, 1,'Purchase');
            Insert INTO Employees VALUES(10, 'Raheem', '000000', 'Makkah', 3000, null, 100, 1,'Purchase');
            Insert INTO Employees VALUES(11, 'Saleem', '00000', null, 7000, 200, 400 , 0,'Purchase');
            Insert INTO Employees VALUES(12, 'Raheem2', '000000', 'Makkah', 6000, null, 100, 1,'Audit');
            Insert INTO Employees VALUES(13, 'Saleem2', '00000', null, 9000, 200, null , 1,'Audit');
            Insert INTO Employees VALUES(14, 'Kamal3', '00000', 'Madina', 18000, 200, 300, 1,'Purchase');
            Insert INTO Employees VALUES(15, 'Raheem2', '000000', 'Makkah', 3000, null, 100, 1,'Purchase');
            Insert INTO Employees VALUES(16, 'Saleem 4', '00000', null, 7000, 200, 400 , 0,'Purchase');
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


            DELETE Categories;
            DELETE Branches;
            DELETE Attachments;
            DELETE People;
";

    }

    public class DbFixtureWithSimpleAccess : IDisposable
    {
        public ISQLiteSimpleAccess SimpleAccess { get; set; }

        public DbFixtureWithSimpleAccess()
        {
            SimpleAccess = new SQLiteSimpleAccess("sqliteDefaultConnection");
            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialData);

        }
        public void Dispose()
        {

            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbClear);

            SimpleAccess.Dispose();
        }
    } 

    public class DbFixtureWithSimpleAccess<TRepository> : IDisposable
        where TRepository : class, ISQLiteSimpleAccess
    {
        public ISQLiteSimpleAccess SimpleAccess { get; set; }
        public ISQLiteRepository SQLiteRepository { get; set; }

        public DbFixtureWithSimpleAccess()
        {
            SimpleAccess = new SQLiteSimpleAccess("sqliteDefaultConnection");

            SQLiteRepository = new SQLiteEntityRepository(SimpleAccess);

            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbInitialData);

        }
        public void Dispose()
        {

            SimpleAccess.ExecuteNonQuery(DbConfiguration.DbClear);

            SimpleAccess.Dispose();
        }
    }

}