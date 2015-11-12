using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

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
        public static void OpenSafely(this SqlConnection con)
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
        public static void CloseSafely(this SqlConnection con)
        {
            if (con.State == ConnectionState.Open)
                con.Close();

            //return con;
        }
    }
}