using AssetParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using SevenZip.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZip;
using System.IO.Compression;

namespace AssetParser.AssetsChanger
{
    public class BundleFile
    {
        private class ProgressDummy : SevenZip.ICodeProgress
        {
            public void SetProgress(long inSize, long outSize)
            {
            }
            public static ProgressDummy Instance { get; } = new ProgressDummy();
        }
        public string PlayerVersion { get; set; }

        public string EngineVersion { get; set; }

        public Int64 BundleSize { get; set; }

        public List<DirectoryEntry> Entries { get; } = new List<DirectoryEntry>();
        public List<BlockInfo> BlockInfos { get; } = new List<BlockInfo>();

        public BundleFile(Stream stream)
        {
            using (var reader = new AssetsReader(stream, false))
            {
                Parse(reader);
            }
        }

        private UnityFSCompressionMode CompressionMode(UInt32 flags)
        {
            return (UnityFSCompressionMode)(flags & 0x3F);
        }

        private bool HasDirectoryInfo(UInt32 flags)
        {
            return (flags & 0x40) > 0;
        }

        private bool IsDirectoryAtEnd(UInt32 flags)
        {
            return (flags & 0x80) > 0;
        }

        private string signature;
        private int fileVersion;
        //private int compressedSize;
        //private int decompressedSize;
        private uint flags;
        private byte[] infoBytes;

        private void Parse(AssetsReader reader)
        {
            //basic header stuff
            signature = reader.ReadCStr();
            if (signature != "UnityFS")
                throw new NotSupportedException("Stream is not UnityFS");
            fileVersion = reader.ReadBEInt32();
            if (fileVersion != 6)
                throw new NotSupportedException("File version is not supported");
            PlayerVersion = reader.ReadCStr();
            EngineVersion = reader.ReadCStr();
            BundleSize = reader.ReadBEInt64();

            //header info
            int compressedSize = reader.ReadBEInt32();
            int decompressedSize = reader.ReadBEInt32();
            flags = reader.ReadBEUInt32();

            if (IsDirectoryAtEnd(flags))
            {
                var start = (int)reader.BaseStream.Position;
                reader.Seek((int)reader.BaseStream.Length - compressedSize);
                infoBytes = reader.ReadBytes(compressedSize);
                reader.Seek(start);
            }
            else
            {
                infoBytes = reader.ReadBytes(compressedSize);
            }

            Stream blockInfoStream = null;
            try
            {
                switch (CompressionMode(flags))
                {
                    case UnityFSCompressionMode.LZ4:
                    case UnityFSCompressionMode.LZ4HC:
                        blockInfoStream = new MemoryStream(LZ4.LZ4Codec.Decode(infoBytes, 0, infoBytes.Length, decompressedSize));
                        break;
                    case UnityFSCompressionMode.NoCompression:
                        blockInfoStream = new MemoryStream(infoBytes);
                        break;
                    case UnityFSCompressionMode.LZMA:
                        // TODO Better way of encoding/decoding
                        blockInfoStream = new MemoryStream(LZMADecode(infoBytes, decompressedSize));
                        break;
                }

                using (AssetsReader infoReader = new AssetsReader(blockInfoStream, false))
                    ParseDirectory(infoReader);
            }
            finally
            {
                if (blockInfoStream != null)
                {
                    blockInfoStream.Dispose();
                }
            }
            MemoryStream outputStream = new MemoryStream();
            foreach (var blockInfo in BlockInfos)
            {
                byte[] blockData = null;
                switch (blockInfo.CompressionMode)
                {
                    case UnityFSCompressionMode.LZ4:
                    case UnityFSCompressionMode.LZ4HC:
                        blockData = LZ4.LZ4Codec.Decode(reader.ReadBytes((int)blockInfo.CompressedSize), 0, (int)blockInfo.CompressedSize, (int)blockInfo.UncompressedSize);
                        break;
                    case UnityFSCompressionMode.NoCompression:
                        blockData = reader.ReadBytes((int)blockInfo.UncompressedSize);
                        break;
                    case UnityFSCompressionMode.LZMA:
                        blockData = LZMADecode(reader.BaseStream, (int)blockInfo.CompressedSize, (int)blockInfo.UncompressedSize);
                        break;
                }

                outputStream.Write(blockData, 0, blockData.Length);
            }
            using (outputStream)
            {
                foreach (var entry in Entries)
                {
                    outputStream.Seek(entry.Offset, SeekOrigin.Begin);
                    entry.Data = outputStream.ReadBytes((int)entry.Size);
                }
            }
        }

