using System.Collections;
using System.Numerics;

namespace AXExpansion;

/// <summary>
/// Fluent control, where method chains together. 
/// </summary>
public static class FExpansion
{
    
    extension<T>(ICollection<T> l)
    {
        public ICollection<T> FAdd(T s)
        {
            l.Add(s);
            return l; 
        }

        public void AddRange(IEnumerable<T> s)
        {
            foreach (var x1 in s)
            {
                l.Add(x1);
            }
        }
        public ICollection<T> FAddRange(IList<T> s)
        {
            l.AddRange(s); 
            return l;
        }

        
        
    }

    extension<T>(IList<T> l)
    {
        public IList<T> FRemoveAt(int index)
        {
            l.RemoveAt(index);
            return l; 
        }
    }

    extension<T>(ICollection<T> collection)
    {
        public void Remove(Func<T, bool> predicate)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (predicate.Invoke(collection.ElementAt(i)))
                {
                    collection.Remove(collection.ElementAt(i));
                    i--;
                }
            }
        }
    }


    extension(string path)
    {
        public string PathJoin(params string[] paths)
        {
            return Path.Join([path, ..paths]);
        }
    }

    extension<T>(T with)
    {
        /// <summary>
        /// General fluent method chain. 
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// var observable = new ObservableCollection<long>().With(o => o.CollectionChanged += (s, e) =>
        /// {
        ///    Console.WriteLine($"New for {(s as ObservableCollection<long>)!.Transform(p => {
        ///        var yieldResult = string.Empty;
        ///        foreach (var x1 in p)
        ///        {
        ///            yieldResult += x1;
        ///        }
        ///        return yieldResult; 
        ///    })}");
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="action">Action to invoke on the object. </param>
        /// <returns>The object. </returns>
        public T With(Action<T> action)
        {
            action.Invoke(with);
            return with; 
        }
        
        /// <summary>
        /// Uses the object to compute a value via <paramref name="action"/> and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the computed result.</typeparam>
        /// <param name="action">The function that computes a value from the object.</param>
        /// <returns>The value computed by <paramref name="action"/>.</returns>
        public TResult With<TResult>(Func<T, TResult> action)
        {
            return action.Invoke(with);
        }

        /// <summary>
        /// Designed for having the ability to "transform" an object into something else and immediately return it. Can be used for other uses. 
        /// </summary>
        /// <param name="transform">Delegate for transform. </param>
        /// <typeparam name="TResult">Type you want to return, usually inferred on return type of Func transform.</typeparam>
        /// <returns>The result of transform</returns>
        public TResult Transform<TResult>(Func<T, TResult> transform)
        {
            return transform.Invoke(with);
        }
        
    }

    extension(int num)
    {
        public bool In(Range x) => num >= x.Start.Value && num < x.End.Value;
    }
    
}

