using System;
using System.Collections.Generic;
using Common.Utils.Serialization;
using UnityEngine;

namespace TerraformingDtoCalculator
{
    [Serializable]
    public partial class ChunkDTO : System.IEquatable<ChunkDTO>
    {
        public System.Collections.Generic.List<Int16> Vertices = new System.Collections.Generic.List<Int16>(TerraformingDtoCalculatorConstants.VerticesMaxCount);

        public int VerticesCount
        {
            get => Vertices.Count;
            set => Vertices.Resize(value, () => new Int16());
        }

        public struct VerticesIndexer
        {
            public VerticesIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Int16 this[VerticesIndexer index]
        {
            get => Vertices[index.Index];
            set => Vertices[index.Index] = value;
        }

        public System.Collections.Generic.List<Int32> Indices = new System.Collections.Generic.List<Int32>(TerraformingDtoCalculatorConstants.IndicesMaxCount);

        public int IndicesCount
        {
            get => Indices.Count;
            set => Indices.Resize(value, () => new Int32());
        }

        public struct IndicesIndexer
        {
            public IndicesIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Int32 this[IndicesIndexer index]
        {
            get => Indices[index.Index];
            set => Indices[index.Index] = value;
        }

        public System.Collections.Generic.List<Single> VertListX = new System.Collections.Generic.List<Single>(TerraformingDtoCalculatorConstants.VertListMaxCount);

        public int VertListXCount
        {
            get => VertListX.Count;
            set => VertListX.Resize(value, () => new Single());
        }

        public struct VertListXIndexer
        {
            public VertListXIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Single this[VertListXIndexer index]
        {
            get => VertListX[index.Index];
            set => VertListX[index.Index] = value;
        }

        public System.Collections.Generic.List<Single> VertListY = new System.Collections.Generic.List<Single>(TerraformingDtoCalculatorConstants.VertListMaxCount);

        public int VertListYCount
        {
            get => VertListY.Count;
            set => VertListY.Resize(value, () => new Single());
        }

        public struct VertListYIndexer
        {
            public VertListYIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Single this[VertListYIndexer index]
        {
            get => VertListY[index.Index];
            set => VertListY[index.Index] = value;
        }

        public System.Collections.Generic.List<Single> VertListZ = new System.Collections.Generic.List<Single>(TerraformingDtoCalculatorConstants.VertListMaxCount);

        public int VertListZCount
        {
            get => VertListZ.Count;
            set => VertListZ.Resize(value, () => new Single());
        }

        public struct VertListZIndexer
        {
            public VertListZIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Single this[VertListZIndexer index]
        {
            get => VertListZ[index.Index];
            set => VertListZ[index.Index] = value;
        }

        public void CopyTo(ChunkDTO target)
        {
            var original = this;
            var writer = new Common.Utils.Serialization.StrictBitsPacker(new byte[8]);
            writer.SetStream(new System.IO.MemoryStream());
            original.Ser(writer);
            var reader = new Common.Utils.Serialization.StrictBitsPacker(new byte[8]);
            reader.SetStream(new System.IO.MemoryStream(writer.Flush()));
            target.Deser(reader);
        }

        public virtual void Ser(ISerializer packer)
        {
#if UNITY_EDITOR
            if (VerticesCount > 11) throw new System.Exception("Max possible value for VerticesCount is 11");
#endif

            packer.WriteInt(VerticesCount, 4);
            for (int i = 0; i < VerticesCount; i++)
            {
                packer.WriteShort(this[new VerticesIndexer(i)], 16);
            }

#if UNITY_EDITOR
            if (IndicesCount > 11) throw new System.Exception("Max possible value for IndicesCount is 11");
#endif

            packer.WriteInt(IndicesCount, 4);
            for (int i = 0; i < IndicesCount; i++)
            {
                packer.WriteInt(this[new IndicesIndexer(i)], 32);
            }

#if UNITY_EDITOR
            if (VertListXCount > 11) throw new System.Exception("Max possible value for VertListXCount is 11");
#endif

            packer.WriteInt(VertListXCount, 4);
            for (int i = 0; i < VertListXCount; i++)
            {
                packer.WriteFloat(this[new VertListXIndexer(i)]);
            }

#if UNITY_EDITOR
            if (VertListYCount > 11) throw new System.Exception("Max possible value for VertListYCount is 11");
#endif

            packer.WriteInt(VertListYCount, 4);
            for (int i = 0; i < VertListYCount; i++)
            {
                packer.WriteFloat(this[new VertListYIndexer(i)]);
            }

#if UNITY_EDITOR
            if (VertListZCount > 11) throw new System.Exception("Max possible value for VertListZCount is 11");
#endif

            packer.WriteInt(VertListZCount, 4);
            for (int i = 0; i < VertListZCount; i++)
            {
                packer.WriteFloat(this[new VertListZIndexer(i)]);
            }
        }

