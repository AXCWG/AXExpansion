using System.Collections;

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
    
}

