# ISimpleAccess(*TDbConnection*, *TDbTransaction*, *TDbCommand*, *TDataParameter*, *TDbDataReader*, *TParameterBuilder*) Interface
 

Represent the interface of SimpleAccess methods and it's implemented by SimpleAccess

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
public interface ISimpleAccess<TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder>
where TDbConnection : new(), IDbConnection
where TDbTransaction : IDbTransaction
where TDbCommand : new(), IDbCommand
where TDataParameter : new(), IDataParameter
where TDbDataReader : IDataReader
where TParameterBuilder : new(), IParameterBuilder

```

**VB**<br />
``` VB
Public Interface ISimpleAccess(Of TDbConnection As {New, IDbConnection}, TDbTransaction As IDbTransaction, TDbCommand As {New, IDbCommand}, TDataParameter As {New, IDataParameter}, TDbDataReader As IDataReader, TParameterBuilder As {New, IParameterBuilder})
```

**C++**<br />
``` C++
generic<typename TDbConnection, typename TDbTransaction, typename TDbCommand, typename TDataParameter, typename TDbDataReader, typename TParameterBuilder>
where TDbConnection : gcnew(), IDbConnection
where TDbTransaction : IDbTransaction
where TDbCommand : gcnew(), IDbCommand
where TDataParameter : gcnew(), IDataParameter
where TDbDataReader : IDataReader
where TParameterBuilder : gcnew(), IParameterBuilder
public interface class ISimpleAccess
```

**F#**<br />
``` F#
type ISimpleAccess<'TDbConnection, 'TDbTransaction, 'TDbCommand, 'TDataParameter, 'TDbDataReader, 'TParameterBuilder when 'TDbConnection : new() and IDbConnection when 'TDbTransaction : IDbTransaction when 'TDbCommand : new() and IDbCommand when 'TDataParameter : new() and IDataParameter when 'TDbDataReader : IDataReader when 'TParameterBuilder : new() and IParameterBuilder> =  interface end
```


#### Type Parameters
&nbsp;<dl><dt>TDbConnection</dt><dd>\[Missing <typeparam name="TDbConnection"/> documentation for "T:SimpleAccess.Core.ISimpleAccess`6"\]</dd><dt>TDbTransaction</dt><dd>\[Missing <typeparam name="TDbTransaction"/> documentation for "T:SimpleAccess.Core.ISimpleAccess`6"\]</dd><dt>TDbCommand</dt><dd>\[Missing <typeparam name="TDbCommand"/> documentation for "T:SimpleAccess.Core.ISimpleAccess`6"\]</dd><dt>TDataParameter</dt><dd>\[Missing <typeparam name="TDataParameter"/> documentation for "T:SimpleAccess.Core.ISimpleAccess`6"\]</dd><dt>TDbDataReader</dt><dd>\[Missing <typeparam name="TDbDataReader"/> documentation for "T:SimpleAccess.Core.ISimpleAccess`6"\]</dd><dt>TParameterBuilder</dt><dd>\[Missing <typeparam name="TParameterBuilder"/> documentation for "T:SimpleAccess.Core.ISimpleAccess`6"\]</dd></dl>&nbsp;
The ISimpleAccess(TDbConnection, TDbTransaction, TDbCommand, TDataParameter, TDbDataReader, TParameterBuilder) type exposes the following members.


## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="P_SimpleAccess_Core_ISimpleAccess_6_DefaultSimpleAccessSettings">DefaultSimpleAccessSettings</a></td><td>
Represent the default settings SimpleAccess <a href="T_SimpleAccess_Core_SimpleAccessSettings">SimpleAccessSettings</a></td></tr></table>&nbsp;
<a href="#isimpleaccess(*tdbconnection*,-*tdbtransaction*,-*tdbcommand*,-*tdataparameter*,-*tdbdatareader*,-*tparameterbuilder*)-interface">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_BeginTrasaction">BeginTrasaction</a></td><td>
Begins a database transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_CloseCurrentDbConnection">CloseCurrentDbConnection</a></td><td>
Close the current open connection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_EndTransaction">EndTransaction</a></td><td>
Close an open database transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_2">ExecuteDynamic(String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_3">ExecuteDynamic(String, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic">ExecuteDynamic(String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_1">ExecuteDynamic(String, CommandType, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_6">ExecuteDynamic(TDbTransaction, String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_7">ExecuteDynamic(TDbTransaction, String, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_4">ExecuteDynamic(TDbTransaction, String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamic_5">ExecuteDynamic(TDbTransaction, String, CommandType, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a dynamic object from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_2">ExecuteDynamics(String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_3">ExecuteDynamics(String, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics">ExecuteDynamics(String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_1">ExecuteDynamics(String, CommandType, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_6">ExecuteDynamics(TDbTransaction, String, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_7">ExecuteDynamics(TDbTransaction, String, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_4">ExecuteDynamics(TDbTransaction, String, CommandType, String, Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteDynamics_5">ExecuteDynamics(TDbTransaction, String, CommandType, String, TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable{dynamic} from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_2">ExecuteEntities(TEntity)(String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_3">ExecuteEntities(TEntity)(String, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1">ExecuteEntities(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_1">ExecuteEntities(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_6">ExecuteEntities(TEntity)(TDbTransaction, String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_7">ExecuteEntities(TEntity)(TDbTransaction, String, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_4">ExecuteEntities(TEntity)(TDbTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a IEnumerable(T) from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntities__1_5">ExecuteEntities(TEntity)(TDbTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_2">ExecuteEntity(TEntity)(String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_3">ExecuteEntity(TEntity)(String, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1">ExecuteEntity(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_1">ExecuteEntity(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_6">ExecuteEntity(TEntity)(TDbTransaction, String, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_7">ExecuteEntity(TEntity)(TDbTransaction, String, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_4">ExecuteEntity(TEntity)(TDbTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteEntity__1_5">ExecuteEntity(TEntity)(TDbTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), TDataParameter[])</a></td><td>
Sends the CommandText to the Connection and builds a TEntity from DataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_2">ExecuteNonQuery(String, Object)</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_3">ExecuteNonQuery(String, TDataParameter[])</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery">ExecuteNonQuery(String, CommandType, Object)</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_1">ExecuteNonQuery(String, CommandType, TDataParameter[])</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_6">ExecuteNonQuery(TDbTransaction, String, Object)</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_7">ExecuteNonQuery(TDbTransaction, String, TDataParameter[])</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_4">ExecuteNonQuery(TDbTransaction, String, CommandType, Object)</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteNonQuery_5">ExecuteNonQuery(TDbTransaction, String, CommandType, TDataParameter[])</a></td><td>
Executes a command text against the connection and returns the number of rows affected.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteReader_3">ExecuteReader(String, TDataParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteReader">ExecuteReader(String, CommandBehavior, TDataParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteReader_2">ExecuteReader(String, CommandType, TDataParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteReader_1">ExecuteReader(String, CommandType, CommandBehavior, TDataParameter[])</a></td><td>
Executes the commandText and return TDbDataReader.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_2">ExecuteScalar(T)(String, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_3">ExecuteScalar(T)(String, TDataParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1">ExecuteScalar(T)(String, CommandType, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_1">ExecuteScalar(T)(String, CommandType, TDataParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_6">ExecuteScalar(T)(TDbTransaction, String, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_7">ExecuteScalar(T)(TDbTransaction, String, TDataParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_4">ExecuteScalar(T)(TDbTransaction, String, CommandType, Object)</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_ExecuteScalar__1_5">ExecuteScalar(T)(TDbTransaction, String, CommandType, TDataParameter[])</a></td><td>
Executes the command text, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_Fill">Fill(String, DataSet)</a></td><td>
Execute commant text against connection and add or refresh rows in DataSet</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_Fill_1">Fill(String, DataTable)</a></td><td>
Execute commant text against connection and add or refresh rows in DataTable</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Core_ISimpleAccess_6_GetNewConnection">GetNewConnection</a></td><td>
Gets the new connection.</td></tr></table>&nbsp;
<a href="#isimpleaccess(*tdbconnection*,-*tdbtransaction*,-*tdbcommand*,-*tdataparameter*,-*tdbdatareader*,-*tparameterbuilder*)-interface">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />