# EntityFrameworkCore.Query.Extensions
EntityFrameworkCore Extensions for Single and Multiple resultSet 
### Querying single resultSet
```csharp
IReadOnlyCollection<TModel> results = context.QuerySingle<TModel>("dbo.procedureName", CommandType.StoredProcedure ,new SqlParameter("@p0","pValue"));
```
### Querying multiple resultSets
```csharp
var results = context.QueryMultiple<T1,T2,T3>("dbo.procedureName", CommandType.StoredProcedure ,new SqlParameter("@p0","pValue"));

IReadOnlyCollection<T1> first = results.FirstResultSet;
IReadOnlyCollection<T2> second = results.SecondResultSet;
IReadOnlyCollection<T3> third = results.ThirdResultSet;
```
### Injecting custom User Defined Type (UDT) parameters
```csharp
SqlParameter udtParameter = new SqlParameter("@parameterName","parameterValue");
udtParameter.TypeName = "dbo.UDT_name";
udtParameter.Value = new List<TModel>(){ new TModel( p1 = pV, p2 = pV)}
IReadOnlyCollection<T> results = context.QuerySingleAsync<T>("dbo.procedureName", CommandType.StoredProcedure, udtParameter);
```