using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    /// <summary>
    /// This class is used to hash the provided strings to Wwise IDs.
    /// I need to do more research to determine if the name is all that is being hashed, or
    /// if it is some other set of parameters.
    /// Hash is computed on the original name of the object, with type = HashType.Hash32
    /// Example: "blackmagic" => 518895575
    /// </summary>
    public class FNVHash
    {
        private HashType type;
        private uint _hash32;
        public uint Hash32
        {
            get
            {
                if (type == HashType.Hash32 || type == HashType.Hash30)
                    return _hash32;
                throw new InvalidOperationException("Cannot get the Hash32/Hash30 of a non-32/30 hash!");
            }
            private set
            {
                _hash32 = value;
            }
        }
        private ulong _hash64;
        public ulong Hash64 { 
            get
            {
                if (type == HashType.Hash64)
                    return _hash64;
                throw new InvalidOperationException("Cannot get the Hash64 of a non-64 hash!");
            }
            private set
            {
                _hash64 = value;
            }
        }

        public FNVHash(HashType type)
        {
            ulong start = 0;
            switch (type)
            {
                case HashType.Hash30:
                    start = (ulong)Hash30.Offset;
                    break;
                case HashType.Hash32:
                    start = (ulong)BankParser.Hash32.Offset;
                    break;
                case HashType.Hash64:
                    start = (ulong)BankParser.Hash64.Offset;
                    break;
            }
            this.type = type;
            if (type == HashType.Hash64)
                Hash64 = start;
            else
                Hash32 = (uint)start;
        }

        public FNVHash(HashType type, ulong startValue)
        {
            this.type = type;
            if (type == HashType.Hash64)
                Hash64 = startValue;
            else
                Hash32 = (uint)startValue;
        }

        private ulong GetPrime()
        {
            switch (type)
            {
                case HashType.Hash30:
                    return (ulong)Hash30.Prime;
                case HashType.Hash32:
                    return (ulong)BankParser.Hash32.Prime;
                case HashType.Hash64:
                    return (ulong)BankParser.Hash64.Prime;
            }
            return 0;
        }

        private byte GetBits()
        {
            switch (type)
            {
                case HashType.Hash30:
                    return Hash30.Bits;
                case HashType.Hash32:
                    return BankParser.Hash32.Bits;
                case HashType.Hash64:
                    return BankParser.Hash64.Bits;
            }
            return 0;
        }

        public ulong Compute(byte[] data, int length)
        {
            if (type == HashType.Hash64)
            {
                for (int i = 0; i < length; i++)
                {
                    Hash64 *= GetPrime();
                    Hash64 ^= data[i];
                }
                return Hash64;
            } else
            {
                for (int i = 0; i < length; i++)
                {
                    Hash32 *= (uint)GetPrime();
                    Hash32 ^= data[i];
                }
                if (type == HashType.Hash30)
                {
                    uint mask = (uint)(1 << GetBits()) - 1;
                    return (Hash32 >> GetBits()) ^ (Hash32 & mask);
                }
                return Hash32;
            }
        }
    }
    public enum HashType
    {
        Hash32 = 0,
        Hash30,
        Hash64
    }
    public enum HashType32 : uint
    {
        Hash32_Prime = 16777619,
        Hash32_Offset = 2166136261U,
    }
    public enum HashType64 : ulong
    {
        Hash64_Prime = 1099511628211UL,
        Hash64_Offset = 14695981039346656037UL
    }
    public class Hash32
    {
        public const byte Bits = 32;
        public const HashType32 Prime = HashType32.Hash32_Prime;
        public const HashType32 Offset = HashType32.Hash32_Offset;
    }

    public class Hash30 : Hash32
    {
        public new const byte Bits = 30;
    }

    public class Hash64
    {
        public const byte Bits = 64;
        public const HashType64 Prime = HashType64.Hash64_Prime;
        public const HashType64 Offset = HashType64.Hash64_Offset;
    }
}
