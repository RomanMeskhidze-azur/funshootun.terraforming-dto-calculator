using System.Collections.Generic;
using System.Linq;
using Common.Utils.Serialization;
using UnityEngine.Profiling;

namespace TerraformingDtoCalculator
{
    public partial class FromServerDto
    {
        public List<ChunkDTO> ChankDtos = new List<ChunkDTO>(TerraformingDtoCalculatorConstants.ChunksMaxCount);

        public FromServerDto()
        {
            ChankDtos.AddRange(Enumerable.Repeat(new ChunkDTO(), TerraformingDtoCalculatorConstants.ChunksMaxCount));
        }

        #region CodeGen

        public int ChankDtosCount
        {
            get => ChankDtos.Count;
            set => ChankDtos.Resize(value, () => new ChunkDTO());
        }

        public struct ChankDtosIndexer
        {
            public ChankDtosIndexer(int index)
            {
                Index = index;
            }

            public int Index;
        }

        public ChunkDTO this[ChankDtosIndexer index]
        {
            get => ChankDtos[index.Index];
            set => ChankDtos[index.Index] = value;
        }

        public void Ser(ISerializer packer)
        {
#if UNITY_EDITOR
            if (ChankDtosCount > 6500) throw new System.Exception("Max possible value for ChankDtosCount is 6500");
#endif

            packer.WriteInt(ChankDtosCount, 13);
            for (int i = 0; i < ChankDtosCount; i++)
            {
                this[new ChankDtosIndexer(i)].Ser(packer);
            }
        }

        public void Deser(ISerializer packer)
        {
            int ChankDtosCount;
            ChankDtosCount = packer.ReadInt(13);
            ChankDtos.Resize(ChankDtosCount, () => new ChunkDTO());
            for (int i = 0; i < ChankDtosCount; i++)
            {
                var codegenTemp45 = new ChunkDTO();
                codegenTemp45.Deser(packer);
                this[new ChankDtosIndexer(i)] = codegenTemp45;
            }
        }

        public bool IsEqual(FromServerDto other)
        {
            if (!this.ChankDtosCount.Equals(other.ChankDtosCount)) return false;
            for (int i = 0; i < ChankDtosCount; i++)
            {
                if (!this[new ChankDtosIndexer(i)].IsEqual(other[new ChankDtosIndexer(i)])) return false;
            }

            return true;
        }

        public void SerDiff(ISerializer packer, FromServerDto data)
        {
            packer.WriteBool(data != null);
            if (data == null)
            {
                Ser(packer);
                return;
            }

            var other = data;
            //packer.WriteInt(ChankDtosCount, 13);
            //var ChankDtosCountEqual = ChankDtosCount == other.ChankDtosCount;
            //packer.WriteBool(ChankDtosCountEqual);
            Profiler.BeginSample("IterateChunksCollection");
            //if (ChankDtosCountEqual)
            //{
                for (int i = 0; i < ChankDtosCount; i++)
                {
                    //packer.WriteBool(this[new ChankDtosIndexer(i)] != null);
                    //if (this[new ChankDtosIndexer(i)] != null)
                    //{
                    var isNotEqual = !this[new ChankDtosIndexer(i)].IsEqual(other[new ChankDtosIndexer(i)]);
                        packer.WriteBool(isNotEqual);
                        if (isNotEqual)
                        {
                            //packer.WriteBool(other[new ChankDtosIndexer(i)] != null);
                            //if (other[new ChankDtosIndexer(i)] != null)
                            //{
                            Profiler.BeginSample("SerDiffConcreteChunk");
                            this[new ChankDtosIndexer(i)].SerDiff(packer, other[new ChankDtosIndexer(i)]);
                            Profiler.EndSample();
                            // }
                            // else
                            // {
                            //     this[new ChankDtosIndexer(i)].Ser(packer);
                            // }
                        }
                    //}
                }
            // }
            // else
            // {
            //     var countToDiff = ChankDtosCount < other.ChankDtosCount ? ChankDtosCount : other.ChankDtosCount;
            //     packer.WriteInt(countToDiff, 13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         packer.WriteBool(this[new ChankDtosIndexer(i)] != null);
            //         if (this[new ChankDtosIndexer(i)] != null)
            //         {
            //             packer.WriteBool(!this[new ChankDtosIndexer(i)].IsEqual(other[new ChankDtosIndexer(i)]));
            //             if (!this[new ChankDtosIndexer(i)].IsEqual(other[new ChankDtosIndexer(i)]))
            //             {
            //                 packer.WriteBool(other[new ChankDtosIndexer(i)] != null);
            //                 if (other[new ChankDtosIndexer(i)] != null)
            //                 {
            //                     this[new ChankDtosIndexer(i)].SerDiff(packer, other[new ChankDtosIndexer(i)]);
            //                 }
            //                 else
            //                 {
            //                     this[new ChankDtosIndexer(i)].Ser(packer);
            //                 }
            //             }
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < ChankDtosCount; i++)
            //     {
            //         this[new ChankDtosIndexer(i)].Ser(packer);
            //     }
            // }
            Profiler.EndSample();
        }

