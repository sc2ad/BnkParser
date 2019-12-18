using AssetParser.AssetsChanger;
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
    public class Enemy : MonoBehaviourObject, INeedAssetsMetadata
    {
        public List<SmartPtr<GameObject>> enableOnSpawn { get; set; }
        public SmartPtr<AssetsObject> anim { get; set; }
        public SmartPtr<AssetsObject> projectile { get; set; }
        // 0x48
        public SmartPtr<AssetsObject> muzzle { get; set; }
        public SmartPtr<AssetsObject> ragdoll { get; set; }
        public SmartPtr<GameObject> disintegrateVFX { get; set; }
        // 0x6C
        public Event fireSound { get; set; }
        // 0x74
        public SmartPtr<AssetsObject> fireIndicatorRenderer { get; set; }
        public SmartPtr<AssetsObject> aimArmPivot { get; set; }
        public SmartPtr<AssetsObject> aimHandPivot { get; set; }
        public SmartPtr<AssetsObject> leftHandAim { get; set; }
        public SmartPtr<AssetsObject> rightHandAim { get; set; }
        // 0xBC
        public Single maxAimAngle { get; set; }
        public Single aimEaseTime { get; set; }
        public Single lookEaseInTime { get; set; }
        // 0xC8
        public AnimationCurve ikEaseInCurve { get; set; }
        // 0x110
        public bool hitSuppressesIK { get; set; }
        // Align4
        public Single ikSupressionTime { get; set; }
        public Single maxIKSupression { get; set; }
        public EnemyToughness toughness { get; set; }
        public EnemyMovement moveSpeeds { get; set; }
        public Single turnRate { get; set; }
        public Single deathDisposalDelay { get; set; }
        public Single deathDisintegrationVFXDelay { get; set; }
        // 0x134
        public SmartPtr<AssetsObject> avatarSkinnedMeshRenderer { get; set; }
        public List<SmartPtr<AssetsObject>> renderersToHideOnDeath { get; set; }
        // tmp:
        // SmartPtr<MeshRenderer>
        // SmartPtr<SkinnedMeshRenderer>
        // SmartPtr<MeshRenderer>
        // SmartPtr<MeshRenderer>
        // 0x174
        //public List<SmartPtr<TargetableObject>> targetables { get; set; }
        public List<SmartPtr<AssetsObject>> targetables { get; set; }
        // 0x1CC
        //public List<SmartPtr<BodyPart>> bodyParts { get; set; }
        public List<SmartPtr<AssetsObject>> bodyParts { get; set; }
        // 0x254
        //public List<SmartPtr<PlayerKiller>> playerKillers { get; set; }
        public List<SmartPtr<AssetsObject>> playerKillers { get; set; }
        public SmartPtr<AssetsObject> target { get; set; }
        public List<Single> fireTimes { get; set; }
        [JsonConstructor]
        public Enemy() { }

        public Enemy(IObjectInfo<AssetsObject> objectInfo, AssetsReader reader, bool parseLiteral = false) : base(objectInfo, reader, parseLiteral)
        {
        }

        public Enemy(AssetsFile assetsFile) : base(assetsFile, assetsFile.Manager.GetScriptObject("Enemy"))
        { }

        public override void ParseObject(AssetsReader reader)
        {
            enableOnSpawn = reader.ReadArrayOf((r) => SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, r));
            anim = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            projectile = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            muzzle = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            ragdoll = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            disintegrateVFX = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            fireSound = new Event(ObjectInfo, reader, true);
            fireIndicatorRenderer = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            aimArmPivot = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            aimHandPivot = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            leftHandAim = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            rightHandAim = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            maxAimAngle = reader.ReadSingle();
            aimEaseTime = reader.ReadSingle();
            lookEaseInTime = reader.ReadSingle();
            ikEaseInCurve = new AnimationCurve(reader);
            hitSuppressesIK = reader.ReadBoolean();
            reader.AlignTo(4);
            ikSupressionTime = reader.ReadSingle();
            maxIKSupression = reader.ReadSingle();
            toughness = reader.ReadEnum<EnemyToughness>();
            moveSpeeds = new EnemyMovement(reader);
            turnRate = reader.ReadSingle();
            deathDisposalDelay = reader.ReadSingle();
            deathDisintegrationVFXDelay = reader.ReadSingle();
            avatarSkinnedMeshRenderer = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            renderersToHideOnDeath = reader.ReadArrayOf((r) => SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader));
            targetables = reader.ReadArrayOf((r) => SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader));
            bodyParts = reader.ReadArrayOf((r) => SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader));
            playerKillers = reader.ReadArrayOf((r) => SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader));
            target = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            fireTimes = reader.ReadArrayOf((r) => r.ReadSingle());
        }

        protected override void WriteObject(AssetsWriter writer)
        {
            writer.WriteArrayOf(enableOnSpawn, (item, w) => item.WritePtr(w));
            anim.WritePtr(writer);
            projectile.WritePtr(writer);
            muzzle.WritePtr(writer);
            ragdoll.WritePtr(writer);
            disintegrateVFX.WritePtr(writer);
            fireSound.Write(writer);
            fireIndicatorRenderer.WritePtr(writer);
            aimArmPivot.WritePtr(writer);
            aimHandPivot.WritePtr(writer);
            leftHandAim.WritePtr(writer);
            rightHandAim.WritePtr(writer);
            writer.Write(maxAimAngle);
            writer.Write(aimEaseTime);
            writer.Write(lookEaseInTime);
            ikEaseInCurve.Write(writer);
            writer.Write(hitSuppressesIK);
            writer.AlignTo(4);
            writer.Write(ikSupressionTime);
            writer.Write(maxIKSupression);
            writer.Write((int)toughness);
            moveSpeeds.Write(writer);
            writer.Write(turnRate);
            writer.Write(deathDisposalDelay);
            writer.Write(deathDisintegrationVFXDelay);
            avatarSkinnedMeshRenderer.WritePtr(writer);
            writer.WriteArrayOf(renderersToHideOnDeath, (item, w) => item.WritePtr(w));
            writer.WriteArrayOf(targetables, (item, w) => item.WritePtr(w));
            writer.WriteArrayOf(bodyParts, (item, w) => item.WritePtr(w));
            writer.WriteArrayOf(playerKillers, (item, w) => item.WritePtr(w));
            target.WritePtr(writer);
            writer.WriteArrayOf(fireTimes, (item, w) => w.Write(item));
        }
    }
}
