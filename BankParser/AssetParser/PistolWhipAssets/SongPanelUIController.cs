using AssetParser.AssetsChanger;
using AssetParser.AssetsChanger.Assets;
using AssetParser.AssetsChanger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetParser.PistolWhipAssets
{
    public class SongPanelUIController : MonoBehaviourObject, INeedAssetsMetadata
    {
        // TYPE: SongSelectionUIController DNE?
        public SmartPtr<AssetsObject> parent { get; set; }
        public int levelDataIndex { get; set; }
        // TYPE: Image
        public SmartPtr<AssetsObject> songImage { get; set; }
        // TYPE: Image
        public SmartPtr<AssetsObject> selectionOutline { get; set; }
        public bool wipOverlayVisible { get; set; }
        // Align4
        public bool selectable { get; set; }
        // Align4
        // TYPE: TextMeshProUGUI
        public SmartPtr<AssetsObject> songInfoBasic { get; set; }
        public SmartPtr<WwiseStateReference> songSwitch { get; set; }
        // TYPE: GameEvent
        public SmartPtr<AssetsObject> songSelectedEvent { get; set; }
        // TYPE: Collider
        public SmartPtr<AssetsObject> selectableTrigger { get; set; }
        public SmartPtr<RectTransform> infoAnchorPoint { get; set; }
        public SongPanelUIController(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("SongPanelUIController"))
        {
        }

        public SongPanelUIController(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public override void ParseObject(AssetsReader reader)
        {
            
        }

        protected override void WriteObject(AssetsWriter writer)
        {
        }
    }
}
