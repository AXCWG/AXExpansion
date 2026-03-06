using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using JetBrains.Annotations;

namespace AXExpansion;

public static class ObjectExpansion
{
    extension<T>(T obj)
    {
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
        #if NET7_0_OR_GREATER
        [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
        #endif
        public string Serialize(JsonSerializerOptions? options = null) 
            => JsonSerializer.Serialize(obj, options);

        public void Serialize(Action<string> cb, JsonSerializerOptions? options = null)
        {
            cb.Invoke(obj.Serialize(options));
        }

        public TR Serialize<TR>(Func<string, TR> cb, JsonSerializerOptions? options = null)
        {
            return cb.Invoke(obj.Serialize(options));
        }

        public string Serialize(JsonTypeInfo<T> j)
        {
            return JsonSerializer.Serialize(obj, j); 
        }

        public void Serialize(Action<string> cb, JsonTypeInfo<T> j)
        {
            cb.Invoke(obj.Serialize(j));
        }

        public TResult Serialize<TResult>(Func<string, TResult> cb, JsonTypeInfo<T> j)
        {
            return cb.Invoke(obj.Serialize(j)); 
        }
        
        
        public void PrintLn()
        {
            Console.WriteLine(obj);
        }

        public void Print()
        {
            Console.Write(obj);
        }

        public void SPrintLn(JsonSerializerOptions? options = null)
        {
            obj.Serialize(s => s.PrintLn(), options ?? new JsonSerializerOptions(){WriteIndented = true});
        }
        
        public void SPrint(JsonSerializerOptions? options = null)
        {
            obj.Serialize(s=>s.Print(), options ?? new JsonSerializerOptions{WriteIndented = true});
        }
    }
    extension(string s)
    {
        [StringFormatMethod(nameof(s))]
        public void PrintF(params object[] args)
        {
            Console.Write(s, args);
        }

        [StringFormatMethod(nameof(s))]
        public void PrintLnF(params object[] args)
        {
            Console.WriteLine(s, args);
        }
    }
}
