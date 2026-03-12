using System.Text.Json;
using System.Text.Json.Serialization;
using SimpleJsonStorage;

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
        var serializedProcessed = h.Serialize(c =>
        {
            return c.Replace(":", "");
        });
        (serializedProcessed).PrintLn();
    }

    [TestMethod]
    public void IntegratedObjectPrint()
    {
        
        new[] {1,2,34,54,5,6,6,6,6,66}.SPrint();
        new List<int>{1,3,3,5,45,4,5,4,54,4}.SPrintLn();
        Enumerable.Range(2,566).SPrintLn();
    }

    [TestMethod]
    public void WriteFormat()
    {
        "{0}, {1}".PrintLnF(2, 4);
    }

    [TestMethod]
    public void Storage()
    {
        var p = new ProgramStorage<Hi>("axexpansion.test", "test");
        p.Set(new Hi
        {
            Content = "Hello World",
            Fucked = true, _dddd = "22222222222"
        });
    }

    [TestMethod]
    public void EnumerableStorage()
    {
        var p = new ProgramStorageSet<string>("axexpansion.test", "ienumerableTest");
        
    }

    [TestMethod]
    public void StoragePoolTest()
    {
        var p = new SampleStoragePool("axexpansion.test");
        p.Strings.Add("Hello world. ");
    }
}

public class Hi
{
    public string? Content { get; set; }
    public bool Fucked { get; set; }
    public string _dddd;

}

[JsonSerializable(typeof(Hi))]
[JsonSerializable(typeof(string))]
public partial class HiSerializerContext : JsonSerializerContext
{
}