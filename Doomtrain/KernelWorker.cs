﻿using System;
using System.Text;

namespace Doomtrain
{
    class KernelWorker
    {
        public static byte[] Kernel;

        public static int MagicDataOffset = -1;
        public static int OffsetToMagicSelected = -1;

        public static int GFDataOffset = -1;
        public static int OffsetToGFSelected = -1;

        public static int GFAttacksDataOffset = -1;
        public static int OffsetToGFAttacksSelected = -1;

        public static int WeaponsDataOffset = -1;
        public static int OffsetToWeaponsSelected = -1;

        public static MagicData GetSelectedMagicData;
        public static GFData GetSelectedGFData;
        public static GFAttacksData GetSelectedGFAttacksData;
        public static WeaponsData GetSelectedWeaponsData;

        static string[] _charstable;
        private static readonly string Chartable =
        @" , ,1,2,3,4,5,6,7,8,9,%,/,:,!,?,…,+,-,=,*,&,「,」,(,),·,.,,,~,“,”,‘,#,$,',_,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,À,Á,Â,Ä,Ç,È,É,Ê,Ë,Ì,Í,Î,Ï,Ñ,Ò,Ó,Ô,Ö,Ù,Ú,Û,Ü,Œ,ß,à,á,â,ä,ç,è,é,ê,ë,ì,í,î,ï,ñ,ò";


        public enum KernelSections : ushort
        { //BitShift to left, to make fast and natural MULTIPLY BY 2 operation
            BattleCommands = 1 << 2,
            MagicData = 2 << 2,
            GFs = 3 << 2,
            EnemyAttacks = 4 << 2,
            Weapons = 5 << 2,
            RenzokukenFinisher = 6 << 2,
            Characters = 7 << 2,
            BattleItems = 8 << 2,
            NonBattleItems = 9 << 2,
            GFAttacks = 10 << 2,
            CommandAbility = 11 << 2,
            JunctionAbilities = 12 << 2,
            CommandAbilities = 13 << 2,
            StatPercentageIncreasingAbilities = 14 << 2,
            CharacterAbilities = 15 << 2,
            PartyAbilities = 16 << 2,
            GFAbilities = 17 << 2,
            MenuAbilities = 18 << 2,
            CharacterLimitBreakes = 19 << 2,
            BlueMagic = 20 << 2,
            BlueMagicParam = 21 << 2,
            Shot_Irvine = 22 << 2,
            Duel_Zell = 23 << 2,
            Duel_ZellParam = 24 << 2,
            RinoaLimit1 = 25 << 2,
            RinoaLimit2 = 26 << 2,
            SelphieSlotArray = 27 << 2,
            SelphieSlotSets = 28 << 2,
            Devour = 29 << 2,
            Misc = 30 << 2,
            MiscTextPointers = 31 << 2,
            Text_BattleCommand = 32 << 2,
            Text_Magictext = 33 << 2,
            Text_JunctionableGF = 34 << 2,
            Text_Enemyattacktext = 35 << 2,
            Text_Weapontext = 36 << 2,
            Text_Renzokukenfinisherstext = 37 << 2,
            Text_Characternames = 38 << 2,
            Text_Battleitemnames = 39 << 2,
            Text_Nonbattleitemnames = 40 << 2,
            Text_NonjunctionableGFattackname = 41 << 2,
            Text_Junctionabilitiestext = 42 << 2,
            Text_Commandabilitiestext = 43 << 2,
            Text_Statpercentageincreasingabilitiestext = 44 << 2,
            Text_Characterabilitytext = 45 << 2,
            Text_Partyabilitytext = 46 << 2,
            Text_GFabilitytext = 47 << 2,
            Text_Menuabilitytext = 48 << 2,
            Text_Temporarycharacterlimitbreaktext = 49 << 2,
            Text_Bluemagictext = 50 << 2,
            Text_Shottext = 51 << 2,
            Text_Dueltext = 52 << 2,
            Text_Rinoalimitbreaktext = 53 << 2,
            Text_Rinoalimitbreaktext2 = 54 << 2,
            Text_Devourtext = 55 << 2,
            Text_Misctext = 56 << 2,


        }


        internal enum Element : byte
        {
            Fire = 0x01,
            Ice = 0x02,
            Thunder = 0x04,
            Earth = 0x08,
            Poison = 0x10,
            Wind = 0x20,
            Water = 0x40,
            Holy = 0x80,
            NonElemental = 0x00
        }