        public bool Equals(ChunkDTO other)
        {
            return IsEqual(other);
        }

        public bool IsEqual(ChunkDTO other)
        {
            if (!this.VerticesCount.Equals(other.VerticesCount)) return false;
            for (int i = 0; i < VerticesCount; i++)
            {
                if (!this[new VerticesIndexer(i)].Equals(other[new VerticesIndexer(i)])) return false;
            }

            if (!this.IndicesCount.Equals(other.IndicesCount)) return false;
            for (int i = 0; i < IndicesCount; i++)
            {
                if (!this[new IndicesIndexer(i)].Equals(other[new IndicesIndexer(i)])) return false;
            }

            if (!this.VertListXCount.Equals(other.VertListXCount)) return false;
            for (int i = 0; i < VertListXCount; i++)
            {
                if (!this[new VertListXIndexer(i)].Equals(other[new VertListXIndexer(i)])) return false;
            }

            if (!this.VertListYCount.Equals(other.VertListYCount)) return false;
            for (int i = 0; i < VertListYCount; i++)
            {
                if (!this[new VertListYIndexer(i)].Equals(other[new VertListYIndexer(i)])) return false;
            }

            if (!this.VertListZCount.Equals(other.VertListZCount)) return false;
            for (int i = 0; i < VertListZCount; i++)
            {
                if (!this[new VertListZIndexer(i)].Equals(other[new VertListZIndexer(i)])) return false;
            }

            return true;
        }

        public virtual void Deser(ISerializer packer)
        {
            int VerticesCount;
            VerticesCount = packer.ReadInt(4);
            Vertices.Resize(VerticesCount, () => new Int16());
            for (int i = 0; i < VerticesCount; i++)
            {
                this[new VerticesIndexer(i)] = packer.ReadShort(16);
            }

            int IndicesCount;
            IndicesCount = packer.ReadInt(4);
            Indices.Resize(IndicesCount, () => new Int32());
            for (int i = 0; i < IndicesCount; i++)
            {
                this[new IndicesIndexer(i)] = packer.ReadInt(32);
            }

            int VertListXCount;
            VertListXCount = packer.ReadInt(4);
            VertListX.Resize(VertListXCount, () => new Single());
            for (int i = 0; i < VertListXCount; i++)
            {
                this[new VertListXIndexer(i)] = packer.ReadFloat();
            }

            int VertListYCount;
            VertListYCount = packer.ReadInt(4);
            VertListY.Resize(VertListYCount, () => new Single());
            for (int i = 0; i < VertListYCount; i++)
            {
                this[new VertListYIndexer(i)] = packer.ReadFloat();
            }

            int VertListZCount;
            VertListZCount = packer.ReadInt(4);
            VertListZ.Resize(VertListZCount, () => new Single());
            for (int i = 0; i < VertListZCount; i++)
            {
                this[new VertListZIndexer(i)] = packer.ReadFloat();
            }
        }

