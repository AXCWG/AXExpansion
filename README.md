# AXExpansion
Expansions to "vanilla C#". 

Mostly extension methods. 

### Contents
```csharp
object.Serialize();
object.(S)Print(Ln)(F)(); // S=>Serialize, Ln=>Console.WriteLine(), F=>C-style format, string only. 
object?.IsNull();
string?.IsNull(OrWhiteSpace | OrEmpty)();
IEnumerable<T>.ChunkWith(int countOfChunks); 
IEnumerable<T>.GetRandom(); 
IEnumerable<T>.ElementAtRange(Range r); 
IEnumerable<T>.ElementAtRangeOrDefault(Range r, IEnumerable<T>? @default); 
ICollection<T>.AddRange(IEnumerable<T> s); 
// Fluent methods, where every call returns itself. 
ICollection<T>.FAdd(T s); // Returns the same ICollection<T>.
ICollection<T>.FAddRange(IEnumerable<T> s); 
IList<T>.FRemoveAt(int index); 
string.PathJoin(params string[] paths); 
// StringHelper
StringHelper.RandomString(int length, Format format = Format.Mixed);
```

NuGet
---
[AXExpansion](https://www.nuget.org/packages/AXExpansion/)

[AXExpansion.AspNetCore](https://www.nuget.org/packages/AXExpansion.AspNetCore/)

[AXExpansion.Miscellaneous](https://www.nuget.org/packages/AXExpansion.Miscellaneous/)