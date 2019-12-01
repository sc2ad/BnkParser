using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser
{
    public class CustomBinaryWriter : BinaryWriter
    {
        private const byte alignment = 4;
        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }
        public CustomBinaryWriter(Stream input) : base(input)
        {
        }

        public void AlignStream()
        {
            var disp = alignment - Position % alignment;
            if (disp != 0)
            {
                WriteZeros((int)disp);
            }
        }

        public void WriteZeros(int count)
        {
            byte[] bts = new byte[count];
            Write(bts);
        }

        public void WriteMany<T>(IEnumerable<T> toWrite, Action<T> action = null)
        {
            foreach (var t in toWrite)
            {
                if (t is Parsable)
                    (t as Parsable).Write(this);
                action?.Invoke(t);
            }
        }
    }
}
