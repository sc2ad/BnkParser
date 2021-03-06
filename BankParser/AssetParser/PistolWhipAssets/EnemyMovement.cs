﻿using AssetParser.AssetsChanger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class EnemyMovement
    {
        // Enemy: 0x120
        public Single walk { get; set; }
        // Enemy: 0x124
        public Single run { get; set; }
        [JsonConstructor]
        public EnemyMovement()
        { }

        public EnemyMovement(AssetsReader reader)
        {
            Parse(reader);
        }

        private void Parse(AssetsReader reader)
        {
            walk = reader.ReadSingle();
            run = reader.ReadSingle();
        }

        public void Write(AssetsWriter writer)
        {
            writer.Write(walk);
            writer.Write(run);
        }
    }
}
