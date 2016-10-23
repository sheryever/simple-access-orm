using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess
{

    /// <summary>
    /// Specifies the stored procedure name of the Entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class StoredProcedureNameKeyWordAttribute : Attribute
    {
        /// <summary>
        /// Stored procedure name key word of the Entity.
        /// </summary>
        public string NameKeyWord { get; private set; }

        /// <summary>
        /// Specifies the stored procedure name key word of the Entity.
        /// </summary>
        /// <param name="nameKeyWord"> Stored procedures name keyword. </param>
        public StoredProcedureNameKeyWordAttribute(string nameKeyWord)
        {
            NameKeyWord = nameKeyWord;
        }
    }
}
