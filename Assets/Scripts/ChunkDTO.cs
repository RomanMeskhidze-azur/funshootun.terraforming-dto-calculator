using System;
using System.Linq;
using Common.Utils.Serialization;
using UnityEngine.Profiling;

namespace TerraformingDtoCalculator
{
    [Serializable]
    public partial class ChunkDTO : System.IEquatable<ChunkDTO>
    {
        public Byte Generation;
        public System.Collections.Generic.List<Int16> Vertices = new System.Collections.Generic.List<Int16>(TerraformingDtoCalculatorConstants.VerticesMaxCount);

        public ChunkDTO()
        {
            Vertices.AddRange(Enumerable.Repeat(new Int16(), TerraformingDtoCalculatorConstants.VerticesMaxCount));
        }

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

        public System.Collections.Generic.List<Byte> VertListX = new System.Collections.Generic.List<Byte>(TerraformingDtoCalculatorConstants.VertListMaxCount);

        public int VertListXCount
        {
            get => VertListX.Count;
            set => VertListX.Resize(value, () => new Byte());
        }

        public struct VertListXIndexer
        {
            public VertListXIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Byte this[VertListXIndexer index]
        {
            get => VertListX[index.Index];
            set => VertListX[index.Index] = value;
        }

        public System.Collections.Generic.List<Byte> VertListY = new System.Collections.Generic.List<Byte>(TerraformingDtoCalculatorConstants.VertListMaxCount);

        public int VertListYCount
        {
            get => VertListY.Count;
            set => VertListY.Resize(value, () => new Byte());
        }

        public struct VertListYIndexer
        {
            public VertListYIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Byte this[VertListYIndexer index]
        {
            get => VertListY[index.Index];
            set => VertListY[index.Index] = value;
        }

        public System.Collections.Generic.List<Byte> VertListZ = new System.Collections.Generic.List<Byte>(TerraformingDtoCalculatorConstants.VertListMaxCount);

        public int VertListZCount
        {
            get => VertListZ.Count;
            set => VertListZ.Resize(value, () => new Byte());
        }

        public struct VertListZIndexer
        {
            public VertListZIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public Byte this[VertListZIndexer index]
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
            packer.WriteByte(Generation, 8);

#if UNITY_EDITOR
            if (VerticesCount > 1331) throw new System.Exception("Max possible value for VerticesCount is 1331");
#endif

            packer.WriteInt(VerticesCount, 11);
            for (int i = 0; i < VerticesCount; i++)
            {
                packer.WriteShort(this[new VerticesIndexer(i)], 14);
            }

#if UNITY_EDITOR
            if (IndicesCount > 5000) throw new System.Exception("Max possible value for IndicesCount is 5000");
#endif

            packer.WriteInt(IndicesCount, 13);
            for (int i = 0; i < IndicesCount; i++)
            {
                packer.WriteInt(this[new IndicesIndexer(i)], 13);
            }

#if UNITY_EDITOR
            if (VertListXCount > 5000) throw new System.Exception("Max possible value for VertListXCount is 5000");
#endif

            packer.WriteInt(VertListXCount, 13);
            for (int i = 0; i < VertListXCount; i++)
            {
                packer.WriteByte(this[new VertListXIndexer(i)], 8);
            }

#if UNITY_EDITOR
            if (VertListYCount > 5000) throw new System.Exception("Max possible value for VertListYCount is 5000");
#endif

            packer.WriteInt(VertListYCount, 13);
            for (int i = 0; i < VertListYCount; i++)
            {
                packer.WriteByte(this[new VertListYIndexer(i)], 8);
            }

#if UNITY_EDITOR
            if (VertListZCount > 5000) throw new System.Exception("Max possible value for VertListZCount is 5000");
#endif

            packer.WriteInt(VertListZCount, 13);
            for (int i = 0; i < VertListZCount; i++)
            {
                packer.WriteByte(this[new VertListZIndexer(i)], 8);
            }
        }

        public bool Equals(ChunkDTO other)
        {
            return IsEqual(other);
        }

        public bool IsEqual(ChunkDTO other)
        {
            return this.Generation == other.Generation;
        }

        public virtual void Deser(ISerializer packer)
        {
            Generation = packer.ReadByte(8);
            int VerticesCount;
            VerticesCount = packer.ReadInt(11);
            Vertices.Resize(VerticesCount, () => new Int16());
            for (int i = 0; i < VerticesCount; i++)
            {
                this[new VerticesIndexer(i)] = packer.ReadShort(14);
            }

            int IndicesCount;
            IndicesCount = packer.ReadInt(13);
            Indices.Resize(IndicesCount, () => new Int32());
            for (int i = 0; i < IndicesCount; i++)
            {
                this[new IndicesIndexer(i)] = packer.ReadInt(13);
            }

            int VertListXCount;
            VertListXCount = packer.ReadInt(13);
            VertListX.Resize(VertListXCount, () => new Byte());
            for (int i = 0; i < VertListXCount; i++)
            {
                this[new VertListXIndexer(i)] = packer.ReadByte(8);
            }

            int VertListYCount;
            VertListYCount = packer.ReadInt(13);
            VertListY.Resize(VertListYCount, () => new Byte());
            for (int i = 0; i < VertListYCount; i++)
            {
                this[new VertListYIndexer(i)] = packer.ReadByte(8);
            }

            int VertListZCount;
            VertListZCount = packer.ReadInt(13);
            VertListZ.Resize(VertListZCount, () => new Byte());
            for (int i = 0; i < VertListZCount; i++)
            {
                this[new VertListZIndexer(i)] = packer.ReadByte(8);
            }
        }

        public virtual void SerDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            packer.WriteByte(Generation, 8);
            
            Profiler.BeginSample("IterateVertices");
            for (var i = 0; i < VerticesCount; i++)
            {
                packer.WriteBool(this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)]);
                if (this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)])
                {
                    packer.WriteShort(this[new VerticesIndexer(i)], 14);
                }
            }
            Profiler.EndSample();
        }

        public virtual void DeserDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            Generation = packer.ReadByte(8);
            
            for (var i = 0; i < VerticesCount; i++)
            {
                if (packer.ReadBool())
                {
                    this[new VerticesIndexer(i)] = packer.ReadShort(14);
                }
                else
                {
                    this[new VerticesIndexer(i)] = other[new VerticesIndexer(i)];
                }
            }
        }
    }
}