using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess.SqlServer.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            TestSotredProcedureCommandSimpleAccess(new SqlSimpleAccess("sqlDefaultConnection"));

            Console.ReadLine();
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

        public static void TestSotredProcedureCommandSimpleAccess(ISqlSimpleAccess sqlSimpleAccess)
        {
            ISqlSimpleAccess simpleAccess = sqlSimpleAccess;
            simpleAccess.DefaultSimpleAccessSettings.DefaultCommandType = CommandType.StoredProcedure;

            Console.WriteLine("Test StoredProcedure Command with SimpleAccess");

            WriteLine("\nTesting simpleAccess.ExecuteEntities<Category>(\"Category_GetAll\", fieldsToSkip: null, propertyInfoDictionary:null, parameters:null);");
            var categories = simpleAccess.ExecuteEntities<Category>("dbo.Category_GetAll");

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
        public static void WriteAll<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity);
            }
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
            foreach (var propertyInfo in GetType().GetTypeInfo().GetProperties())
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