        internal enum Characters : byte
        {
            Squall = 0x00,
            Zell = 0x01,
            Irvine = 0x02,
            Quistis = 0x03,
            Rinoa = 0x04,
            Selphie = 0x05,
            Seifer = 0x06,
            Edea = 0x07,
            Laguna = 0x08,
            Kiros = 0x09,
            Ward = 0x0A
        }

        internal enum Status0 : UInt16 //Statuses 0-7 - How the game stores this data - upper bits are unused
        {
            Death = 0x0001,
            Poison = 0x0002,
            Petrify = 0x0004,
            Darkness = 0x0008,
            Silence = 0x0010,
            Berserk = 0x0020,
            Zombie = 0x0040,
            Unknown = 0x0080 //possibly unused - causing this status leaves you unable to cure it
        }

        internal enum Status1 : UInt32 //Statuses 8-39 - How the game stores this data
        {
            Sleep = 0x00000001,
            Haste = 0x00000002,
            Slow = 0x00000004,
            Stop = 0x00000008,
            Regen = 0x00000010,
            Protect = 0x00000020,
            Shell = 0x00000040,
            Reflect = 0x00000080,
            Aura = 0x00000100,
            Curse = 0x00000200,
            Doom = 0x00000400,
            Invincible = 0x00000800,
            Petrifying = 0x00001000,
            Float = 0x00002000,
            Confusion = 0x00004000,
            Drain = 0x00008000,
            Eject = 0x00010000,
            Double = 0x00020000,
            Triple = 0x00040000,
            Defend = 0x00080000,
            Unknown1 = 0x00100000,
            Unknown2 = 0x00200000,
            Unknown3 = 0x00400000,
            BackAttacked = 0x00800000,
            Vit0 = 0x01000000,
            AngelWing = 0x02000000,
            Unknown4 = 0x04000000,
            Unknown5 = 0x08000000,
            Unknown6 = 0x10000000,
            Unknown7 = 0x20000000,
            Unknown8 = 0x40000000,
            Unknown9 = 0x80000000
        }

        public struct MagicData
        {
            public string OffsetSpellName;
            //public string OffsetSpellDescription;
            public UInt16 MagicID;
            public byte Unknown1;
            public byte AttackType;
            public byte SpellPower;
            public byte Unknown2;
            public byte DefaultTarget;
            public byte Unknown3;
            public byte DrawResist;
            public byte HitCount;
            public Element Element;
            //public byte Element; This now gets opted
            public byte Unknown4;
            public byte StatusMagic1;
            public byte StatusMagic2;
            public byte StatusMagic3;
            public byte StatusMagic4;
            public byte StatusMagic5;
            public UInt16 Unknown5;
            public byte HP;
            public byte STR;
            public byte VIT;
            public byte MAG;
            public byte SPR;
            public byte SPD;
            public byte EVA;
            public byte HIT;
            public byte LUCK;
            public byte ElemAttackEN;
            public byte ElemAttackVAL;
            public byte ElemDefenseEN;
            public byte ElemDefenseVAL;
            public byte StatusATKval;
            public byte StatusDEFval;
            public UInt16 StatusATKEN;
            public UInt16 StatusDefEN;
            public byte StatusAttackEnabler;
            public byte QuezacoltCompatibility;
            public byte ShivaCompatibility;
            public byte IfritCompatibility;
            public byte SirenCompatibility;
            public byte BrothersCompatibility;
            public byte DiablosCompatibility;
            public byte CarbuncleCompatibility;
            public byte LeviathanCompatibility;
            public byte PandemonaCompatibility;
            public byte CerberusCompatibility;
            public byte AlexanderCompatibility;
            public byte DoomtrainCompatibility;
            public byte BahamutCompatibility;
            public byte CactuarCompatibility;
            public byte TonberryCompatibility;
            public byte EdenCompatibility;
            public byte[] Unknown6;
        }

