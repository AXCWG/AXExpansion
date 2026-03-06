using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace AXExpansion.AXHelper.Extensions;

public static class StringExtensions
{
    public const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string Lower = "abcdefghijklmnopqrstuvwxyz";
    extension(string source)
    {
        

        #region JoinWith
        public string JoinWith(char separator, params string[] other)
        {
            return other.Aggregate(source, (current, s1) => current + (separator + s1));
        }
        public string JoinWith(string separator, params string[] other)
        {
            return other.Aggregate(source, (current, s1) => current + (separator + s1));
        }

        public string JoinWith(int separator, params string[] other)
        {
            return source.JoinWith(separator.ToString(CultureInfo.InvariantCulture), other);
        }
        public string JoinWith(long separator, params string[] other)
        {
            return source.JoinWith(separator.ToString(CultureInfo.InvariantCulture), other);
        }
        public string JoinWith(double separator, params string[] other)
        {
            return source.JoinWith(separator.ToString(CultureInfo.InvariantCulture), other);
        }
        public string JoinWith(float separator, params string[] other)
        {
            return source.JoinWith(separator.ToString(CultureInfo.InvariantCulture), other);
        }
        

        #endregion

        #region Sha256

        public byte[] ToSha256()
                {
                    using var sha256 = SHA256.Create();
                    var bytes = Encoding.UTF8.GetBytes(source);
                    var hash = sha256.ComputeHash(bytes);
                    return hash;
                }
        public string ToSha256String()
                {
                    var hex = BitConverter.ToString(ToSha256(source)).Replace("-", "").ToLower();
                    return hex;
                }

        #endregion
        
        /// <summary>
        /// Move every character that fells into English alphabet to offset by a certain offset.
        /// </summary>
        /// <param name="by">Offsets to move</param>
        /// <returns></returns>
        public string AlphabeticalShift(int by)
        {
            char[] sChar =  source.ToCharArray();
            for (var index = 0; index < sChar.Length; index++)
            {
                if (Upper.Contains(sChar[index]))
                {
                    sChar[index] = Upper.ElementAt(Upper.IndexOf(sChar[index]) + by);
                }
                if (Lower.Contains(sChar[index]))
                {
                    sChar[index] = Lower.ElementAt(Lower.IndexOf(sChar[index]) + by);
                }
            }
            return new string(sChar);
        }

        public string ConcatEllipsisByChar(int count)
        {
            if (count > source.Length)
            {
                return source; 
            }
            var s = source[0..count];
            return s + (source.Equals(s) ? "" : "...");
        }
        public string ConcatEllipsisByWord(int count)
        {
            var strings = source.Split(' ', StringSplitOptions.TrimEntries)[0..count];
            return strings.Combine(' ') + ( source.Equals(strings.Combine(' ')) ? "" :  "...");
        }

        
    }
    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<T> RemoveLast()
        {
            return source.ToArray()[..^1];
        }
    }

    extension(IEnumerable<string> source)
    {
        public string Combine()
        {
            return string.Join(null, source);
        }
        public string Combine(char separatorWith)
        {
            return string.Join(separatorWith, source);
        }
    }

    
    
    
}
