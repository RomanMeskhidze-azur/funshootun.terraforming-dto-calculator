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
            //packer.WriteInt(VerticesCount, 11);
            //var VerticesCountEqual = VerticesCount == other.VerticesCount;
            //packer.WriteBool(VerticesCountEqual);
            //if (VerticesCountEqual)
            //{
            Profiler.BeginSample("IterateVertices");
                for (int i = 0; i < VerticesCount; i++)
                {
                    packer.WriteBool(this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)]);
                    if (this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)])
                    {
                        packer.WriteShort(this[new VerticesIndexer(i)], 14);
                    }
                }
                Profiler.EndSample();
            // }
            // else
            // {
            //     var countToDiff = VerticesCount < other.VerticesCount ? VerticesCount : other.VerticesCount;
            //     packer.WriteInt(countToDiff, 11);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         packer.WriteBool(this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)]);
            //         if (this[new VerticesIndexer(i)] != other[new VerticesIndexer(i)])
            //         {
            //             packer.WriteShort(this[new VerticesIndexer(i)], 14);
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VerticesCount; i++)
            //     {
            //         packer.WriteShort(this[new VerticesIndexer(i)], 14);
            //     }
            // }

            // Profiler.BeginSample("IterateIndices");
            // packer.WriteInt(IndicesCount, 13);
            // var IndicesCountEqual = IndicesCount == other.IndicesCount;
            // packer.WriteBool(IndicesCountEqual);
            // if (IndicesCountEqual)
            // {
            //     for (int i = 0; i < IndicesCount; i++)
            //     {
            //         packer.WriteBool(this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)]);
            //         if (this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)])
            //         {
            //             packer.WriteInt(this[new IndicesIndexer(i)], 13);
            //         }
            //     }
            // }
            // else
            // {
            //     var countToDiff = IndicesCount < other.IndicesCount ? IndicesCount : other.IndicesCount;
            //     packer.WriteInt(countToDiff, 13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         packer.WriteBool(this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)]);
            //         if (this[new IndicesIndexer(i)] != other[new IndicesIndexer(i)])
            //         {
            //             packer.WriteInt(this[new IndicesIndexer(i)], 13);
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < IndicesCount; i++)
            //     {
            //         packer.WriteInt(this[new IndicesIndexer(i)], 13);
            //     }
            // }
            // Profiler.EndSample();
            //
            // Profiler.BeginSample("IterateVertListX");
            // packer.WriteInt(VertListXCount, 13);
            // var VertListXCountEqual = VertListXCount == other.VertListXCount;
            // packer.WriteBool(VertListXCountEqual);
            // if (VertListXCountEqual)
            // {
            //     for (int i = 0; i < VertListXCount; i++)
            //     {
            //         packer.WriteBool(this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)]);
            //         if (this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)])
            //         {
            //             packer.WriteByte(this[new VertListXIndexer(i)], 8);
            //         }
            //     }
            // }
            // else
            // {
            //     var countToDiff = VertListXCount < other.VertListXCount ? VertListXCount : other.VertListXCount;
            //     packer.WriteInt(countToDiff, 13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         packer.WriteBool(this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)]);
            //         if (this[new VertListXIndexer(i)] != other[new VertListXIndexer(i)])
            //         {
            //             packer.WriteByte(this[new VertListXIndexer(i)], 8);
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VertListXCount; i++)
            //     {
            //         packer.WriteByte(this[new VertListXIndexer(i)], 8);
            //     }
            // }
            // Profiler.EndSample();
            //
            // Profiler.BeginSample("IterateVertListY");
            // packer.WriteInt(VertListYCount, 13);
            // var VertListYCountEqual = VertListYCount == other.VertListYCount;
            // packer.WriteBool(VertListYCountEqual);
            // if (VertListYCountEqual)
            // {
            //     for (int i = 0; i < VertListYCount; i++)
            //     {
            //         packer.WriteBool(this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)]);
            //         if (this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)])
            //         {
            //             packer.WriteByte(this[new VertListYIndexer(i)], 8);
            //         }
            //     }
            // }
            // else
            // {
            //     var countToDiff = VertListYCount < other.VertListYCount ? VertListYCount : other.VertListYCount;
            //     packer.WriteInt(countToDiff, 13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         packer.WriteBool(this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)]);
            //         if (this[new VertListYIndexer(i)] != other[new VertListYIndexer(i)])
            //         {
            //             packer.WriteByte(this[new VertListYIndexer(i)], 8);
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VertListYCount; i++)
            //     {
            //         packer.WriteByte(this[new VertListYIndexer(i)], 8);
            //     }
            // }
            // Profiler.EndSample();
            //
            // Profiler.BeginSample("IterateVertListZ");
            // packer.WriteInt(VertListZCount, 13);
            // var VertListZCountEqual = VertListZCount == other.VertListZCount;
            // packer.WriteBool(VertListZCountEqual);
            // if (VertListZCountEqual)
            // {
            //     for (int i = 0; i < VertListZCount; i++)
            //     {
            //         packer.WriteBool(this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)]);
            //         if (this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)])
            //         {
            //             packer.WriteByte(this[new VertListZIndexer(i)], 8);
            //         }
            //     }
            // }
            // else
            // {
            //     var countToDiff = VertListZCount < other.VertListZCount ? VertListZCount : other.VertListZCount;
            //     packer.WriteInt(countToDiff, 13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         packer.WriteBool(this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)]);
            //         if (this[new VertListZIndexer(i)] != other[new VertListZIndexer(i)])
            //         {
            //             packer.WriteByte(this[new VertListZIndexer(i)], 8);
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VertListZCount; i++)
            //     {
            //         packer.WriteByte(this[new VertListZIndexer(i)], 8);
            //     }
            // }
            // Profiler.EndSample();
        }

        public virtual void DeserDiff(ISerializer packer, ChunkDTO data)
        {
            var other = data;
            Generation = packer.ReadByte(8);
            //VerticesCount = packer.ReadInt(11);
            //var VerticesCountEqual = packer.ReadBool();
            //if (VerticesCountEqual)
            //{
                for (int i = 0; i < VerticesCount; i++)
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
            // }
            // else
            // {
            //     int countToDiff;
            //     countToDiff = packer.ReadInt(11);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VerticesIndexer(i)] = packer.ReadShort(14);
            //         }
            //         else
            //         {
            //             this[new VerticesIndexer(i)] = other[new VerticesIndexer(i)];
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VerticesCount; i++)
            //     {
            //         this[new VerticesIndexer(i)] = packer.ReadShort(14);
            //     }
            // }

            // IndicesCount = packer.ReadInt(13);
            // var IndicesCountEqual = packer.ReadBool();
            // if (IndicesCountEqual)
            // {
            //     for (int i = 0; i < IndicesCount; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new IndicesIndexer(i)] = packer.ReadInt(13);
            //         }
            //         else
            //         {
            //             this[new IndicesIndexer(i)] = other[new IndicesIndexer(i)];
            //         }
            //     }
            // }
            // else
            // {
            //     int countToDiff;
            //     countToDiff = packer.ReadInt(13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new IndicesIndexer(i)] = packer.ReadInt(13);
            //         }
            //         else
            //         {
            //             this[new IndicesIndexer(i)] = other[new IndicesIndexer(i)];
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < IndicesCount; i++)
            //     {
            //         this[new IndicesIndexer(i)] = packer.ReadInt(13);
            //     }
            // }
            //
            // VertListXCount = packer.ReadInt(13);
            // var VertListXCountEqual = packer.ReadBool();
            // if (VertListXCountEqual)
            // {
            //     for (int i = 0; i < VertListXCount; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VertListXIndexer(i)] = packer.ReadByte(8);
            //         }
            //         else
            //         {
            //             this[new VertListXIndexer(i)] = other[new VertListXIndexer(i)];
            //         }
            //     }
            // }
            // else
            // {
            //     int countToDiff;
            //     countToDiff = packer.ReadInt(13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VertListXIndexer(i)] = packer.ReadByte(8);
            //         }
            //         else
            //         {
            //             this[new VertListXIndexer(i)] = other[new VertListXIndexer(i)];
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VertListXCount; i++)
            //     {
            //         this[new VertListXIndexer(i)] = packer.ReadByte(8);
            //     }
            // }
            //
            // VertListYCount = packer.ReadInt(13);
            // var VertListYCountEqual = packer.ReadBool();
            // if (VertListYCountEqual)
            // {
            //     for (int i = 0; i < VertListYCount; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VertListYIndexer(i)] = packer.ReadByte(8);
            //         }
            //         else
            //         {
            //             this[new VertListYIndexer(i)] = other[new VertListYIndexer(i)];
            //         }
            //     }
            // }
            // else
            // {
            //     int countToDiff;
            //     countToDiff = packer.ReadInt(13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VertListYIndexer(i)] = packer.ReadByte(8);
            //         }
            //         else
            //         {
            //             this[new VertListYIndexer(i)] = other[new VertListYIndexer(i)];
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VertListYCount; i++)
            //     {
            //         this[new VertListYIndexer(i)] = packer.ReadByte(8);
            //     }
            // }
            //
            // VertListZCount = packer.ReadInt(13);
            // var VertListZCountEqual = packer.ReadBool();
            // if (VertListZCountEqual)
            // {
            //     for (int i = 0; i < VertListZCount; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VertListZIndexer(i)] = packer.ReadByte(8);
            //         }
            //         else
            //         {
            //             this[new VertListZIndexer(i)] = other[new VertListZIndexer(i)];
            //         }
            //     }
            // }
            // else
            // {
            //     int countToDiff;
            //     countToDiff = packer.ReadInt(13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             this[new VertListZIndexer(i)] = packer.ReadByte(8);
            //         }
            //         else
            //         {
            //             this[new VertListZIndexer(i)] = other[new VertListZIndexer(i)];
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < VertListZCount; i++)
            //     {
            //         this[new VertListZIndexer(i)] = packer.ReadByte(8);
            //     }
            // }
        }
    }
}