        public struct GFData
        {
            //public string OffsetGFName;
            //public string OffsetGFDescription;
            public UInt16 GFMagicID;
            public byte GFAttackType;
            public byte GFPower;
            public Element ElementGF;
            public byte StatusGF1;
            public byte StatusGF2;
            public byte StatusGF3;
            public byte StatusGF4;
            public byte StatusGF5;
            public byte GFHP;
            public byte GFStatusAttackEnabler;
            public byte GFPowerMod;
            public byte GFLevelMod;
            public UInt16 GFAbility1;
            public UInt16 GFAbility2;
            public UInt16 GFAbility3;
            public UInt16 GFAbility4;
            public UInt16 GFAbility5;
            public UInt16 GFAbility6;
            public UInt16 GFAbility7;
            public UInt16 GFAbility8;
            public UInt16 GFAbility9;
            public UInt16 GFAbility10;
            public UInt16 GFAbility11;
            public UInt16 GFAbility12;
            public UInt16 GFAbility13;
            public UInt16 GFAbility14;
            public UInt16 GFAbility15;
            public UInt16 GFAbility16;
            public UInt16 GFAbility17;
            public UInt16 GFAbility18;
            public UInt16 GFAbility19;
            public UInt16 GFAbility20;
            public UInt16 GFAbility21;
            public byte GFQuezacoltCompatibility;
            public byte GFShivaCompatibility;
            public byte GFIfritCompatibility;
            public byte GFSirenCompatibility;
            public byte GFBrothersCompatibility;
            public byte GFDiablosCompatibility;
            public byte GFCarbuncleCompatibility;
            public byte GFLeviathanCompatibility;
            public byte GFPandemonaCompatibility;
            public byte GFCerberusCompatibility;
            public byte GFAlexanderCompatibility;
            public byte GFDoomtrainCompatibility;
            public byte GFBahamutCompatibility;
            public byte GFCactuarCompatibility;
            public byte GFTonberryCompatibility;
            public byte GFEdenCompatibility;
        }

        public struct GFAttacksData
        {
            //public string OffsetGFAttacksName;
            public UInt16 GFAttacksMagicID;
            public byte GFAttacksType;
            public byte GFAttacksPower;
            public byte GFAttacksStatusEnabler;
            public Element ElementGFAttacks;
            public byte StatusGFAttacks1;
            public byte StatusGFAttacks2;
            public byte StatusGFAttacks3;
            public byte StatusGFAttacks4;
            public byte StatusGFAttacks5;
            public byte GFAttacksPowerMod;
            public byte GFAttacksLevelMod;
        }

        public struct WeaponsData
        {
            //public string WeaponsName;
            public byte RenzokukenFinishers;
            public Characters CharacterID;
            public byte AttackType;
            public byte AttackPower;
            public byte HITBonus;
            public byte STRBonus;
        }
        
        public static void UpdateVariable_Magic(int index, object variable, byte arg0 = 127)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 2:
                    {
                        UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_Magic); //MagicID
                        return;
                    }
                case 3:
                    {
                        Kernel[OffsetToMagicSelected + 8] = Convert.ToByte(variable); //SpellPower
                        return;
                    }
                case 4:
                    {
                        Kernel[OffsetToMagicSelected + 12] = Convert.ToByte(variable); //drawResist
                        return;
                    }
                case 5:
                    {
                        Kernel[OffsetToMagicSelected + 14] = Convert.ToByte(variable); //Element
                        return;
                    }
                case 6:
                    {
                        MagicStatusUpdator(arg0, variable); //Status
                        return;
                    }

                case 7:
                    {
                        Kernel[OffsetToMagicSelected + 23] = Convert.ToByte(variable); //HP
                        return;
                    }

                case 8:
                    {
                        Kernel[OffsetToMagicSelected + 24] = Convert.ToByte(variable); //STR
                        return;
                    }

                case 9:
                    {
                        Kernel[OffsetToMagicSelected + 25] = Convert.ToByte(variable); //VIT
                        return;
                    }

                case 10:
                    {
                        Kernel[OffsetToMagicSelected + 26] = Convert.ToByte(variable); //MAG
                        return;
                    }

                case 11:
                    {
                        Kernel[OffsetToMagicSelected + 27] = Convert.ToByte(variable); //SPR
                        return;
                    }

                case 12:
                    {
                        Kernel[OffsetToMagicSelected + 28] = Convert.ToByte(variable); //SPD
                        return;
                    }

                case 13:
                    {
                        Kernel[OffsetToMagicSelected + 29] = Convert.ToByte(variable); //EVA
                        return;
                    }

                case 14:
                    {
                        Kernel[OffsetToMagicSelected + 30] = Convert.ToByte(variable); //HITJ
                        return;
                    }

                case 15:
                    {
                        Kernel[OffsetToMagicSelected + 31] = Convert.ToByte(variable); //LUCK
                        return;
                    }

                case 16:
                    {
                        Kernel[OffsetToMagicSelected + 32] = Convert.ToByte(variable); // J-Elem attack enabler
                        return;
                    }

                case 17:
                    {
                        Kernel[OffsetToMagicSelected + 13] = Convert.ToByte(variable); //HIT [It's good, leave it]
                        return;
                    }

