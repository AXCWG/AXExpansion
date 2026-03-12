using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace SimpleJsonStorage;

[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
#if NET7_0_OR_GREATER
        [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
#endif
public class ProgramStorage<T>where T: new()
{
    /// <summary>
    /// Name of the storage
    /// </summary>
    public string StorageName { get; init;  }
    /// <summary>
    /// Storage path of the storage. 
    /// </summary>
    public string ProgramStoragePath { get; init;  }
    /// <summary>
    /// Options that'll be used every serialization and deserialization. 
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; init;  }
    /// <summary>
    /// Directory structure goes like: [path] / [identifier] / [name].json
    /// </summary>
    /// <param name="identifier">Program identifier.</param>
    /// <param name="name">Storage name. </param>
    /// <param name="path">Folder path. Defaults to <c>Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)</c></param>
    /// <param name="options"></param>
    public ProgramStorage(string identifier, string name, string? path = null, JsonSerializerOptions? options = null)
    {
        JsonSerializerOptions = options ?? new JsonSerializerOptions
        {
            IncludeFields = false
        };
        if (path == null)
        {
            if (!Directory.Exists(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    identifier, $"{name}.json")))
            {
                var dataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), identifier);
                Directory.CreateDirectory(dataPath); 
                ProgramStoragePath = Path.Join(dataPath,($"{name}.json"));
                File.WriteAllText(ProgramStoragePath, JsonSerializer.Serialize(new T()));
                StorageName = name; 
            }
            else
            {
                var dataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), identifier);
                ProgramStoragePath = Path.Join(dataPath,$"{name}.json");
                StorageName = name;

            }
            return; 
        }

        if (!File.Exists(Path.Join(path, identifier, $"{name}.json")))
        {
            var d = Path.Join(path, identifier);
            Directory.CreateDirectory(d); 
            ProgramStoragePath = Path.Join(d,$"{name}.json");
            File.WriteAllText(ProgramStoragePath, JsonSerializer.Serialize(new T()));
            StorageName = name; 
        }
        else
        {
            var d = Path.Join(path, identifier);
            ProgramStoragePath = Path.Join(d, $"{name}.json");
            StorageName = name; 
        }
        

    }
    /// <summary>
    /// Set the entire object. 
    /// </summary>
    /// <param name="obj"></param>
    public void Set(T obj)
    {
        File.WriteAllText(ProgramStoragePath, JsonSerializer.Serialize(obj, JsonSerializerOptions));
    }
    /// <summary>
    /// Set the property / field in storage. 
    /// </summary>
    /// <param name="expression">Expression that points to the property / field</param>
    /// <param name="value">Value to set. </param>
    /// <typeparam name="T2">Type of the specified property / field</typeparam>
    /// <exception cref="InvalidOperationException">Either its occured by specifying field name with no JsonSerializerOptions which has IncludeFields property set to true or giving something like <c>(i)=>i</c>. </exception>
    public void Set<T2>(Expression<Func<T, T2>> expression, T2 value)
    {
        T? i = JsonSerializer.Deserialize<T>(File.ReadAllText(ProgramStoragePath),  JsonSerializerOptions);
        if (i == null)
        {
            throw new NullReferenceException("The object is null. "); 
        }
        if (expression.Body is MemberExpression memberExpression)
        {
            if (memberExpression.Member is PropertyInfo propInfo)
            {
                typeof(T).GetProperty(propInfo.Name)?.SetValue(i, value);
                goto SERIALIZE; 
            }
            if (memberExpression.Member is FieldInfo fieldInfo)
            {
                if (!JsonSerializerOptions.IncludeFields)
                {
                    throw new InvalidOperationException(
                        "While specifying fields, please ensure a JsonSerializerOption with IncludeFields = true is passed in the construction of this object. ");
                }
                typeof(T).GetField(fieldInfo.Name)?.SetValue(i, value);
            }
        }
        else
        {
            throw new InvalidOperationException("Use Set(T obj) instead. ");
        }
        SERIALIZE:
        File.WriteAllText(ProgramStoragePath,JsonSerializer.Serialize(i, JsonSerializerOptions)); 
    }
    /// <summary>
    /// Get the property / field in storage. 
    /// </summary>
    /// <param name="expression">Expression that points to the property / field</param>
    /// <typeparam name="T2">Type of the specified property / field</typeparam>
    /// <returns>Value of specified property / field</returns>
    /// <exception cref="NullReferenceException">Object is null. </exception>
    /// <exception cref="InvalidOperationException">Specifying field name with no JsonSerializerOptions which has IncludeFields property set to true. </exception>
    public T2? Get<T2>(Expression<Func<T, T2>> expression)
    {
        T? i = JsonSerializer.Deserialize<T>(File.ReadAllText(ProgramStoragePath), JsonSerializerOptions);
        if (i == null)
        {
            throw new NullReferenceException("The object is null. ");
        }

        if (expression.Body is MemberExpression memberExpression)
        {
            if (memberExpression.Member is PropertyInfo propInfo)
            {
                return (T2?)typeof(T).GetProperty(propInfo.Name)?.GetValue(i);
            }

            if (memberExpression.Member is FieldInfo fieldInfo)
            {
                if (!JsonSerializerOptions.IncludeFields)
                {
                    throw new InvalidOperationException(
                        "While specifying fields, please ensure a JsonSerializerOption with IncludeFields = true is passed in the construction of this object. ");
                }

                return (T2?)typeof(T).GetField(fieldInfo.Name)?.GetValue(i);
            }
        }
        return (T2)(dynamic)i; 
    }

}