﻿using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.AssetsChanger.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public sealed class EnemyActionStopFiring : EnemyAction, INeedAssetsMetadata
    {
        [JsonConstructor]
        public EnemyActionStopFiring() { }
        public EnemyActionStopFiring(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public EnemyActionStopFiring(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("EnemyActionStopFiring"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            base.ParseObject(reader);
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            base.WriteObject(writer);
        }

        [System.ComponentModel.Browsable(false)]
        [Newtonsoft.Json.JsonIgnore]
        public override byte[] ScriptParametersData
        {
            get
            {
                throw new InvalidOperationException("Cannot access parameters data from this object.");
            }
            set
            {
                throw new InvalidOperationException("Cannot access parameters data from this object.");
            }
        }
    }
}