                case 18:
                    {
                        Kernel[OffsetToMagicSelected + 33] = Convert.ToByte(variable); //Characters J-Elem attack value
                        return;
                    }

                case 19:
                    {
                        Kernel[OffsetToMagicSelected + 34] ^= Convert.ToByte(variable); //Elemental Defense Modifier XOR!!!!!!!!!!
                        return;
                    }

                case 20:
                    {
                        Kernel[OffsetToMagicSelected + 35] = Convert.ToByte(variable); //Elemental Defense Value
                        return;
                    }

                case 21:
                    {
                        ushort a = BitConverter.ToUInt16(Kernel, OffsetToMagicSelected + 38);
                        byte[] temp = BitConverter.GetBytes(a ^= Convert.ToUInt16(variable));
                        Array.Copy(temp, 0, Kernel, OffsetToMagicSelected + 36, 2);
                        return;
                    }

                case 22:
                    {
                        ushort a = BitConverter.ToUInt16(Kernel, OffsetToMagicSelected + 40);
                        byte[] temp = BitConverter.GetBytes(a ^= Convert.ToUInt16(variable));
                        Array.Copy(temp, 0, Kernel, OffsetToMagicSelected + 39, 2);
                        return;
                    }

                case 23:
                    {
                        Kernel[OffsetToMagicSelected + 36] = Convert.ToByte(variable); //J-Status Attack Value
                        return;
                    }

                case 24:
                    {
                        Kernel[OffsetToMagicSelected + 37] = Convert.ToByte(variable); //J-Status Defense Value
                        return;
                    }
                case 25:
                    {
                        Kernel[OffsetToMagicSelected + 22] = Convert.ToByte(variable); //Status Attack Enabler
                        return;
                    }
                case 26:
                    {
                        Kernel[OffsetToMagicSelected + 42] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Quezacolt Compatibility
                        return;
                    }
                case 27:
                    {
                        Kernel[OffsetToMagicSelected + 43] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Shiva Compatibility
                        return;
                    }
                case 28:
                    {
                        Kernel[OffsetToMagicSelected + 44] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Ifrit Compatibility
                        return;
                    }
                case 29:
                    {
                        Kernel[OffsetToMagicSelected + 45] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Siren Compatibility
                        return;
                    }
                case 30:
                    {
                        Kernel[OffsetToMagicSelected + 46] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Brothers Compatibility
                        return;
                    }
                case 31:
                    {
                        Kernel[OffsetToMagicSelected + 47] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Diablos Compatibility
                        return;
                    }
                case 32:
                    {
                        Kernel[OffsetToMagicSelected + 48] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Carbuncle Compatibility
                        return;
                    }
                case 33:
                    {
                        Kernel[OffsetToMagicSelected + 49] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Leviathan Compatibility
                        return;
                    }
                case 34:
                    {
                        Kernel[OffsetToMagicSelected + 50] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Pandemona Compatibility
                        return;
                    }
                case 35:
                    {
                        Kernel[OffsetToMagicSelected + 51] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cerberus Compatibility
                        return;
                    }
                case 36:
                    {
                        Kernel[OffsetToMagicSelected + 52] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Alexander Compatibility
                        return;
                    }
                case 37:
                    {
                        Kernel[OffsetToMagicSelected + 53] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Doomtrain Compatibility
                        return;
                    }
                case 38:
                    {
                        Kernel[OffsetToMagicSelected + 54] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Bahamut Compatibility
                        return;
                    }
                case 39:
                    {
                        Kernel[OffsetToMagicSelected + 55] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cactuar Compatibility
                        return;
                    }
                case 40:
                    {
                        Kernel[OffsetToMagicSelected + 56] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Tonberry Compatibility
                        return;
                    }
                case 41:
                    {
                        Kernel[OffsetToMagicSelected + 57] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Eden Compatibility
                        return;
                    }
                case 42:
                    {
                        Kernel[OffsetToMagicSelected + 10] = Convert.ToByte(variable); //default target
                        return;
                    }


