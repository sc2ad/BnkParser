using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class Vector3F
    {
        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Z { get; set; }

        public Vector3F()
        { }

        public Vector3F(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }
    }

    public class Vector3I
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector3I()
        { }

        public Vector3I(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
            Z = reader.ReadInt32();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }
    }

    public class Vector4F
    {

        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Z { get; set; }
        public Single W { get; set; }
        public Vector4F()
        { }

        public Vector4F(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
            W = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
            writer.Write(W);
        }

    }

    public class Vector2F
    {
        public Single X { get; set; }
        public Single Y { get; set; }
        public Vector2F()
        { }
        public Vector2F(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }
    }

    public class Vector2I
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2I()
        { }

        public Vector2I(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }
    }

    public class QuaternionF
    {

        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Z { get; set; }
        public Single W { get; set; }

        public QuaternionF()
        { }

        public QuaternionF(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
            W = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
            writer.Write(W);
        }
    }
}
