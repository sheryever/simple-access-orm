# ISqlRepository Interface
 

Represent the interface of SimpleAccess Repository methods and it's implemented by SqlRepository

**Namespace:**&nbsp;<a href="N_SimpleAccess_Repository">SimpleAccess.Repository</a><br />**Assembly:**&nbsp;SimpleAccess.SqlServer (in SimpleAccess.SqlServer.dll) Version: 0.2.3.0 (0.2.8.0)

## Syntax

**C#**<br />
``` C#
public interface ISqlRepository
```

**VB**<br />
``` VB
Public Interface ISqlRepository
```

**C++**<br />
``` C++
public interface class ISqlRepository
```

**F#**<br />
``` F#
type ISqlRepository =  interface end
```

The ISqlRepository type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Delete__1">Delete(TEntity)(SqlParameter[])</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Delete__1_3">Delete(TEntity)(Int64)</a></td><td>
Deletes TEntity the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Delete__1_4">Delete(TEntity)(Object)</a></td><td>
Deletes the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Delete__1_1">Delete(TEntity)(SqlTransaction, SqlParameter[])</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Delete__1_2">Delete(TEntity)(SqlTransaction, Int64)</a></td><td>
Deletes the given ID.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Get__1">Get(TEntity)(SqlParameter, String)</a></td><td>
Get TEntity by Id or anyother parameter.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Get__1_1">Get(TEntity)(SqlTransaction, SqlParameter, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Get__1_2">Get(TEntity)(Int64, SqlTransaction, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Get__1_3">Get(TEntity)(Object, SqlTransaction, String)</a></td><td>
Gets.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_GetAll__1">GetAll(TEntity)</a></td><td>
Get all TEntity object in a IEnumerable(T).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Insert__1_1">Insert(TEntity)(SqlParameter[])</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Insert__1_3">Insert(TEntity)(Object)</a></td><td>
Inserts the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Insert__1">Insert(TEntity)(StoredProcedureParameters)</a></td><td>
Inserts the given TEntity.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Insert__1_2">Insert(TEntity)(SqlTransaction, StoredProcedureParameters)</a></td><td>
Inserts the given SQL parameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_SoftDelete__1">SoftDelete(TEntity)</a></td><td>
Soft delete.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Update__1_1">Update(TEntity)(SqlParameter[])</a></td><td>
Updates the given sqlParameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Update__1_3">Update(TEntity)(Object)</a></td><td>
Updates the given dynamic object as SqlParameter names and values.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Update__1">Update(TEntity)(StoredProcedureParameters)</a></td><td>
Updates the given sqlParameters.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_SimpleAccess_Repository_ISqlRepository_Update__1_2">Update(TEntity)(SqlTransaction, StoredProcedureParameters)</a></td><td>
Updates the given sqlParameters.</td></tr></table>&nbsp;
<a href="#isqlrepository-interface">Back to Top</a>

## See Also


#### Reference
<a href="N_SimpleAccess_Repository">SimpleAccess.Repository Namespace</a><br />