                default:
                    return;
            }

        }

        public static void MagicStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToMagicSelected + 16] = (byte)(Kernel[OffsetToMagicSelected + 16] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;
                case 1:
                    Kernel[OffsetToMagicSelected + 17] = (byte)(Kernel[OffsetToMagicSelected + 17] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToMagicSelected + 18] = (byte)(Kernel[OffsetToMagicSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToMagicSelected + 19] = (byte)(Kernel[OffsetToMagicSelected + 19] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToMagicSelected + 20] = (byte)(Kernel[OffsetToMagicSelected + 20] ^ Convert.ToByte(variable));
                    return;
            }
        }

        public static void UpdateVariable_GF(int index, object variable, byte AbilityIndex = 0, byte arg0 = 127)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_GF); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToGFSelected + 7] = Convert.ToByte(variable); //GFPower
                    return;
                case 2:
                    Kernel[OffsetToGFSelected + 13] = Convert.ToByte(variable); //Element
                    return;
                case 3:
                    Kernel[OffsetToGFSelected + 20] = Convert.ToByte(variable); //GFHP
                    return;
                case 4:
                    Kernel[OffsetToGFSelected + 130] = Convert.ToByte(variable); //Power Mod
                    return;
                case 5:
                    Kernel[OffsetToGFSelected + 131] = Convert.ToByte(variable); //Level Mod
                    return;
                case 6:
                    Kernel[OffsetToGFSelected + 30 + (AbilityIndex * 4)] = Convert.ToByte(variable); //GF abilities
                    return;
                case 7:
                    GFStatusUpdator(arg0, variable); //Status
                    return;
                case 8:
                    Kernel[OffsetToGFSelected + 27] = Convert.ToByte(variable); //enable modifier
                    return;
                case 9: //Reset Status
                    Kernel[OffsetToGFSelected + 14] = 0x00;
                    Kernel[OffsetToGFSelected + 16] = 0x00;
                    Kernel[OffsetToGFSelected + 17] = 0x00;
                    Kernel[OffsetToGFSelected + 18] = 0x00;
                    Kernel[OffsetToGFSelected + 19] = 0x00;
                    return;
                case 10:
                        Kernel[OffsetToMagicSelected + 112] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Quezacolt Compatibility
                        return;
                case 11:
                        Kernel[OffsetToMagicSelected + 113] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Shiva Compatibility
                        return;
                case 12:
                        Kernel[OffsetToMagicSelected + 114] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Ifrit Compatibility
                        return;
                case 13:
                        Kernel[OffsetToMagicSelected + 115] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Siren Compatibility
                        return;
                case 14:
                        Kernel[OffsetToMagicSelected + 116] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Brothers Compatibility
                        return;
                case 15:
                        Kernel[OffsetToMagicSelected + 117] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Diablos Compatibility
                        return;
                case 16:
                        Kernel[OffsetToMagicSelected + 118] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Carbuncle Compatibility
                        return;
                case 17:
                        Kernel[OffsetToMagicSelected + 119] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Leviathan Compatibility
                        return;
                case 18:
                        Kernel[OffsetToMagicSelected + 120] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Pandemona Compatibility
                        return;
                case 19:
                        Kernel[OffsetToMagicSelected + 121] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cerberus Compatibility
                        return;
                case 20:
                        Kernel[OffsetToMagicSelected + 122] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Alexander Compatibility
                        return;
                case 21:
                        Kernel[OffsetToMagicSelected + 123] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Doomtrain Compatibility
                        return;
                case 22:
                        Kernel[OffsetToMagicSelected + 124] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Bahamut Compatibility
                        return;
                case 23:
                        Kernel[OffsetToMagicSelected + 125] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cactuar Compatibility
                        return;
                case 24:
                        Kernel[OffsetToMagicSelected + 126] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Tonberry Compatibility
                        return;
                case 25:
                        Kernel[OffsetToMagicSelected + 127] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Eden Compatibility
                        return;

                default:
                    return;
            }
        }

        private static void GFStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToGFSelected + 14] = (byte)(Kernel[OffsetToGFSelected + 14] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;      
                case 1:          
                    Kernel[OffsetToGFSelected + 16] = (byte)(Kernel[OffsetToGFSelected + 16] ^ Convert.ToByte(variable));
                    return;     
                case 2:         
                    Kernel[OffsetToGFSelected + 17] = (byte)(Kernel[OffsetToGFSelected + 17] ^ Convert.ToByte(variable));
                    return;     
                case 3:         
                    Kernel[OffsetToGFSelected + 18] = (byte)(Kernel[OffsetToGFSelected + 18] ^ Convert.ToByte(variable));
                    return;       
                case 4:           
                    Kernel[OffsetToGFSelected + 19] = (byte)(Kernel[OffsetToGFSelected + 19] ^ Convert.ToByte(variable));
                    return;
            }
        }

        public static void UpdateVariable_GFAttacks(int index, object variable, byte arg0 = 127)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 2, (byte)Mode.Mode_GFAttacks); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToGFAttacksSelected + 5] = Convert.ToByte(variable); //GFPower
                    return;
                case 2:
                    Kernel[OffsetToGFAttacksSelected + 6] = Convert.ToByte(variable); //enable modifier
                    return;
                case 3:
                    Kernel[OffsetToGFAttacksSelected + 11] = Convert.ToByte(variable); //Element
                    return;
                case 4:
                    GFAttacksStatusUpdator(arg0, variable); //Status
                    return;
                case 5:
                    Kernel[OffsetToGFAttacksSelected + 18] = Convert.ToByte(variable); //Power Mod
                    return;
                case 6:
                    Kernel[OffsetToGFAttacksSelected + 19] = Convert.ToByte(variable); //Level Mod
                    return;
                case 7: //Reset Status
                    Kernel[OffsetToGFAttacksSelected + 12] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 13] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 14] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 15] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 16] = 0x00;
                    return;
                default:

                    return;
            }
        }

        private static void GFAttacksStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToGFAttacksSelected + 12] = (byte)(Kernel[OffsetToGFAttacksSelected + 12] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;
                case 1:
                    Kernel[OffsetToGFAttacksSelected + 13] = (byte)(Kernel[OffsetToGFAttacksSelected + 13] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToGFAttacksSelected + 14] = (byte)(Kernel[OffsetToGFAttacksSelected + 14] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToGFAttacksSelected + 15] = (byte)(Kernel[OffsetToGFAttacksSelected + 15] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToGFAttacksSelected + 16] = (byte)(Kernel[OffsetToGFAttacksSelected + 16] ^ Convert.ToByte(variable));
                    return;
            }
        }


        public static void UpdateVariable_Weapons(int index, object variable, byte arg0 = 127)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    RenzokukenFinishersUpdator(arg0, variable); //renzokuken finishers
                    return;
                case 1:
                    Kernel[OffsetToWeaponsSelected + 4] = Convert.ToByte(variable); //character id
                    return;
                case 2:
                    Kernel[OffsetToWeaponsSelected + 6] = Convert.ToByte(variable); //attack power
                    return;
                case 3:
                    Kernel[OffsetToWeaponsSelected + 7] = Convert.ToByte(variable); //hit bonus
                    return;
                case 4:
                    Kernel[OffsetToWeaponsSelected + 8] = Convert.ToByte(variable); //str bonus
                    return;

                default:
                    return;
            }
        }

        private static void RenzokukenFinishersUpdator(byte FinisherByteIndex, object variable)
        {
            switch (FinisherByteIndex)
            {
                case 0:
                    Kernel[OffsetToWeaponsSelected + 2] = (byte)(Kernel[OffsetToWeaponsSelected + 2] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;
            }
        }



        /// <summary>
        /// This is for MagicID list
        /// </summary>
        /// <param name="a"></param>
        /// <param name="add"></param>
        private static void UshortToKernel(ushort a, int add, byte mode)
        {
            byte[] magicIdBytes = BitConverter.GetBytes(a + 1);
            switch (mode)
            {
                case (byte) Mode.Mode_Magic:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToMagicSelected + add, 2);
                    break;
                case (byte) Mode.Mode_GF:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToGFSelected + add, 2);
                    break;
                case (byte)Mode.Mode_GFAttacks:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToGFAttacksSelected + add, 2);
                    break;
                default:
                    return;
            }
        }

        enum Mode : byte
        {
            Mode_Magic,
            Mode_GF,
            Mode_GFAttacks
        }


        public static void ReadKernel(byte[] kernel)
        {
            Kernel = kernel;
            MagicDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.MagicData);
            GFDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.GFs);
            GFAttacksDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.GFAttacks);
            WeaponsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Weapons);
        }


        public static void ReadMagic(int MagicID_List)
        {

            GetSelectedMagicData = new MagicData();
            MagicID_List++;
            /*
            int SelectedMagicOffset = MagicID_List == 0 ? 
                MagicDataOffset + 8 + (MagicID_List * 60) 
                : MagicDataOffset + (MagicID_List * 60);*/

            int selectedMagicOffset = MagicDataOffset + (MagicID_List * 60);
            OffsetToMagicSelected = selectedMagicOffset;

            #region UnusedNameRegion functionality. You can use it for future improvements
            GetSelectedMagicData.OffsetSpellName = BuildString((ushort)(
                    BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(Kernel, selectedMagicOffset))));
            //BELOW DOESN'T WORK?
            // GetSelectedMagicData.OffsetSpellDescription = BuildString((ushort)(
            //BitConverter.ToInt32(kernel, (int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(kernel, SelectedMagicOffset += 2))));
            //Console.WriteLine("DEBUG: {0}", GetSelectedMagicData.OffsetSpellName);
            #endregion


            GetSelectedMagicData.MagicID = BitConverter.ToUInt16(Kernel, selectedMagicOffset += 4);
            selectedMagicOffset += 2;
            GetSelectedMagicData.Unknown1 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.AttackType = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SpellPower = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown2 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DefaultTarget = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown3 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DrawResist = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HitCount = Kernel[selectedMagicOffset++];
            byte b = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedMagicData.Unknown4 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic1 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic2 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic3 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic4 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic5 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown5 = BitConverter.ToUInt16(Kernel, selectedMagicOffset ++);
            GetSelectedMagicData.StatusAttackEnabler = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HP = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.STR = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.VIT = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.MAG = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SPR = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SPD = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.EVA = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HIT = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.LUCK = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemAttackEN = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemAttackVAL = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemDefenseEN = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemDefenseVAL = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusATKval = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusDEFval = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusATKEN = BitConverter.ToUInt16(Kernel, selectedMagicOffset);
            selectedMagicOffset += 2;
            GetSelectedMagicData.StatusDefEN = BitConverter.ToUInt16(Kernel, selectedMagicOffset);
            selectedMagicOffset += 2;
            GetSelectedMagicData.QuezacoltCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ShivaCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.IfritCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SirenCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.BrothersCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DiablosCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.CarbuncleCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.LeviathanCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.PandemonaCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.CerberusCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.AlexanderCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DoomtrainCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.BahamutCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.CactuarCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.TonberryCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.EdenCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown6 = new byte[2];
            Array.Copy(Kernel, selectedMagicOffset, GetSelectedMagicData.Unknown6, 0, 2);
        }

        public static void ReadGF(int GFID_List)
        {
            GetSelectedGFData = new GFData();
            int selectedGfOffset = GFDataOffset + (GFID_List * 132);
            OffsetToGFSelected = selectedGfOffset;

            GetSelectedGFData.GFMagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedGfOffset + 4) - 1);
            selectedGfOffset += 4 + 2; //Name Offset + Description Offset + MagicID
            GetSelectedGFData.GFAttackType = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFPower = Kernel[selectedGfOffset];
            selectedGfOffset += 1 + 5; //Unknown + GFPower
            byte b = Kernel[selectedGfOffset++];
            GetSelectedGFData.ElementGF =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedGFData.StatusGF1 = Kernel[selectedGfOffset++];
            selectedGfOffset += 1;
            GetSelectedGFData.StatusGF2 = Kernel[selectedGfOffset++];
            GetSelectedGFData.StatusGF3 = Kernel[selectedGfOffset++];
            GetSelectedGFData.StatusGF4 = Kernel[selectedGfOffset++];
            GetSelectedGFData.StatusGF5 = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFHP = Kernel[selectedGfOffset];
            selectedGfOffset += 7; //Unknown+GFHP
            GetSelectedGFData.GFStatusAttackEnabler = Kernel[selectedGfOffset];
            selectedGfOffset += 3; //Status + unknown
            //AbilityRun
            GetSelectedGFData.GFAbility1 = Kernel[selectedGfOffset];
            GetSelectedGFData.GFAbility2 = Kernel[selectedGfOffset + (4 * 1)];
            GetSelectedGFData.GFAbility3 = Kernel[selectedGfOffset + (4 * 2)];
            GetSelectedGFData.GFAbility4 = Kernel[selectedGfOffset + (4 * 3)];
            GetSelectedGFData.GFAbility5 = Kernel[selectedGfOffset + (4 * 4)];
            GetSelectedGFData.GFAbility6 = Kernel[selectedGfOffset + (4 * 5)];
            GetSelectedGFData.GFAbility7 = Kernel[selectedGfOffset + (4 * 6)];
            GetSelectedGFData.GFAbility8 = Kernel[selectedGfOffset + (4 * 7)];
            GetSelectedGFData.GFAbility9 = Kernel[selectedGfOffset + (4 * 8)];
            GetSelectedGFData.GFAbility10 = Kernel[selectedGfOffset + (4 * 9)];
            GetSelectedGFData.GFAbility11 = Kernel[selectedGfOffset + (4 * 10)];
            GetSelectedGFData.GFAbility12 = Kernel[selectedGfOffset + (4 * 11)];
            GetSelectedGFData.GFAbility13 = Kernel[selectedGfOffset + (4 * 12)];
            GetSelectedGFData.GFAbility14 = Kernel[selectedGfOffset + (4 * 13)];
            GetSelectedGFData.GFAbility15 = Kernel[selectedGfOffset + (4 * 14)];
            GetSelectedGFData.GFAbility16 = Kernel[selectedGfOffset + (4 * 15)];
            GetSelectedGFData.GFAbility17 = Kernel[selectedGfOffset + (4 * 16)];
            GetSelectedGFData.GFAbility18 = Kernel[selectedGfOffset + (4 * 17)];
            GetSelectedGFData.GFAbility19 = Kernel[selectedGfOffset + (4 * 18)];
            GetSelectedGFData.GFAbility20 = Kernel[selectedGfOffset + (4 * 19)];
            GetSelectedGFData.GFAbility21 = Kernel[selectedGfOffset + (4 * 20)];
            //EndofAbility
            selectedGfOffset += (4 * 20) + 2;
            GetSelectedGFData.GFQuezacoltCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFShivaCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFIfritCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFSirenCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFBrothersCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFDiablosCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFCarbuncleCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFLeviathanCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFPandemonaCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFCerberusCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFAlexanderCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFDoomtrainCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFBahamutCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFCactuarCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFTonberryCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFEdenCompatibility = Kernel[selectedGfOffset++];
            selectedGfOffset += 2;
            GetSelectedGFData.GFPowerMod = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFLevelMod = Kernel[selectedGfOffset++];
        }

        public static void ReadGFAttacks(int GFAttacksID_List)
        {
            GetSelectedGFAttacksData = new GFAttacksData();
            int selectedGfAttacksOffset = GFAttacksDataOffset + (GFAttacksID_List * 20);
            OffsetToGFAttacksSelected = selectedGfAttacksOffset;

            GetSelectedGFAttacksData.GFAttacksMagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedGfAttacksOffset + 2) - 1);
            selectedGfAttacksOffset += 4;
            GetSelectedGFAttacksData.GFAttacksType = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.GFAttacksPower = Kernel[selectedGfAttacksOffset++];            
            GetSelectedGFAttacksData.GFAttacksStatusEnabler = Kernel[selectedGfAttacksOffset++];
            selectedGfAttacksOffset += 4;
            byte b = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.ElementGFAttacks =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedGFAttacksData.StatusGFAttacks1 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks2 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks3 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks4 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks5 = Kernel[selectedGfAttacksOffset++];
            selectedGfAttacksOffset += 1;
            GetSelectedGFAttacksData.GFAttacksPowerMod = Kernel[selectedGfAttacksOffset];
            GetSelectedGFAttacksData.GFAttacksLevelMod = Kernel[selectedGfAttacksOffset + 1];
        }


        public static void ReadWeapons(int WeaponsID_List)
        {
            GetSelectedWeaponsData = new WeaponsData();
            int selectedWeaponsOffset = WeaponsDataOffset + (WeaponsID_List * 12);
            OffsetToWeaponsSelected = selectedWeaponsOffset;

            GetSelectedWeaponsData.RenzokukenFinishers = Kernel[selectedWeaponsOffset + 2];
            selectedWeaponsOffset += 4;
            byte c = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.CharacterID =
                c == (byte)Characters.Squall
                    ? Characters.Squall
                    : c == (byte)Characters.Edea
                        ? Characters.Edea
                        : c == (byte)Characters.Irvine
                            ? Characters.Irvine
                            : c == (byte)Characters.Kiros
                                ? Characters.Kiros
                                : c == (byte)Characters.Laguna
                                    ? Characters.Laguna
                                    : c == (byte)Characters.Quistis
                                        ? Characters.Quistis
                                        : c == (byte)Characters.Rinoa
                                            ? Characters.Rinoa
                                            : c == (byte)Characters.Seifer
                                                ? Characters.Seifer
                                                : c == (byte)Characters.Selphie
                                                    ? Characters.Selphie
                                                    : c == (byte)Characters.Ward
                                                        ? Characters.Ward
                                                        : c == (byte)Characters.Zell
                                                            ? Characters.Zell
                                                           : 0; //Error handler
            GetSelectedWeaponsData.AttackType = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.AttackPower = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.HITBonus = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.STRBonus = Kernel[selectedWeaponsOffset++];
        }


        private static string BuildString(int index)
        {
            if (_charstable == null)
                _charstable = Chartable.Split(',');
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (Kernel[index] == 0x00)
                    return sb.ToString();
                char c = _charstable[Kernel[index++] - 31].ToCharArray()[0];
                sb.Append(c);
            }
        }



    }
}