        public void Write(AssetsWriter writer, byte[] compressedData)
        {
            if (fileVersion != 6)
                throw new NotSupportedException("File version is not supported");
            //basic header stuff
            writer.WriteCString(signature);
            writer.WriteBEInt32(fileVersion);
            // TODO ADD
            // WriteDirectory to a MemoryStream
            // Check length of the resultant byte array, should be = UncompressedLength
            // Compress it
            // Write compressed length
            MemoryStream s = new MemoryStream();
            using (AssetsWriter w = new AssetsWriter(s))
            {
                WriteDirectory(w);
            }
            int uncompressedLength = (int)s.Length;
            // todo
            var compressedMetadata = LZ4.LZ4Codec.EncodeHC(s.ToArray(), 0, uncompressedLength);
            BundleSize += compressedMetadata.Length;
            writer.WriteCString(PlayerVersion);
            writer.WriteCString(EngineVersion);
            writer.WriteBEInt64(BundleSize);

            // compressedSize is actually the size of the METADATA byte array
            // The one with the Entries and BlockInfos + unknown directory header
            writer.WriteBEInt32(compressedMetadata.Length);
            writer.WriteBEInt32(uncompressedLength);
            writer.WriteBEUInt32(flags);

            if (!IsDirectoryAtEnd(flags))
            {
                // Actually write the directory information to the file
                writer.Write(compressedMetadata);
            }
            // Now write the actual data the BlockInfos use
            writer.Write(compressedData);
            if (IsDirectoryAtEnd(flags))
            {
                // Actually write the directory information to the file
                writer.Write(compressedMetadata);
            }
        }

        private static byte[] LZMADecode(byte[] inputData, int uncompressedSize)
        {
            if (inputData.Length < 5)
                throw new ArgumentException("Input data is too short.");
            var decoder = new SevenZip.Compression.LZMA.Decoder();
            var outProps = new byte[5];
            Array.Copy(inputData, 0, outProps, 0, 5);
            decoder.SetDecoderProperties(outProps);
            using (var outLZMA = new MemoryStream())
            using (var inLZMA = new MemoryStream(inputData, 13, inputData.Length - 13))
            {
                decoder.Code(inLZMA, outLZMA, inputData.Length - 13, uncompressedSize, null);
                return outLZMA.ToArray();
            }
        }

        private static byte[] LZMAEncode(byte[] inputData, byte[] properties)
        {
            if (inputData.Length < 5)
                throw new ArgumentException("Input data is too short.");
            var encoder = new SevenZip.Compression.LZMA.Encoder();
            var val = properties[0] / 9;
            var dSize = BitConverter.ToUInt32(properties, 1);
            var obs = new object[]
            {
                properties[0] % 9,
                val % 5,
                val / 5,
                dSize
            };
            encoder.SetCoderProperties(new CoderPropID[] { CoderPropID.LitContextBits, CoderPropID.LitPosBits, CoderPropID.PosStateBits, CoderPropID.DictionarySize }, obs);
            using (var outLZMA = new MemoryStream())
            using (var inLZMA = new MemoryStream(inputData, 0, inputData.Length))
            {
                encoder.Code(inLZMA, outLZMA, inputData.Length, -1, null);
                return outLZMA.ToArray();
            }
        }

        private static byte[] LZMADecode(Stream inStream, int compressedSize, int uncompressedSize)
        {
            var decoder = new SevenZip.Compression.LZMA.Decoder();
            var properties = new byte[5];
            if (inStream.Read(properties, 0, 5) != 5)
                throw new ArgumentException("Input data is too short.");
            decoder.SetDecoderProperties(properties);
            using (var outLZMA = new MemoryStream())
            {
                decoder.Code(inStream, outLZMA, compressedSize, uncompressedSize, null);
                return outLZMA.ToArray();
            }
        }

        private byte[] _directoryUnknown;
        private void ParseDirectory(AssetsReader reader)
        {
            //unknown?
            _directoryUnknown = reader.ReadBytes(16);
            int numBlocks = reader.ReadBEInt32();

            for (int i = 0; i < numBlocks; i++)
                BlockInfos.Add(new BlockInfo(reader));

            int numEntries = reader.ReadBEInt32();
            for (int i = 0; i < numEntries; i++)
            {
                Entries.Add(new DirectoryEntry(reader));
            }
        }

        private void WriteDirectory(AssetsWriter writer)
        {
            //unknown?
            writer.Write(_directoryUnknown);
            writer.WriteBEInt32(BlockInfos.Count);

            for (int i = 0; i < BlockInfos.Count; i++)
                BlockInfos[i].Write(writer);

            writer.WriteBEInt32(Entries.Count);
            for (int i = 0; i < Entries.Count; i++)
            {
                Entries[i].Write(writer);
            }
        }

