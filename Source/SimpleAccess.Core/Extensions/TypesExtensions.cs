using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleAccess.Core.Extensions
{
    public static class TypesExtensions
    {
        /// <summary>
        /// Compare the value with multiple value.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="source">Any type</param>
        /// <param name="list">Multiple values or array of the same type</param>
        /// <returns>Returns a bool value indicates criteria matched or not</returns>
        /// <example>
        /// Example shows how to use In function
        /// <code>
        /// <![CDATA[
        ///     var names = new [] { "Salman", "Jameel", "Kareem", "Kashif" };
        ///     if ("Kareem".In(names))
        ///     {
        ///         Console.Write("Kareem is in names");
        ///     }
        /// ]]>
        /// </code>
        /// </example>
        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ((IList) list).Contains(source);
        }
    }
}
