using System.Collections;
using System.Reflection;
using System.Text.Json;

namespace SimpleJsonStorage;

public abstract class StoragePool
{
    public virtual void OnConfiguring(string identifier, JsonSerializerOptions? options = null)
    {
        foreach (var propertyInfo in GetType().GetProperties().Where(i => i.PropertyType == typeof(ProgramStorageSet<>).MakeGenericType(i.PropertyType.GenericTypeArguments[0])))
        {
            var t = propertyInfo.PropertyType.GenericTypeArguments[0];
            propertyInfo.SetValue(this, Activator.CreateInstance(typeof(ProgramStorageSet<>).MakeGenericType(t), args:
                [identifier, propertyInfo.Name, null, options]));
        }
    }
}

public sealed class SampleStoragePool : StoragePool
{
    public ProgramStorageSet<string> Strings { get; set; } = null!;

    public SampleStoragePool(string identifier, JsonSerializerOptions? options = null)
    {
        OnConfiguring(identifier, options);
    }
}