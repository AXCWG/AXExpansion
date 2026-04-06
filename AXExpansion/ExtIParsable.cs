namespace AXExpansion;

#if NET7_0_OR_GREATER
public static class ExtIParsable
{
    extension<T>(T) where T : IParsable<T>
    { 
        public static T? TryParseOrDefault(string? s, IFormatProvider? provider = null)
        {
            T.TryParse(s, provider, out var result);
            return result; 
        }
        
    }

    
}

public static class ExtIParsableExtensions
{
    extension(string str)
    {
        public T Parse<T>(IFormatProvider? formatProvider = null) where T : IParsable<T>
        {
            return T.Parse(str, formatProvider);
        }

        public bool TryParse<T>( out T? res, IFormatProvider? formatProvider = null) where T : IParsable<T>
        {
            var r = T.TryParse(str, formatProvider, out var resL);
            res = resL;
            return r; 
        }

        public T? TryParseOrDefault<T>(IFormatProvider? formatProvider = null) where T: IParsable<T>
        {
            return ExtIParsable.TryParseOrDefault<T>(str, formatProvider);
        }
    }
}
#endif