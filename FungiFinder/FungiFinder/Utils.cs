using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder
{
    public static class Utils
    {
        public static bool CheckFileSignature(Stream stream)
        {
            Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
            {
                { ".jpeg", new List<byte[]>
                    {
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                    }
                },
            };

            using (var reader = new BinaryReader(stream))
            {
                var signatures = _fileSignature[".jpeg"];
                var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                return signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }
    }
}
