#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Collections.Generic;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Data;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Data.Common;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Data.SqlClient;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Linq;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Reflection;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)

namespace SimpleAccess.Core
{

    /**--------------------------------------------------------------------------------------------------
    <summary> Connection extension. </summary>
    **/
    public static class ConnectionExtension
    {
        /**--------------------------------------------------------------------------------------------------
        <summary> A SqlConnection extension method that opens a safely. </summary>
		
        <param name="con"> The con to act on. </param>
		
        <returns> . </returns>
        **/
        public static void OpenSafely(this DbConnection con)
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            //return con;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> A SqlConnection extension method that closes a safely. </summary>
		
        <param name="con"> The con to act on. </param>
		
        <returns> . </returns>
        **/
        public static void CloseSafely(this DbConnection con)
        {
            if (con.State == ConnectionState.Open)
                con.Close();

            //return con;
        }
    }
}