        // TODO BASICALLY FIX THIS ENTIRE FUNCTION
        public void Save(Stream stream)
        {
            // Fix parsing of DirectoryEntries
            long offset = 0;

            var oldBIs = new List<BlockInfo>();
            foreach (var bi in BlockInfos)
            {
                oldBIs.Add(bi);
            }

            uint blockLen = BlockInfos[0].UncompressedSize;

            long compsum = 0;
            long uncompsum = 0;
            foreach (var bi in BlockInfos)
            {
                compsum += bi.CompressedSize;
                uncompsum += bi.UncompressedSize;
            }
            var unknownDict = new Dictionary<int, BlockInfo>();
            for (int i = 0; i < BlockInfos.Count; i++)
            {
                if (BlockInfos[i].CompressionMode != UnityFSCompressionMode.LZ4HC)
                {
                    unknownDict.Add(i, BlockInfos[i]);
                }
            }

            List<byte> data = new List<byte>();
            List<byte> compressed = new List<byte>();
            long uncompressedSize = 0;
            for (int i = 0; i < Entries.Count; i++)
            {
                Entries[i].Offset = offset;
                offset += Entries[i].Size;
                uncompressedSize += Entries[i].Size;
                data.AddRange(Entries[i].Data);
            }
            // offset = length of stream
            // Fix parsing of all blocks
            UnityFSCompressionMode compMode = BlockInfos[0].CompressionMode;
            long blockCount = uncompressedSize / blockLen + 1;
            BlockInfos.Clear();
            BundleSize = 0;
            for (int i = 0; i < blockCount; i++)
            {
                long len = blockLen;
                if (uncompressedSize - i * blockLen < blockLen)
                {
                    // Not correct size!
                    len = uncompressedSize - i * blockLen;
                }
                var bts = data.GetRange((int)(i * blockLen), (int)len);
                if (unknownDict.ContainsKey(i))
                {
                    // This is actually a really poor way of doing it, I really need to clean all of this up
                    BlockInfos.Add(new BlockInfo()
                    {
                        UncompressedSize = (uint)len,
                        CompressionMode = unknownDict[i].CompressionMode,
                        CompressedSize = (uint)len
                    });
                    compressed.AddRange(bts.ToArray());
                } else
                {
                    //byte[] o = new byte[oldBIs[i].CompressedSize];
                    //var dat = LZ4.LZ4Codec.EncodeHC(bts.ToArray(), 0, bts.Count, o, 0, (int)oldBIs[i].CompressedSize);

                    var dat = LZ4.LZ4Codec.EncodeHC(bts.ToArray(), 0, bts.Count);

                    LZ4.LZ4Codec.Decode(dat, 0, dat.Length, bts.Count);
                    //var q = LZ4.LZ4Codec.Decode(dat, 0, dat, bts.Count);

                    //for (int l = 0; l < q.Length; l++)
                    //{
                    //    if (q[l] != bts[l])
                    //        Console.WriteLine("Byte does not match error");
                    //}

                    compressed.AddRange(dat);
                    
                    BlockInfos.Add(new BlockInfo()
                    {
                        UncompressedSize = (uint)len,
                        CompressionMode = compMode,
                        CompressedSize = (uint)dat.Length
                    });
                }
            }
            BundleSize = compressed.Count;
            
            using (AssetsWriter writer = new AssetsWriter(stream))
            {
                Write(writer, compressed.ToArray());
            }
        }
    }

    public enum UnityFSCompressionMode : UInt32
    {
        NoCompression = 0,
        LZMA = 1,
        LZ4 = 2,
        LZ4HC = 3
    }

    public class DirectoryEntry
    {
        public Int64 Offset { get; set; }
        public Int64 Size { get; set; }
        private UInt32 _flags;
        public string Filename { get; set; }
        public byte[] Data { get; set; }

        public DirectoryEntry()
        {

        }

        public DirectoryEntry(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            Offset = reader.ReadBEInt64();
            Size = reader.ReadBEInt64();
            _flags = reader.ReadBEUInt32();
            Filename = reader.ReadCStr();
        }

        public void Write(AssetsWriter writer)
        {
            writer.WriteBEInt64(Offset);
            writer.WriteBEInt64(Size);
            writer.WriteBEUInt32(_flags);
            writer.WriteCString(Filename);
        }
    }
    public class BlockInfo
    {
        public UInt32 CompressedSize { get; set; }
        public UInt32 UncompressedSize { get; set; }
        private UInt16 _flags;
        public UnityFSCompressionMode CompressionMode
        {
            get
            {
                return (UnityFSCompressionMode)(_flags & 0x3F);
            }
            set
            {

                _flags = (ushort)((_flags & ~0x3F) | ((ushort)value & 0x3F));
            }
        }
        public BlockInfo()
        {

        }

        public BlockInfo(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            UncompressedSize = reader.ReadBEUInt32();
            CompressedSize = reader.ReadBEUInt32();

            _flags = reader.ReadBEUInt16();
        }

        public void Write(AssetsWriter writer)
        {
            writer.WriteBEUInt32(UncompressedSize);
            writer.WriteBEUInt32(CompressedSize);
            writer.WriteBEUInt16(_flags);
        }
    }
}
