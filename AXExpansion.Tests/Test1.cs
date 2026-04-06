using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using AXExpansion.AXHelper.Helpers;

namespace AXExpansion.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        var h = new Hi
        {
            Content = "WWWWWWW"
        };
        Console.WriteLine(
            h.Serialize(HiSerializerContext.Default.Hi));

    }

    [TestMethod]
    public void CbSerialize()
    {
        var h = new Hi
        {
            Content = "IPWJDIAI"
        };
        var serializedProcessed = h.Serialize(c => { return c.Replace(":", ""); });
        (serializedProcessed).PrintLn();
    }

    [TestMethod]
    public void IntegratedObjectPrint()
    {

        new[] { 1, 2, 34, 54, 5, 6, 6, 6, 6, 66 }.SPrint();
        new List<int> { 1, 3, 3, 5, 45, 4, 5, 4, 54, 4 }.SPrintLn();
        Enumerable.Range(2, 566).SPrintLn();
    }

    [TestMethod]
    public void WriteFormat()
    {
        "{0}, {1}".PrintLnF(2, 4);
    }

    [TestMethod]
    public void Range()
    {
        var a = new[] { 1, 2, 34, 5, 6 };
        a.ElementAtRangeOrDefault(1..6).SPrintLn();
    }

    [TestMethod]
    public void Test()
    {
        List<int> i = [1, 23, 3, 5, 6];
        foreach (var memberInfo in i.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance))
        {
            if (memberInfo is MethodInfo m)
            {
                if (m.Name == "Add")
                {
                    
                }
                Console.WriteLine(m.Name);
            }
        }
    }

}
//     [TestMethod]
//     public void Storage()
//     {
//         var p = new ProgramStorage<Hi>("axexpansion.test", "test");
//         p.Set(new Hi
//         {
//             Content = "Hello World",
//             Fucked = true, _dddd = "22222222222"
//         });
//     }
//
//     [TestMethod]
//     public void EnumerableStorage()
//     {
//         var p = new ProgramStorageSet<string>("axexpansion.test", "ienumerableTest");
//         
//     }
//
//     [TestMethod]
//     public void StoragePoolTest()
//     {
//         var p = new SampleStoragePool("axexpansion.test");
//         p.Strings.Add("Hello world. ");
//     }
//
//     [TestMethod]
//     public void BasicInputOutputHandling()
//     {
//         var p = new AppStorage();
//         p.AlbumCovers.Add(new()
//         {
//             Uuid = Guid.NewGuid().ToString(),
//             Name = "Dark side of the moon"
//         });
//         p.Ids.Add(new()
//         {
//             Uuid = Guid.NewGuid().ToString(),
//             Host = "8.141.5.12"
//         });
//         p.Settings.Set(i=>i.IsOn, false);
//         p.Settings.Get(i => i.Name)?.Length.PrintLn();
//         p.Ids.SPrintLn();
//         var x = Enumerable.Range(1, 2555).Select(_ => new Id
//         {
//             Uuid = Guid.NewGuid().ToString(),
//             Host = "2222"
//         }).ToList();
//         p.Ids.AddRange(x); 
//     }
// }
//
public class Hi
{
    public string? Content { get; set; }
    public bool Fucked { get; set; }
    public string _dddd;

}
//
[JsonSerializable(typeof(Hi))]
[JsonSerializable(typeof(string))]
public partial class HiSerializerContext : JsonSerializerContext
{
}
//
// public class AlbumCover
// {
//     public required string Uuid { get; set; }
//     public required string Name { get; set; }
// }
//
// public class Id
// {
//     public required string Uuid { get; set; }
//     public required string Host { get; set; }
// }
//
// public class Setting
// {
//     public bool IsAdmin { get; set; }
//     public string? Name { get; set; }
//     public bool IsOn{ get; set; }
// }
// public sealed class AppStorage : StoragePool
// {
//     public ProgramStorageSet<AlbumCover> AlbumCovers { get; set; } = null!;
//     public ProgramStorageSet<Id> Ids { get; set; } = null!;
//     public ProgramStorage<Setting> Settings { get; set; } = null!; 
//     
//     public AppStorage()
//     {
//         OnConfiguring("com.axcwg.test", new JsonSerializerOptions{WriteIndented = true});
//     }
// }