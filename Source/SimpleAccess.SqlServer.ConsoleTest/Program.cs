using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;
using SimpleAccess.SqlServer.ConsoleTest.Entities;
using SimpeAccess.SqlServer.ConsoleTest;
using SimpleAccess.Core;

namespace SimpleAccess.SqlServer.ConsoleTest
{
    class Program
    {
        public class Person
        {
            [Identity]
            public int Id { get; set; }

            public string Name { get; set; }
            public string Address { get; set; }
        }

        static void Main5(string[] args)
        {

            Console.WriteLine("Testing Incident Transaction function");

            //ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            IncidentsService service = new IncidentsService();
            try
            {

                var incident = new Incident { Name = "inci" };
                service.InsertIncidentSimple(incident);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }


        static void Main4(string[] args)
        {

            Console.WriteLine("Testing ExecuteValues function");

            ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("sqlDefaultConnection");
            SqlTransaction transaction = null;
            try
            {
                var data = simpleAccess.ExecuteValues<string>("Select Name from Category;");
                using (transaction = simpleAccess.BeginTransaction())
                {
                    data = simpleAccess.ExecuteValues<string>(transaction, "Select Name from Category;");

                    simpleAccess.EndTransaction(transaction);
                }
            }
            catch (Exception e)
            {
                simpleAccess.EndTransaction(transaction, false);
                throw;
            }


            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {



            (new FindExpressionTest()).Test();

            //branches = repo.FindAll<Branche>(b => b.Address2.Contains("Munawwarah") && b.Name == "البيداء");
            (new TestClassFindAndFindAll()).Test("Al Madina", "Munawwarah");


            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        public class FindExpressionTest
        {
            public void Test()
            {
                ISqlRepository repo = new SqlRepository("sqlDefaultConnection");

                var branches = repo.GetAll<Branch>();
                var branch = repo.FindAll<Branch>(b => b.Id == 1);
                branches = repo.FindAll<Branch>(b => b.Address2.EndsWith("Munawwarah") && b.Name == "البيداء");
                branches = repo.FindAll<Branch>(b => b.Address2.StartsWith("Al Madina"));
                branches = repo.FindAll<Branch>(b => b.Address2 == null);
                var bs = new List<Branch>();

            }

        }





        static void Main2(string[] args)
        {

            ConstructorTests();
            ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("defaultConnection");
            SqlTransaction transaction = null;
            try
            {
                using (transaction = simpleAccess.BeginTransaction())
                {
                    var person = new Person() { Name = "Ahmed", Address = "Madina" };
                    
                    var newId = simpleAccess.ExecuteScalar<int>(transaction, "INSERT INTO People Values (@name, @Address); SELECT SCOPE_IDENTITY();", person);
                    
                    simpleAccess.EndTransaction(transaction);
                }
            }
            catch (Exception)
            {
                simpleAccess.EndTransaction(transaction, false);
                throw;
            }


            //TestTextCommandSimpleAccess(GetTextQuerySimpleAccess());
            //TestSotredProcedureCommandSimpleAccess(GetStroedProcedureSimpleAccess());
            TestSotredProcedureCommandSimpleAccessRepository();
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }


        public static void TestTextCommandSimpleAccess(ISqlSimpleAccess sqlSimpleAccess)
        {
            ISqlSimpleAccess simpleAccess = sqlSimpleAccess;

            Console.WriteLine("Test TextCommand SimpleAccess");

            WriteLine("\nTesting simpleAccess.ExecuteEntities<Category>(query, fieldsToSkip: null, propertyInfoDictionary:null, parameters:null);");
            var categories = simpleAccess.ExecuteEntities<Category>("Select Id, Name, Description FROM Category", fieldsToSkip: null, propertyInfoDictionary:null, parameters:null);

            
            WriteAll(categories);

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Category>(wherequery, fieldsToSkip: null, propertyInfoDictionary:null, parameters: 1.ToDataParam(\"Id\"));");
            var category = simpleAccess.ExecuteEntity<Category>("Select Id, Name, Description FROM Category WHERE Id = @Id", parameters: 1.ToDataParam("Id"));
            WriteLine(category.ToString());

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Category>(wherequery, fieldsToSkip: \"Description\", propertyInfoDictionary:null, parameters: 1.ToDataParam(\"Id\"));");
            category = simpleAccess.ExecuteEntity<Category>("Select Id, Name, Description FROM Category WHERE Id = @Id", "Description", parameters: 1.ToDataParam("Id"));
            WriteLine(category.ToString());

            WriteLine("\nTesting simpleAccess.ExecuteNonQuery(insertQuery, fieldsToSkip: null, propertyInfoDictionary:null, parameters:dynamicParam);");
            var dynamicParam = new { Name = "Shampu", CategoryId = 1, IsActive = true, ProductType = (short)ProductType.Liquid
                                    , PricePerUnit = 10.50f, PricePerPackage = 100.50d, UnitPerPackage = (short)20
                                    , AvailableTill = DateTime.Now, LastPurchase = DateTime.Now};
            var insertQuery = @"INSERT INTO [dbo].[Product] ([Name],[CategoryId],[IsActive],[ProductType],[PricePerUnit],[PricePerPackage],[UnitPerPackage],[AvailableTill],[LastPurchase]) 
                                    VALUES(@Name, @CategoryId, @IsActive, @ProductType, @PricePerUnit, @PricePerPackage, @UnitPerPackage, @AvailableTill, @LastPurchase); ";
            var recordAffected = simpleAccess.ExecuteNonQuery(insertQuery, dynamicParam);

            WriteLine("Record affacted:{0}", recordAffected);

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Product>(query, fieldsToSkip: null, propertyInfoDictionary:null, parameters:null);");
            var products = simpleAccess.ExecuteEntities<Product>("SELECT [Id],[Name],[CategoryId],[IsActive],[ProductType],[PricePerUnit],[PricePerPackage],[UnitPerPackage],[AvailableTill],[LastPurchase] FROM [dbo].[Product]", "Description", parameters: null);
            WriteAll(products);

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Product>(query, fieldsToSkip: null, propertyInfoDictionary:null, parameters:nul);");
            var productCount = simpleAccess.ExecuteScalar<int>("SELECT COUNT([Id]) FROM Product;");
            WriteLine("Total product count:{0}", productCount);

            Console.WriteLine("End Of TextCommand with SimpleAccess");
        }

        public static void TestSotredProcedureCommandSimpleAccess(ISqlSimpleAccess sqlSimpleAccess)
        {
            ISqlSimpleAccess simpleAccess = sqlSimpleAccess;

            Console.WriteLine("Test StoredProcedure Command with SimpleAccess");

            WriteLine("\nTesting simpleAccess.ExecuteEntities<Category>(\"Category_GetAll\", fieldsToSkip: null, propertyInfoDictionary:null, parameters:null);");
            var categories = simpleAccess.ExecuteEntities<Category>("dbo.Category_GetAll", fieldsToSkip: null, propertyInfoDictionary: null, parameters: null);


            WriteAll(categories);

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Category>(\"dbo.Category_GetById\", fieldsToSkip: null, propertyInfoDictionary:null, parameters: 1.ToDataParam(\"Id\"));");
            var category = simpleAccess.ExecuteEntity<Category>("dbo.Category_GetById", parameters: 1.ToDataParam("Id"));
            WriteLine(category.ToString());

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Category>(\"dbo.Category_GetById\", fieldsToSkip: \"Description\", propertyInfoDictionary:null, parameters: 1.ToDataParam(\"Id\"));");
            category = simpleAccess.ExecuteEntity<Category>("dbo.Category_GetById", "Description", parameters: 1.ToDataParam("Id"));
            WriteLine(category.ToString());

            WriteLine("\nTesting simpleAccess.ExecuteNonQuery(\"dbo.Product_Insert\", fieldsToSkip: null, propertyInfoDictionary:null, parameters:dynamicParam);");
            var dynamicParam = new
            {
                Name = "Shampu",
                CategoryId = 1,
                IsActive = true,
                ProductType = (short)ProductType.Liquid
                                    ,
                PricePerUnit = 10.50f,
                PricePerPackage = 100.50d,
                UnitPerPackage = (short)20
                                    ,
                AvailableTill = DateTime.Now,
                LastPurchase = DateTime.Now
            };
            var recordAffected = simpleAccess.ExecuteNonQuery("dbo.Product_Insert", dynamicParam);

            WriteLine("Record affacted:{0}", recordAffected);

            WriteLine("\nTesting simpleAccess.ExecuteEntity<Product>(\"dbo.Product_GetAll\", fieldsToSkip: null, propertyInfoDictionary:null, parameters:nul);");
            var products = simpleAccess.ExecuteEntities<Product>("dbo.Product_GetAll", "Description", parameters: null);
            WriteAll(products);

            WriteLine("\nTesting simpleAccess.ExecuteEntity<int>(\"dbo.Product_GetCount\", fieldsToSkip: null, propertyInfoDictionary:null, parameters:nul);");
            var productCount = simpleAccess.ExecuteScalar<int>("dbo.Product_GetCount");
            WriteLine("Total product count:{0}", productCount);

            Console.WriteLine("End Of StoredProcedure Command with SimpleAccess");
        }

        public static void TestSotredProcedureCommandSimpleAccessRepository()
        {
            ISqlRepository sqlRepo = new SqlRepository();
            Console.WriteLine("Test StoredProcedure Command with SimpleAccess SQL Repository");

            
            WriteLine("\nTesting simpleAccess.ExecuteNonQuery(\"dbo.Product_Insert\", fieldsToSkip: null, propertyInfoDictionary:null, parameters:dynamicParam);");
            var product = new Product
            {
                Name = "Shampu",
                CategoryId = 1,
                IsActive = true,
                ProductType =  ProductType.Liquid
                                    ,
                PricePerUnit = 10.50f,
                PricePerPackage = (decimal) 100.50,
                UnitPerPackage = (short)20
                                    ,
                AvailableTill = DateTime.Now,
                LastPurchase = DateTime.Now
            };
            var recordAffected = sqlRepo.Insert<Product>(product);

            product.Name = "Updated Records " + product.Name;

            recordAffected = sqlRepo.Update<Product>(product);

            recordAffected = sqlRepo.Insert<Product>(product);

            WriteLine("Record affacted:{0}", recordAffected);

            Console.WriteLine("Test StoredProcedure Command with SimpleAccess SQL Repository");
        }


        public static void WriteLine(string format, params object[] args)
        {
            if (args != null)
                Console.WriteLine(format, args);
            else
            {
                Console.WriteLine(format);
            }
        }

        public static void WriteAll<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
        }

        public static void ConstructorTests()
        {
            ISqlSimpleAccess simpleAccess = new SqlSimpleAccess("sqlDefaultConnection");

            simpleAccess = new SqlSimpleAccess("Data Source=.\\SQLEXPRESS2014;Initial Catalog=SimpleAccessTest;Persist Security Info=True;User ID=sa;Password=test");

        }

        public static SqlSimpleAccess GetTextQuerySimpleAccess()
        {
            return new SqlSimpleAccess(CommandType.Text);
        }

        public static SqlSimpleAccess GetStroedProcedureSimpleAccess()
        {
            return new SqlSimpleAccess(CommandType.StoredProcedure);
        }
    }

    public class Product
    {
        [Identity]
        public long Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public ProductType ProductType { get; set; }
        public double PricePerUnit { get; set; }
        public decimal PricePerPackage { get; set; }
        public short? UnitPerPackage { get; set; }
        public DateTime AvailableTill { get; set; }
        public DateTime LastPurchase { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var propertyInfo in GetType().GetProperties())
            {
                sb.AppendFormat("{0}: {1}\n", propertyInfo.Name, propertyInfo.GetValue(this));
            }
            return sb.ToString();
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, Description: {2}", Id, Name, Description);
        }
    }

    public enum ProductType
    {
        Solid = 1,
        Liquid
    }
}
