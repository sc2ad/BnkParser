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
        // 0x20
        public List<SmartPtr<GameObject>> enableOnSpawn { get; set; }
        // 0x30
        // Animator
        public SmartPtr<AssetsObject> anim { get; set; }
        // 0x3C
        // Projectile
        public SmartPtr<AssetsObject> projectile { get; set; }
        // 0x48
        public SmartPtr<Transform> muzzle { get; set; }
        // 0x54
        // RagdollHandler
        public SmartPtr<AssetsObject> ragdoll { get; set; }
        // 0x60
        public SmartPtr<GameObject> disintegrateVFX { get; set; }
        // 0x6C
        public Event fireSound { get; set; }
        // 0x80
        public SmartPtr<Renderer> fireIndicatorRenderer { get; set; }
        // 0x8C
        public SmartPtr<Transform> aimArmPivot { get; set; }
        // 0x98
        public SmartPtr<Transform> aimHandPivot { get; set; }
        // 0xA4
        public SmartPtr<Transform> leftHandAim { get; set; }
        // 0xB0
        public SmartPtr<Transform> rightHandAim { get; set; }
        // 0xBC
        public Single maxAimAngle { get; set; }
        // 0xC0
        public Single aimEaseTime { get; set; }
        // 0xC4
        public Single lookEaseInTime { get; set; }
        // 0xC8
        public AnimationCurve ikEaseInCurve { get; set; }
        // 0x110
        public bool hitSuppressesIK { get; set; }
        // Align4
        // 0x114
        public Single ikSupressionTime { get; set; }
        // 0x118
        public Single maxIKSupression { get; set; }
        // 0x11C
        public EnemyToughness toughness { get; set; }
        // 0x120
        public EnemyMovement moveSpeeds { get; set; }
        // 0x128
        public Single turnRate { get; set; }
        // 0x12C
        public Single deathDisposalDelay { get; set; }
        // 0x130
        public Single deathDisintegrationVFXDelay { get; set; }
        // 0x134
        public SmartPtr<SkinnedMeshRenderer> avatarSkinnedMeshRenderer { get; set; }
        // 0x140
        public List<SmartPtr<Renderer>> renderersToHideOnDeath { get; set; }
        // tmp:
        // SmartPtr<MeshRenderer>
        // SmartPtr<SkinnedMeshRenderer>
        // SmartPtr<MeshRenderer>
        // SmartPtr<MeshRenderer>
        // 0x150
        public List<SmartPtr<TargetableObject>> targetables { get; set; }
        //public List<SmartPtr<AssetsObject>> targetables { get; set; }
        // 0x1A8
        public List<SmartPtr<BodyPart>> bodyParts { get; set; }
        //public List<SmartPtr<AssetsObject>> bodyParts { get; set; }
        // 0x230
        public List<SmartPtr<PlayerKiller>> playerKillers { get; set; }
        //public List<SmartPtr<AssetsObject>> playerKillers { get; set; }
        // 0x258
        public SmartPtr<Transform> target { get; set; }
        // 0x264
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
            muzzle = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
            ragdoll = SmartPtr<AssetsObject>.Read(ObjectInfo.ParentFile, this, reader);
            disintegrateVFX = SmartPtr<GameObject>.Read(ObjectInfo.ParentFile, this, reader);
            fireSound = new Event(ObjectInfo, reader, true);
            fireIndicatorRenderer = SmartPtr<Renderer>.Read(ObjectInfo.ParentFile, this, reader);
            aimArmPivot = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
            aimHandPivot = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
            leftHandAim = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
            rightHandAim = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
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
            avatarSkinnedMeshRenderer = SmartPtr<SkinnedMeshRenderer>.Read(ObjectInfo.ParentFile, this, reader);
            renderersToHideOnDeath = reader.ReadArrayOf((r) => SmartPtr<Renderer>.Read(ObjectInfo.ParentFile, this, reader));
            targetables = reader.ReadArrayOf((r) => SmartPtr<TargetableObject>.Read(ObjectInfo.ParentFile, this, reader));
            bodyParts = reader.ReadArrayOf((r) => SmartPtr<BodyPart>.Read(ObjectInfo.ParentFile, this, reader));
            playerKillers = reader.ReadArrayOf((r) => SmartPtr<PlayerKiller>.Read(ObjectInfo.ParentFile, this, reader));
            target = SmartPtr<Transform>.Read(ObjectInfo.ParentFile, this, reader);
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
