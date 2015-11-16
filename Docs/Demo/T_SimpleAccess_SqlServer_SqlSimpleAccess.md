# SqlSimpleAccess Class
 

Sql Server implementaion for SimpleAccess.


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.SqlServer.SqlSimpleAccess<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public class SqlSimpleAccess : ISqlSimpleAccess, 
	ISimpleAccess<SqlConnection, SqlTransaction, SqlCommand, SqlParameter, SqlDataReader, SqlParameterBuilder>, 
	IDisposable
```

**VB**<br />
``` VB
Public Class SqlSimpleAccess
	Implements ISqlSimpleAccess, ISimpleAccess(Of SqlConnection, SqlTransaction, SqlCommand, SqlParameter, SqlDataReader, SqlParameterBuilder), 
	IDisposable
```

**C++**<br />
``` C++
public ref class SqlSimpleAccess : ISqlSimpleAccess, 
	ISimpleAccess<SqlConnection^, SqlTransaction^, SqlCommand^, SqlParameter^, SqlDataReader^, SqlParameterBuilder^>, 
	IDisposable
```

**F#**<br />
``` F#
type SqlSimpleAccess =  
    class
        interface ISqlSimpleAccess
        interface ISimpleAccess<SqlConnection, SqlTransaction, SqlCommand, SqlParameter, SqlDataReader, SqlParameterBuilder>
        interface IDisposable
    end
```

The SqlSimpleAccess type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor">SqlSimpleAccess()</a></td><td>
Default constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_2">SqlSimpleAccess(CommandType)</a></td><td>
Default constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_3">SqlSimpleAccess(SqlConnection)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_6">SqlSimpleAccess(String)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_1">SqlSimpleAccess(SimpleAccessSettings)</a></td><td>
Default constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_4">SqlSimpleAccess(SqlConnection, SimpleAccessSettings)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_5">SqlSimpleAccess(SqlConnection, CommandType)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_7">SqlSimpleAccess(String, SimpleAccessSettings)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess__ctor_8">SqlSimpleAccess(String, CommandType)</a></td><td>
Constructor.</td></tr></table>&nbsp;
<a href="#sqlsimpleaccess-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")![Static member](media/static.gif "Static member")</td><td><a href="P_SimpleAccess_SqlServer_SqlSimpleAccess_DefaultConnectionString">DefaultConnectionString</a></td><td>
Default connection string.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_SqlServer_SqlSimpleAccess_DefaultSimpleAccessSettings">DefaultSimpleAccessSettings</a></td><td>
Default settings for simple access</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_SqlServer_SqlSimpleAccess_SimpleLogger">SimpleLogger</a></td><td>
SimpleLogger to log exception</td></tr></table>&nbsp;
<a href="#sqlsimpleaccess-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_BeginTrasaction">BeginTrasaction</a></td><td>
Begins a transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_BuildSqlParameters">BuildSqlParameters</a></td><td>
Build SqlParameter Array from dynamic object.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_CloseCurrentDbConnection">CloseCurrentDbConnection</a></td><td>
Close the current open connection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_CreateCommand_1">CreateCommand(String, CommandType, SqlParameter[])</a></td><td>
Creates a command.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_CreateCommand">CreateCommand(SqlTransaction, String, CommandType, SqlParameter[])</a></td><td>
Creates a command.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_Dispose">Dispose</a></td><td>
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_EndTransaction">EndTransaction</a></td><td>
Ends a transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Equals</td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_6">ExecuteDynamic(String, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_7">ExecuteDynamic(String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_2">ExecuteDynamic(SqlTransaction, String, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_3">ExecuteDynamic(SqlTransaction, String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_4">ExecuteDynamic(String, CommandType, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_5">ExecuteDynamic(String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic">ExecuteDynamic(SqlTransaction, String, CommandType, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamic_1">ExecuteDynamic(SqlTransaction, String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_6">ExecuteDynamics(String, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_7">ExecuteDynamics(String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_2">ExecuteDynamics(SqlTransaction, String, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_3">ExecuteDynamics(SqlTransaction, String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_4">ExecuteDynamics(String, CommandType, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_5">ExecuteDynamics(String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics">ExecuteDynamics(SqlTransaction, String, CommandType, String, SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteDynamics_1">ExecuteDynamics(SqlTransaction, String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_6">ExecuteEntities(TEntity)(String, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_7">ExecuteEntities(TEntity)(String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_2">ExecuteEntities(TEntity)(SqlTransaction, String, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_3">ExecuteEntities(TEntity)(SqlTransaction, String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_4">ExecuteEntities(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_5">ExecuteEntities(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1">ExecuteEntities(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntities__1_1">ExecuteEntities(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_6">ExecuteEntity(TEntity)(String, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_7">ExecuteEntity(TEntity)(String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_2">ExecuteEntity(TEntity)(SqlTransaction, String, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_3">ExecuteEntity(TEntity)(SqlTransaction, String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_4">ExecuteEntity(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_5">ExecuteEntity(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1">ExecuteEntity(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteEntity__1_1">ExecuteEntity(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_6">ExecuteNonQuery(String, SqlParameter[])</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_7">ExecuteNonQuery(String, Object)</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_2">ExecuteNonQuery(SqlTransaction, String, SqlParameter[])</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_3">ExecuteNonQuery(SqlTransaction, String, Object)</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_4">ExecuteNonQuery(String, CommandType, SqlParameter[])</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_5">ExecuteNonQuery(String, CommandType, Object)</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery">ExecuteNonQuery(SqlTransaction, String, CommandType, SqlParameter[])</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteNonQuery_1">ExecuteNonQuery(SqlTransaction, String, CommandType, Object)</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteReader_3">ExecuteReader(String, SqlParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteReader">ExecuteReader(String, CommandBehavior, SqlParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteReader_2">ExecuteReader(String, CommandType, SqlParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteReader_1">ExecuteReader(String, CommandType, CommandBehavior, SqlParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_6">ExecuteScalar(T)(String, SqlParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_7">ExecuteScalar(T)(String, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_2">ExecuteScalar(T)(SqlTransaction, String, SqlParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_3">ExecuteScalar(T)(SqlTransaction, String, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_4">ExecuteScalar(T)(String, CommandType, SqlParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_5">ExecuteScalar(T)(String, CommandType, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1">ExecuteScalar(T)(SqlTransaction, String, CommandType, SqlParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_ExecuteScalar__1_1">ExecuteScalar(T)(SqlTransaction, String, CommandType, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_Fill">Fill(String, DataSet)</a></td><td>
Execute commant text against connection and add or refresh rows in DataSet</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_Fill_1">Fill(String, DataTable)</a></td><td>
Execute commant text against connection and add or refresh rows in DataTable</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>Finalize</td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_GetDynamicSqlData">GetDynamicSqlData</a></td><td>
Gets a dynamic SQL data.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetHashCode</td><td>
Serves as the default hash function.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_GetNewConnection">GetNewConnection</a></td><td>
Gets the new connection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetType</td><td>
Gets the Type of the current instance.
 (Inherited from Object.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>MemberwiseClone</td><td>
Creates a shallow copy of the current Object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_SqlServer_SqlSimpleAccess_SqlDataReaderToExpando">SqlDataReaderToExpando</a></td><td>
SQL data reader to expando.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>ToString</td><td>
Returns a string that represents the current object.
 (Inherited from Object.)</td></tr></table>&nbsp;
<a href="#sqlsimpleaccess-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_SqlServer">SimpleAccess.SqlServer Namespace</a><br />