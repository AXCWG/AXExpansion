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

        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
#if NET7_0_OR_GREATER
        [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
#endif
        public void Serialize(Action<string> cb, JsonSerializerOptions? options = null)
        {
            cb.Invoke(obj.Serialize(options));
        }

        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
#if NET7_0_OR_GREATER
        [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
#endif
        public TR Serialize<TR>(Func<string, TR> cb, JsonSerializerOptions? options = null)
        {
            return cb.Invoke(obj.Serialize(options));
        }
        /// <summary>
        /// NativeAOT Serialization.
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public string Serialize(JsonTypeInfo<T> j)
        {
            return JsonSerializer.Serialize(obj, j); 
        }
        /// <summary>
        /// NativeAOT Serialization with callbacks.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="j"></param>
        public void Serialize(Action<string> cb, JsonTypeInfo<T> j)
        {
            cb.Invoke(obj.Serialize(j));
        }
        /// <summary>
        /// NativeAOT Serialization with callbacks that return.
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="j"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
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
            obj.Serialize(s => s.PrintLn(), options ?? new JsonSerializerOptions {WriteIndented = true});
        }
        
        public void SPrint(JsonSerializerOptions? options = null)
        {
            obj.Serialize(s=>s.Print(), options ?? new JsonSerializerOptions{WriteIndented = true});
        }
    }

    extension(object? obj)
    {
        public bool IsNull() => obj is null;
    }

    extension(string? s)
    {
        public bool IsNullOrWhiteSpace() => string.IsNullOrWhiteSpace(s);
        public bool IsNullOrEmpty() =>  string.IsNullOrEmpty(s);
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

    extension<T>(IEnumerable<T> e)
    {
        /// <summary>
        /// Get a range of element. 
        /// </summary>
        /// <param name="range">Specified range to take. </param>
        /// <returns></returns>
        public IEnumerable<T> ElementAtRange(Range range)
        {
            return e.ToArray()[range].AsEnumerable();
        }
        /// <summary>
        /// Get a range of elements. If the range is not applicable in this enumerable, default value is returned. 
        /// </summary>
        /// <param name="range">Specified range to take. </param>
        /// <param name="default">Could be used to set default behavior. </param>
        /// <returns></returns>
        public IEnumerable<T>? ElementAtRangeOrDefault(Range range, IEnumerable<T>? @default = null)
        {
            var enumerable = e as T[] ?? e.ToArray();
            if (enumerable.ElementAtOrDefault(range.Start) is not null && enumerable.ElementAtOrDefault(range.End) is not null)
            {
                return @default;
            }

            return enumerable.ElementAtRange(range); 
        }
        /// <summary>
        /// Comparing to ChunkBy(), specify how many chunks you want to have.
        /// </summary>
        /// <param name="countOfChunks">Specify how many chunks you want to have.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Strange stuff happened</exception>
        public IEnumerable<IEnumerable<T>> ChunkWith(int countOfChunks)
        {
            var arrayBox = new List<T>[countOfChunks]; 
            for (var i = 0; i < arrayBox.Length; i++)
            {
                arrayBox[i] = new List<T>();
            }
            var arrayBoxEnumerator = arrayBox.GetEnumerator();
            var t = e.ToList(); 
            while (t.Count != 0)
            {
                if (!arrayBoxEnumerator.MoveNext())
                {
                    arrayBoxEnumerator.Reset();
                    continue; 
                }
                var item = t.ToArray()[Random.Shared.Next(t.Count)]; 
                ((List<T>)(arrayBoxEnumerator.Current ?? throw new Exception())).Add(item);
                t.Remove(item);
            }
            return arrayBox;
        }
        /// <summary>
        /// Get random item from IEnumerable. 
        /// </summary>
        /// <returns></returns>
        public T GetRandom()
        {
            var enumerable = e as T[] ?? e.ToArray();
            return enumerable.ElementAt(Random.Shared.Next(enumerable.Length));
        }
    }
}