        public virtual void SerDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            packer.WriteInt(VerticesCount, 4);
            var VerticesCountEqual = VerticesCount == other.VerticesCount;
            packer.WriteBool(VerticesCountEqual);
            if (VerticesCountEqual)
            {
                for (int i = 0; i < VerticesCount; i++)
                {
                    packer.WriteBool(this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)]);
                    if (this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)])
                    {
                        packer.WriteShort(this[new VerticesIndexer(i)], 16);
                    }
                }
            }
            else
            {
                var countToDiff = VerticesCount < other.VerticesCount ? VerticesCount : other.VerticesCount;
                packer.WriteInt(countToDiff, 4);
                for (int i = 0; i < countToDiff; i++)
                {
                    packer.WriteBool(this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)]);
                    if (this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)])
                    {
                        packer.WriteShort(this[new VerticesIndexer(i)], 16);
                    }
                }

                for (int i = countToDiff; i < VerticesCount; i++)
                {
                    packer.WriteShort(this[new VerticesIndexer(i)], 16);
                }
            }

            packer.WriteInt(IndicesCount, 4);
            var IndicesCountEqual = IndicesCount == other.IndicesCount;
            packer.WriteBool(IndicesCountEqual);
            if (IndicesCountEqual)
            {
                for (int i = 0; i < IndicesCount; i++)
                {
                    packer.WriteBool(this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)]);
                    if (this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)])
                    {
                        packer.WriteInt(this[new IndicesIndexer(i)], 32);
                    }
                }
            }
            else
            {
                var countToDiff = IndicesCount < other.IndicesCount ? IndicesCount : other.IndicesCount;
                packer.WriteInt(countToDiff, 4);
                for (int i = 0; i < countToDiff; i++)
                {
                    packer.WriteBool(this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)]);
                    if (this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)])
                    {
                        packer.WriteInt(this[new IndicesIndexer(i)], 32);
                    }
                }

                for (int i = countToDiff; i < IndicesCount; i++)
                {
                    packer.WriteInt(this[new IndicesIndexer(i)], 32);
                }
            }

            packer.WriteInt(VertListXCount, 4);
            var VertListXCountEqual = VertListXCount == other.VertListXCount;
            packer.WriteBool(VertListXCountEqual);
            if (VertListXCountEqual)
            {
                for (int i = 0; i < VertListXCount; i++)
                {
                    packer.WriteBool(this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)]);
                    if (this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)])
                    {
                        packer.WriteFloat(this[new VertListXIndexer(i)]);
                    }
                }
            }
            else
            {
                var countToDiff = VertListXCount < other.VertListXCount ? VertListXCount : other.VertListXCount;
                packer.WriteInt(countToDiff, 4);
                for (int i = 0; i < countToDiff; i++)
                {
                    packer.WriteBool(this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)]);
                    if (this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)])
                    {
                        packer.WriteFloat(this[new VertListXIndexer(i)]);
                    }
                }

                for (int i = countToDiff; i < VertListXCount; i++)
                {
                    packer.WriteFloat(this[new VertListXIndexer(i)]);
                }
            }

            packer.WriteInt(VertListYCount, 4);
            var VertListYCountEqual = VertListYCount == other.VertListYCount;
            packer.WriteBool(VertListYCountEqual);
            if (VertListYCountEqual)
            {
                for (int i = 0; i < VertListYCount; i++)
                {
                    packer.WriteBool(this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)]);
                    if (this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)])
                    {
                        packer.WriteFloat(this[new VertListYIndexer(i)]);
                    }
                }
            }
            else
            {
                var countToDiff = VertListYCount < other.VertListYCount ? VertListYCount : other.VertListYCount;
                packer.WriteInt(countToDiff, 4);
                for (int i = 0; i < countToDiff; i++)
                {
                    packer.WriteBool(this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)]);
                    if (this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)])
                    {
                        packer.WriteFloat(this[new VertListYIndexer(i)]);
                    }
                }

                for (int i = countToDiff; i < VertListYCount; i++)
                {
                    packer.WriteFloat(this[new VertListYIndexer(i)]);
                }
            }

            packer.WriteInt(VertListZCount, 4);
            var VertListZCountEqual = VertListZCount == other.VertListZCount;
            packer.WriteBool(VertListZCountEqual);
            if (VertListZCountEqual)
            {
                for (int i = 0; i < VertListZCount; i++)
                {
                    packer.WriteBool(this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)]);
                    if (this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)])
                    {
                        packer.WriteFloat(this[new VertListZIndexer(i)]);
                    }
                }
            }
            else
            {
                var countToDiff = VertListZCount < other.VertListZCount ? VertListZCount : other.VertListZCount;
                packer.WriteInt(countToDiff, 4);
                for (int i = 0; i < countToDiff; i++)
                {
                    packer.WriteBool(this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)]);
                    if (this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)])
                    {
                        packer.WriteFloat(this[new VertListZIndexer(i)]);
                    }
                }

                for (int i = countToDiff; i < VertListZCount; i++)
                {
                    packer.WriteFloat(this[new VertListZIndexer(i)]);
                }
            }
        }

        public virtual void DeserDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            VerticesCount = packer.ReadInt(4);
            var VerticesCountEqual = packer.ReadBool();
            if (VerticesCountEqual)
            {
                for (int i = 0; i < VerticesCount; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VerticesIndexer(i)] = packer.ReadShort(16);
                    }
                    else
                    {
                        this[new VerticesIndexer(i)] = other[new VerticesIndexer(i)];
                    }
                }
            }
            else
            {
                int countToDiff;
                countToDiff = packer.ReadInt(4);
                for (int i = 0; i < countToDiff; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VerticesIndexer(i)] = packer.ReadShort(16);
                    }
                    else
                    {
                        this[new VerticesIndexer(i)] = other[new VerticesIndexer(i)];
                    }
                }

                for (int i = countToDiff; i < VerticesCount; i++)
                {
                    this[new VerticesIndexer(i)] = packer.ReadShort(16);
                }
            }

            IndicesCount = packer.ReadInt(4);
            var IndicesCountEqual = packer.ReadBool();
            if (IndicesCountEqual)
            {
                for (int i = 0; i < IndicesCount; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new IndicesIndexer(i)] = packer.ReadInt(32);
                    }
                    else
                    {
                        this[new IndicesIndexer(i)] = other[new IndicesIndexer(i)];
                    }
                }
            }
            else
            {
                int countToDiff;
                countToDiff = packer.ReadInt(4);
                for (int i = 0; i < countToDiff; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new IndicesIndexer(i)] = packer.ReadInt(32);
                    }
                    else
                    {
                        this[new IndicesIndexer(i)] = other[new IndicesIndexer(i)];
                    }
                }

                for (int i = countToDiff; i < IndicesCount; i++)
                {
                    this[new IndicesIndexer(i)] = packer.ReadInt(32);
                }
            }

            VertListXCount = packer.ReadInt(4);
            var VertListXCountEqual = packer.ReadBool();
            if (VertListXCountEqual)
            {
                for (int i = 0; i < VertListXCount; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VertListXIndexer(i)] = packer.ReadFloat();
                    }
                    else
                    {
                        this[new VertListXIndexer(i)] = other[new VertListXIndexer(i)];
                    }
                }
            }
            else
            {
                int countToDiff;
                countToDiff = packer.ReadInt(4);
                for (int i = 0; i < countToDiff; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VertListXIndexer(i)] = packer.ReadFloat();
                    }
                    else
                    {
                        this[new VertListXIndexer(i)] = other[new VertListXIndexer(i)];
                    }
                }

                for (int i = countToDiff; i < VertListXCount; i++)
                {
                    this[new VertListXIndexer(i)] = packer.ReadFloat();
                }
            }

            VertListYCount = packer.ReadInt(4);
            var VertListYCountEqual = packer.ReadBool();
            if (VertListYCountEqual)
            {
                for (int i = 0; i < VertListYCount; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VertListYIndexer(i)] = packer.ReadFloat();
                    }
                    else
                    {
                        this[new VertListYIndexer(i)] = other[new VertListYIndexer(i)];
                    }
                }
            }
            else
            {
                int countToDiff;
                countToDiff = packer.ReadInt(4);
                for (int i = 0; i < countToDiff; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VertListYIndexer(i)] = packer.ReadFloat();
                    }
                    else
                    {
                        this[new VertListYIndexer(i)] = other[new VertListYIndexer(i)];
                    }
                }

                for (int i = countToDiff; i < VertListYCount; i++)
                {
                    this[new VertListYIndexer(i)] = packer.ReadFloat();
                }
            }

            VertListZCount = packer.ReadInt(4);
            var VertListZCountEqual = packer.ReadBool();
            if (VertListZCountEqual)
            {
                for (int i = 0; i < VertListZCount; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VertListZIndexer(i)] = packer.ReadFloat();
                    }
                    else
                    {
                        this[new VertListZIndexer(i)] = other[new VertListZIndexer(i)];
                    }
                }
            }
            else
            {
                int countToDiff;
                countToDiff = packer.ReadInt(4);
                for (int i = 0; i < countToDiff; i++)
                {
                    if (packer.ReadBool())
                    {
                        this[new VertListZIndexer(i)] = packer.ReadFloat();
                    }
                    else
                    {
                        this[new VertListZIndexer(i)] = other[new VertListZIndexer(i)];
                    }
                }

                for (int i = countToDiff; i < VertListZCount; i++)
                {
                    this[new VertListZIndexer(i)] = packer.ReadFloat();
                }
            }
        }
    }
}