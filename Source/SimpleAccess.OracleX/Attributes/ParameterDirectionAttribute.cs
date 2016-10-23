using System;
using System.Data;

namespace SimpleAccess.Oracle
{

    /// <summary>
    /// Specifies that the ParameterDirection for a SqlParameter Property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterDirectionAttribute : Attribute
    {
        public ParameterDirection SpParameterDirection { get; private set; }

        /// <summary>
        /// Specifies that the ParameterDirection for a SqlParameter Property.
        /// </summary>
        /// <param name="spParameterDirection"> Direction for the SqlParameter.</param>
        public ParameterDirectionAttribute(ParameterDirection spParameterDirection)
        {
            this.SpParameterDirection = spParameterDirection;
        }
    }
}
