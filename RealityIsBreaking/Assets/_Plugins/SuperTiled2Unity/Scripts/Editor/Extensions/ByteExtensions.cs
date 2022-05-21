using System;
using System.IO;
using System.IO.Compression;

namespace SuperTiled2Unity.Editor {
    public static class ByteExtensions {
        public static uint[] ToUInts(this byte[] bytes) {
            var u32s = new uint[bytes.Length / 4];
            for (var i = 0; i < u32s.Length; ++i) u32s[i] = BitConverter.ToUInt32(bytes, i * 4);
            return u32s;
        }

        public static byte[] GzipDecompress(this byte[] bytesCompressed) {
            var streamCompressed = new MemoryStream(bytesCompressed);

            // Now, decompress the bytes
            using (var streamDecompressed = new MemoryStream())
            using (var deflateStream = new GZipStream(streamCompressed, CompressionMode.Decompress)) {
                deflateStream.CopyTo(streamDecompressed);
                return streamDecompressed.ToArray();
            }
        }

        public static byte[] ZlibDeflate(this byte[] bytesCompressed) {
            var streamCompressed = new MemoryStream(bytesCompressed);

            // Nasty trick: Have to read past the zlib stream header
            streamCompressed.ReadByte();
            streamCompressed.ReadByte();

            // Now, decompress the bytes
            using (var streamDecompressed = new MemoryStream())
            using (var deflateStream = new DeflateStream(streamCompressed, CompressionMode.Decompress)) {
                deflateStream.CopyTo(streamDecompressed);
                return streamDecompressed.ToArray();
            }
        }
    }
}