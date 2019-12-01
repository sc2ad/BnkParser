using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankParser.HIRCData
{
    public class SoundStructure : Parsable
    {
        //public bool overrideParentEffects;
        //public byte effectCount;

        //#region EffectCount>0
        //public byte bitmaskEffect;
        //public List<EffectStructure> effects;
        //#endregion

        //public uint outputBusID;
        //public uint parentID;
        //public bool overrideParentPlayback;
        //public bool offsetPrioritySetting;
        //public byte additionalParametersCount;
        //public List<byte> additionalParameters;
        //// Each item is 4 bytes long, can vary between float/(u)int32
        //public List<byte[]> additionalParameterValues;
        //private byte _zero;
        //public bool positioningSection;

        //#region PositioningSection
        //public byte positioningType;
        //#region PositioningType==0
        //public bool enablePanner;
        //#endregion
        //#region PositioningType==1
        //public uint positionSource;
        //public uint attenuationID;
        //public bool enableSpatialization;
        //#region PositionSource==2
        //public uint playType;
        //public bool loop;
        //public uint transitionTime;
        //public bool listenerOrientation;
        //#endregion
        //#region PositionSource==3
        //public bool update;
        //#endregion
        //#endregion
        //#endregion

        //public bool overrideParentAuxiliary;
        //public bool gameDefinedAuxiliary;
        //public bool overrideParentUserAuxiliary;
        //public bool userAuxiliaryExists;

        //#region userAuxiliaryExists
        //public uint busID0;
        //public uint busID1;
        //public uint busID2;
        //public uint busID3;
        //#endregion

        //public bool playbackLimit;

        //#region playbackLimit
        //public byte priorityEqualApproach;
        //public byte limitReachedApproach;
        //public ushort limitSoundsTo;
        //#endregion

        //public bool overrideParentPlaybackLimit;
        //public byte limitSoundInstances;
        //public byte virtualVoiceBehavior;
        //public bool overrideVirtualVoice;
        public uint unknown4_0;
        public uint unknown_id;
        private byte[] _unknown4_0;
        public byte unknown1_0;
        public byte unknown1_1;
        public byte unknown1_2;
        public float someFloat;
        public uint twoFlags;
        public uint id;
        private byte[] _unknown12_0;
        public uint booleanData;
        // 0x2B
        public uint stateGroupsCount;
        public List<StateGroup> stateGroups;
        // 0x2F
        public ushort rtpcCount;
        public List<RTPC> rtpcs;

        public long Size
        { 
            get
            {
                long initial = 45;
                if (stateGroups.Count > 0)
                    initial += stateGroups.Count * stateGroups[0].Size;
                if (rtpcs.Count > 0)
                    initial += rtpcs.Count * rtpcs[0].Size;
                return initial;
            }
        }

        public SoundStructure(CustomBinaryReader reader)
        {
            Read(reader);
        }
        public void Read(CustomBinaryReader reader)
        {
            //overrideParentEffects = reader.ReadBoolean();
            //effectCount = reader.ReadByte();
            //if (effectCount > 0)
            //{
            //    bitmaskEffect = reader.ReadByte();
            //    effects = reader.ReadMany((r) => new EffectStructure(r), (ulong)effectCount);
            //}
            //outputBusID = reader.ReadUInt32();
            //parentID = reader.ReadUInt32();
            //overrideParentPlayback = reader.ReadBoolean();
            //offsetPrioritySetting = reader.ReadBoolean();
            //additionalParametersCount = reader.ReadByte();
            //additionalParameters = reader.ReadMany((r) => r.ReadByte(), (ulong)additionalParametersCount);
            //additionalParameterValues = reader.ReadMany((r) => r.ReadBytes(4), (ulong)additionalParametersCount);
            //_zero = reader.ReadByte();
            //positioningSection = reader.ReadBoolean();
            //if (positioningSection)
            //{
            //    positioningType = reader.ReadByte();
            //    if (positioningType == 0)
            //    {
            //        enablePanner = reader.ReadBoolean();
            //    } else if (positioningType == 1)
            //    {
            //        positionSource = reader.ReadUInt32();
            //        attenuationID = reader.ReadUInt32();
            //        enableSpatialization = reader.ReadBoolean();
            //    } else if (positioningType == 2)
            //    {
            //        playType = reader.ReadUInt32();
            //        loop = reader.ReadBoolean();
            //        transitionTime = reader.ReadUInt32();
            //        listenerOrientation = reader.ReadBoolean();
            //    } else if (positioningType == 3)
            //    {
            //        update = reader.ReadBoolean();
            //    } else
            //    {
            //        // ERROR!
            //        throw new ParseException($"Could not parse positioningType from {nameof(SoundStructure)}! Value: {positioningType} is not between 0 - 3, at reader offset: 0x{reader.Position:X}");
            //    }
            //}
            //overrideParentAuxiliary = reader.ReadBoolean();
            //gameDefinedAuxiliary = reader.ReadBoolean();
            //overrideParentUserAuxiliary = reader.ReadBoolean();
            //userAuxiliaryExists = reader.ReadBoolean();
            //if (userAuxiliaryExists)
            //{
            //    busID0 = reader.ReadUInt32();
            //    busID1 = reader.ReadUInt32();
            //    busID2 = reader.ReadUInt32();
            //    busID3 = reader.ReadUInt32();
            //}
            //playbackLimit = reader.ReadBoolean();
            //if (playbackLimit)
            //{
            //    priorityEqualApproach = reader.ReadByte();
            //    limitReachedApproach = reader.ReadByte();
            //    limitSoundsTo = reader.ReadUInt16();
            //}
            //limitSoundInstances = reader.ReadByte();
            //virtualVoiceBehavior = reader.ReadByte();
            //overrideParentPlaybackLimit = reader.ReadBoolean();
            //overrideVirtualVoice = reader.ReadBoolean();
            unknown4_0 = reader.ReadUInt32();
            unknown_id = reader.ReadUInt32();
            _unknown4_0 = reader.ReadBytes(4);
            unknown1_0 = reader.ReadByte();
            unknown1_1 = reader.ReadByte();
            unknown1_2 = reader.ReadByte();
            someFloat = reader.ReadSingle();
            twoFlags = reader.ReadUInt32();
            id = reader.ReadUInt32();
            _unknown12_0 = reader.ReadBytes(12);
            booleanData = reader.ReadUInt32();
            stateGroupsCount = reader.ReadUInt32();
            stateGroups = reader.ReadMany((r) => new StateGroup(r), (ulong)stateGroupsCount);
            rtpcCount = reader.ReadUInt16();
            rtpcs = reader.ReadMany((r) => new RTPC(r), (ulong)rtpcCount);
        }
        public void Write(CustomBinaryWriter writer, bool writeData = false)
        {
            //writer.Write(overrideParentEffects);
            //writer.Write((byte)effects.Count);
            //if (effects.Count > 0)
            //{
            //    writer.Write(bitmaskEffect);
            //    writer.WriteMany(effects);
            //}
            //writer.Write(outputBusID);
            //writer.Write(parentID);
            //writer.Write(overrideParentPlayback);
            //writer.Write(offsetPrioritySetting);
            //writer.Write((byte)additionalParameters.Count);
            //writer.WriteMany(additionalParameters, (p) => writer.Write(p));
            //writer.WriteMany(additionalParameterValues, (p) => writer.Write(p));
            //writer.Write(_zero);
            //writer.Write(positioningSection);
            //if (positioningSection)
            //{
            //    writer.Write(positioningType);
            //    if (positioningType == 0)
            //    {
            //        writer.Write(enablePanner);
            //    }
            //    else if (positioningType == 1)
            //    {
            //        writer.Write(positionSource);
            //        writer.Write(attenuationID);
            //        writer.Write(enableSpatialization);
            //    }
            //    else if (positioningType == 2)
            //    {
            //        writer.Write(playType);
            //        writer.Write(loop);
            //        writer.Write(transitionTime);
            //        writer.Write(listenerOrientation);
            //    }
            //    else if (positioningType == 3)
            //    {
            //        writer.Write(update);
            //    }
            //    else
            //    {
            //        // ERROR!
            //        throw new ParseException($"Could not write positioningType from {nameof(SoundStructure)}! Value: {positioningType} is not between 0 - 3, at writer offset: 0x{writer.Position:X}");
            //    }
            //}
            //writer.Write(overrideParentAuxiliary);
            //writer.Write(gameDefinedAuxiliary);
            //writer.Write(overrideParentUserAuxiliary);
            //writer.Write(userAuxiliaryExists);
            //if (userAuxiliaryExists)
            //{
            //    writer.Write(busID0);
            //    writer.Write(busID1);
            //    writer.Write(busID2);
            //    writer.Write(busID3);
            //}
            //writer.Write(playbackLimit);
            //if (playbackLimit)
            //{
            //    writer.Write(priorityEqualApproach);
            //    writer.Write(limitReachedApproach);
            //    writer.Write(limitSoundsTo);
            //}
            //writer.Write(limitSoundInstances);
            //writer.Write(virtualVoiceBehavior);
            //writer.Write(overrideParentPlaybackLimit);
            //writer.Write(overrideVirtualVoice);
            writer.Write(unknown4_0);
            writer.Write(unknown_id);
            writer.Write(_unknown4_0);
            writer.Write(unknown1_0);
            writer.Write(unknown1_1);
            writer.Write(unknown1_2);
            writer.Write(someFloat);
            writer.Write(twoFlags);
            writer.Write(id);
            writer.Write(_unknown12_0);
            writer.Write(booleanData);
            writer.Write(stateGroups.Count);
            writer.WriteMany(stateGroups);
            writer.Write(rtpcs.Count);
            writer.WriteMany(rtpcs);
        }
    }
}
