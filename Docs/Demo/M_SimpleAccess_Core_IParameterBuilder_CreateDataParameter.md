# IParameterBuilder.CreateDataParameter Method 
 

\[Missing <summary> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]

**Namespace:**&nbsp;<a href="N_SimpleAccess_Core">SimpleAccess.Core</a><br />**Assembly:**&nbsp;SimpleAccess.Core (in SimpleAccess.Core.dll) Version: 0.2.3.0 (0.2.5.0)

## Syntax

**C#**<br />
``` C#
IDataParameter CreateDataParameter(
	PropertyInfo propertyInfo,
	ParametersType parametesType,
	IEnumerable<PropertyInfo> propertyInfos,
	IList<PropertyInfo> outParameterPropertyInfoCollection,
	List<IDataParameter> outDataParameters
)
```

**VB**<br />
``` VB
Function CreateDataParameter ( 
	propertyInfo As PropertyInfo,
	parametesType As ParametersType,
	propertyInfos As IEnumerable(Of PropertyInfo),
	outParameterPropertyInfoCollection As IList(Of PropertyInfo),
	outDataParameters As List(Of IDataParameter)
) As IDataParameter
```

**C++**<br />
``` C++
IDataParameter^ CreateDataParameter(
	PropertyInfo^ propertyInfo, 
	ParametersType parametesType, 
	IEnumerable<PropertyInfo^>^ propertyInfos, 
	IList<PropertyInfo^>^ outParameterPropertyInfoCollection, 
	List<IDataParameter^>^ outDataParameters
)
```

**F#**<br />
``` F#
abstract CreateDataParameter : 
        propertyInfo : PropertyInfo * 
        parametesType : ParametersType * 
        propertyInfos : IEnumerable<PropertyInfo> * 
        outParameterPropertyInfoCollection : IList<PropertyInfo> * 
        outDataParameters : List<IDataParameter> -> IDataParameter 

```


#### Parameters
&nbsp;<dl><dt>propertyInfo</dt><dd>Type: System.Reflection.PropertyInfo<br />\[Missing <param name="propertyInfo"/> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]</dd><dt>parametesType</dt><dd>Type: <a href="T_SimpleAccess_Core_ParametersType">SimpleAccess.Core.ParametersType</a><br />\[Missing <param name="parametesType"/> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]</dd><dt>propertyInfos</dt><dd>Type: System.Collections.Generic.IEnumerable(PropertyInfo)<br />\[Missing <param name="propertyInfos"/> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]</dd><dt>outParameterPropertyInfoCollection</dt><dd>Type: System.Collections.Generic.IList(PropertyInfo)<br />\[Missing <param name="outParameterPropertyInfoCollection"/> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]</dd><dt>outDataParameters</dt><dd>Type: System.Collections.Generic.List(IDataParameter)<br />\[Missing <param name="outDataParameters"/> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]</dd></dl>

#### Return Value
Type: IDataParameter<br />\[Missing <returns> documentation for "M:SimpleAccess.Core.IParameterBuilder.CreateDataParameter(System.Reflection.PropertyInfo,SimpleAccess.Core.ParametersType,System.Collections.Generic.IEnumerable{System.Reflection.PropertyInfo},System.Collections.Generic.IList{System.Reflection.PropertyInfo},System.Collections.Generic.List{System.Data.IDataParameter})"\]

## See Also


#### Reference
<a href="T_SimpleAccess_Core_IParameterBuilder">IParameterBuilder Interface</a><br /><a href="N_SimpleAccess_Core">SimpleAccess.Core Namespace</a><br />