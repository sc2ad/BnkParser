using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public class CustomBinaryReader : BinaryReader
    {
        private const byte alignment = 4;
        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }
        public CustomBinaryReader(Stream input) : base(input)
        {
        }

        public void AlignStream()
        {
            var disp = alignment - Position % alignment;
            if (disp != 0)
            {
                base.ReadBytes((int)disp);
            }
        }

        public List<T> ReadMany<T>(Func<CustomBinaryReader, T> creator, ulong count)
        {
            var lst = new List<T>((int)count);
            for (ulong i = 0; i < count; i++)
            {
                lst.Add(creator(this));
            }
            return lst;
        }
    }
}
