namespace AXExpansion.AXHelper.Extensions;

public static class StreamExtensions
{
    /// <param name="anyStream" >Must be Stream or IFormFile</param>
    extension<T>(T anyStream) where T:Stream
    {
        
        /// <remarks>
        /// ONLY ACT ON Stream or IFormFile<br/><br/>
        /// </remarks>
        /// <summary>
        /// Set pos to 0, copy all data to memStream, convert to arr then set pos to original. 
        /// </summary>
        /// <returns>The byte array in byte[]</returns>
        /// <exception cref="InvalidOperationException">When the anyStream is neither IFormFile nor Stream. </exception>
        public byte[] ReadToByteArray()
        {
                using var memStream = new MemoryStream();
                var pos = anyStream.Position;
                anyStream.Position = 0; 
                anyStream.CopyTo(memStream);
                anyStream.Position = pos; 
                return memStream.ToArray();
        }
    }

    
}