using Microsoft.AspNetCore.Http;

namespace AXExpansion.AspNetCore;

public static class AspNetCoreExpansions
{
    extension(IFormFile file)
    {
        public byte[] ReadToByteArray()
        {
            using var memStream = new MemoryStream();
            file.CopyTo(memStream);
            return memStream.ToArray();
        }
        
    }
}