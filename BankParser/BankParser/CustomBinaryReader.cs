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
            var disp = Position % alignment;
            if (disp != 0)
            {
                base.ReadBytes((int)disp);
            }
        }
    }
}
