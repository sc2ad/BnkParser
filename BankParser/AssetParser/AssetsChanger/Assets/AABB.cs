﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.AssetsChanger.Assets
{
    public class AABB
    {

        public AABB()
        { }
        public AABB(AssetsReader reader)
        {
            Parse(reader);
        }

        public void Parse(AssetsReader reader)
        {
            Center = new Vector3F(reader);
            Extent = new Vector3F(reader);
        }

        public void Write(AssetsWriter writer)
        {
            Center.Write(writer);
            Extent.Write(writer);
        }
        public Vector3F Center { get; set; }
        public Vector3F Extent { get; set; }
    }
}
