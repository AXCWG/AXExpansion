namespace AXExpansion;

/// <summary>
/// Fluent control, where method chains together. 
/// </summary>
public static class FExpansion
{
    
    extension<T>(IList<T> l)
    {
        public IList<T> FAdd(T s)
        {
            l.Add(s);
            return l; 
        }

        public void AddRange(IList<T> s)
        {
            foreach (var x1 in s)
            {
                l.Add(x1);
            }
        }
        public IList<T> FAddRange(IList<T> s)
        {
            l.AddRange(s); 
            return l;
        }

        public IList<T> FRemoveAt(int index)
        {
            l.RemoveAt(index);
            return l; 
        }
        
    }
}

