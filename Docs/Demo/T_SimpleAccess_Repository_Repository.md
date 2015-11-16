# Repository Class
 

Repository.


## Inheritance Hierarchy
System.Object<br />&nbsp;&nbsp;SimpleAccess.Repository.Repository<br />
**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public class Repository : IRepository, IDisposable
```

**VB**<br />
``` VB
Public Class Repository
	Implements IRepository, IDisposable
```

**C++**<br />
``` C++
public ref class Repository : IRepository, 
	IDisposable
```

**F#**<br />
``` F#
type Repository =  
    class
        interface IRepository
        interface IDisposable
    end
```

The Repository type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository__ctor">Repository()</a></td><td>
Default constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository__ctor_1">Repository(SqlConnection)</a></td><td>
Constructor.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository__ctor_2">Repository(String)</a></td><td>
Constructor.</td></tr></table>&nbsp;
<a href="#repository-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")![Static member](media/static.gif "Static member")</td><td><a href="P_SimpleAccess_Repository_Repository_DefaultConnectionString">DefaultConnectionString</a></td><td>
Default connection string.</td></tr></table>&nbsp;
<a href="#repository-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_BeginTrasaction">BeginTrasaction</a></td><td>
Begins a transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Delete__1">Delete(TEntity)(SqlParameter[])</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Delete__1_3">Delete(TEntity)(Object)</a></td><td>
Deletes the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Delete__1_1">Delete(TEntity)(SqlTransaction, SqlParameter[])</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Delete__1_2">Delete(TEntity)(Int64, SqlTransaction)</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Dispose">Dispose</a></td><td>
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_EndTransaction">EndTransaction</a></td><td>
Ends a transaction.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>Equals</td><td>
Determines whether the specified object is equal to the current object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteNonQuery_2">ExecuteNonQuery(String, CommandType, SqlParameter[])</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteNonQuery_3">ExecuteNonQuery(String, CommandType, Object)</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteNonQuery">ExecuteNonQuery(SqlTransaction, String, CommandType, SqlParameter[])</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteNonQuery_1">ExecuteNonQuery(SqlTransaction, String, CommandType, Object)</a></td><td>
Executes the non query operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader_2">ExecuteReader(String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader_3">ExecuteReader(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader">ExecuteReader(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader_1">ExecuteReader(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader__1_2">ExecuteReader(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader__1_3">ExecuteReader(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader__1">ExecuteReader(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReader__1_1">ExecuteReader(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle_2">ExecuteReaderSingle(String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle_3">ExecuteReaderSingle(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle">ExecuteReaderSingle(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle_1">ExecuteReaderSingle(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle__1_2">ExecuteReaderSingle(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle__1_3">ExecuteReaderSingle(TEntity)(String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle__1">ExecuteReaderSingle(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), SqlParameter[])</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteReaderSingle__1_1">ExecuteReaderSingle(TEntity)(SqlTransaction, String, CommandType, String, Dictionary(String, PropertyInfo), Object)</a></td><td>
Executes the reader single operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteScalar__1_2">ExecuteScalar(T)(String, CommandType, SqlParameter[])</a></td><td>
Executes the scalar operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteScalar__1_3">ExecuteScalar(T)(String, CommandType, Object)</a></td><td>
Executes the scalar operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteScalar__1">ExecuteScalar(T)(SqlTransaction, String, CommandType, SqlParameter[])</a></td><td>
Executes the scalar operation.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_ExecuteScalar__1_1">ExecuteScalar(T)(SqlTransaction, String, CommandType, Object)</a></td><td>
Executes the scalar operation.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>Finalize</td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Get">Get(String, SqlParameter, String, Dictionary(String, PropertyInfo))</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Get_1">Get(String, Int64, String, Dictionary(String, PropertyInfo))</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Get_2">Get(String, Object, String, Dictionary(String, PropertyInfo))</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Get__1">Get(TEntity)(SqlParameter, SqlTransaction, String, Dictionary(String, PropertyInfo))</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Get__1_1">Get(TEntity)(Int64, SqlTransaction, String, Dictionary(String, PropertyInfo))</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Get__1_2">Get(TEntity)(Object, SqlTransaction, String, Dictionary(String, PropertyInfo))</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_GetAll__1">GetAll(TEntity)</a></td><td>
Enumerates get all in this collection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetHashCode</td><td>
Serves as the default hash function.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_GetNewConnection">GetNewConnection</a></td><td>
Gets the new connection.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>GetType</td><td>
Gets the Type of the current instance.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Insert__1_2">Insert(TEntity)(SqlParameter[])</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Insert__1_4">Insert(TEntity)(Object)</a></td><td>
Inserts the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Insert__1">Insert(TEntity)(StoredProcedureParameters)</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Insert__1_3">Insert(TEntity)(SqlTransaction, StoredProcedureParameters)</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Insert__1_1">Insert(TEntity)(StoredProcedureParameters, SqlTransaction)</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td>MemberwiseClone</td><td>
Creates a shallow copy of the current Object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_SoftDelete__1">SoftDelete(TEntity)</a></td><td>
Soft delete.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td>ToString</td><td>
Returns a string that represents the current object.
 (Inherited from Object.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Update__1_1">Update(TEntity)(SqlParameter[])</a></td><td>
Updates the given sqlParameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Update__1_3">Update(TEntity)(Object)</a></td><td>
Updates the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Update__1">Update(TEntity)(StoredProcedureParameters)</a></td><td>
Updates the given sqlParameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_Repository_Update__1_2">Update(TEntity)(SqlTransaction, StoredProcedureParameters)</a></td><td>
Updates the given sqlParameters.</td></tr></table>&nbsp;
<a href="#repository-class">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />