using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace SimpleAccess.Oracle.DbExtensions
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
        public static OracleConnection OpenSafely(this OracleConnection con)
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            return con;
        }

        /**--------------------------------------------------------------------------------------------------
        <summary> A SqlConnection extension method that closes a safely. </summary>
		
        <param name="con"> The con to act on. </param>
		
        <returns> . </returns>
        **/
        public static OracleConnection CloseSafely(this OracleConnection con)
        {
            if (con.State == ConnectionState.Open)
                con.Close();

            return con;
        }
    }
}