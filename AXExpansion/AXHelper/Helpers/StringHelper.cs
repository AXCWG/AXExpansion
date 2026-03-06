using AXExpansion.AXHelper.Extensions;

namespace AXExpansion.AXHelper.Helpers;

public static class StringHelper
{
    public enum Format
    {
        Uppercase, Lowercase, Mixed
    }
    public static string RandomString(int length, Format format = Format.Mixed)
    {
        var s = string.Empty;
        switch (format)
        {
            case Format.Uppercase:
                for (int i = 0; i < length; i++)
                {
                    s += StringExtensions.Upper[Random.Shared.Next(0, 26)];
                }
                break;
            case Format.Lowercase:
                for (int i = 0; i < length; i++)
                {
                    
                    s += StringExtensions.Lower[Random.Shared.Next(0, 26)];
                }
                break;
            case Format.Mixed:
                for (int i = 0; i < length; i++)
                {
                    var capOrNot = Random.Shared.Next(0, 2) == 0; 
                    s += capOrNot ? StringExtensions.Upper[Random.Shared.Next(0, 26)] : StringExtensions.Lower[Random.Shared.Next(0, 26)];
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(format), format, null);
        }
        

        return s;
    }
    
}