        public void DeserDiff(ISerializer packer, FromServerDto other)
        {
            var isDiff = packer.ReadBool();
            if (!isDiff)
            {
                Deser(packer);
                return;
            }

            Profiler.BeginSample("ResizeChunksCollection");
            //ChankDtosCount = packer.ReadInt(13);
            Profiler.EndSample();

            //var ChankDtosCountEqual = packer.ReadBool();
            Profiler.BeginSample("IterateChunksCollection");
            //if (ChankDtosCountEqual)
            //{
                for (int i = 0; i < ChankDtosCount; i++)
                {
                    //if (packer.ReadBool())
                    //{
                        if (packer.ReadBool())
                        {
                            //if (packer.ReadBool())
                            //{
                                Profiler.BeginSample("ChunkDeserDiff");
                                this[new ChankDtosIndexer(i)].DeserDiff(packer, other[new ChankDtosIndexer(i)]);
                                Profiler.EndSample();
                            // }
                            // else
                            // {
                            //     var codegenTemp46 = new ChunkDTO();
                            //     codegenTemp46.Deser(packer);
                            //     this[new ChankDtosIndexer(i)] = codegenTemp46;
                            // }
                        }
                        else
                        {
                            //if (other[new ChankDtosIndexer(i)] != null)
                            //{
                                Profiler.BeginSample("CopyLink");
                                other[new ChankDtosIndexer(i)] = this[new ChankDtosIndexer(i)];
                                Profiler.EndSample();
                            // }
                            // else
                            // {
                            //     this[new ChankDtosIndexer(i)] = null;
                            // }
                        }
                    // }
                    // else
                    // {
                    //     this[new ChankDtosIndexer(i)] = null;
                    // }
                }
            // }
            // else
            // {
            //     int countToDiff;
            //     countToDiff = packer.ReadInt(13);
            //     for (int i = 0; i < countToDiff; i++)
            //     {
            //         if (packer.ReadBool())
            //         {
            //             if (packer.ReadBool())
            //             {
            //                 if (packer.ReadBool())
            //                     this[new ChankDtosIndexer(i)].DeserDiff(packer, other[new ChankDtosIndexer(i)]);
            //                 else
            //                 {
            //                     var codegenTemp47 = new ChunkDTO();
            //                     codegenTemp47.Deser(packer);
            //                     this[new ChankDtosIndexer(i)] = codegenTemp47;
            //                 }
            //             }
            //             else
            //             {
            //                 if (other[new ChankDtosIndexer(i)] != null)
            //                 {
            //                     other[new ChankDtosIndexer(i)].CopyTo(this[new ChankDtosIndexer(i)]);
            //                 }
            //                 else
            //                 {
            //                     this[new ChankDtosIndexer(i)] = null;
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             this[new ChankDtosIndexer(i)] = null;
            //         }
            //     }
            //
            //     for (int i = countToDiff; i < ChankDtosCount; i++)
            //     {
            //         var ChankDtosTemp = new ChunkDTO();
            //         ChankDtosTemp.Deser(packer);
            //         this[new ChankDtosIndexer(i)] = ChankDtosTemp;
            //     }
            // }
            Profiler.EndSample();
        }

        #endregion
    }
}